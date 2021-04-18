using bs.component.integrations.Common;
using bs.component.integrations.Customers;
using bs.component.sharedkernal.Utility;
using bs.order.domain.Entities;
using bs.order.domain.Enums;
using bs.order.domain.Repositories;
using bs.order.service.Events;
using MassTransit;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace bs.order.service.Consumers
{
    public class CustomerConsumer : IConsumer<ICustomerCreateEvent>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly ILogger<CustomerConsumer> _logger;

        public CustomerConsumer(ICustomerRepository customerRepository, ILogger<CustomerConsumer> logger)
        {
            _customerRepository = customerRepository;
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<ICustomerCreateEvent> context)
        {
            try
            {
                _logger.LogInformation($"Customer Request received for order ref: {context.Message.CorrelationId}");

                var customer = GetCustomerObject(context.Message);

                var checkCustomer = (await _customerRepository.FindByConditionAsync(c => c.EmailAddress == context.Message.EmailAddress)).ToList();

                if (!checkCustomer.Any())
                {
                    _customerRepository.Add(customer);
                }
                else
                {
                    _customerRepository.Update(checkCustomer.Single().UpdatePersonalDetails(customer));
                }
                
                await _customerRepository.UnitOfWork.SaveEntitiesAsync();
                
                _logger.LogInformation($"Customer Request saved successfully for order ref: {context.Message.CorrelationId}");

                var result = (await _customerRepository.FindByConditionAsync(c => c.EmailAddress == context.Message.EmailAddress)).Single();

                int cardDetailId = 0;

                if (context.Message.CardDetails is not null)
                {
                    cardDetailId = result.CardDetails.First(x => x.CardNumberUnFormatted == context.Message.CardDetails.CardNumber).Id;
                }
                
                await context.RespondAsync<ICustomerCreatedEvent>(new CustomerCreatedEvent
                {
                    CorrelationId = context.Message.CorrelationId,
                    CustomerId = result.Id,
                    CardDetailId = cardDetailId
                });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Customer process failed for order ref: {context.Message.CorrelationId} with error: {ErrorUtility.BuildExceptionDetail(ex)}");

                await context.RespondAsync<IOrderProcessingFailedEvent>(new OrderProcessingFailedEvent
                {
                    OrderRef = context.Message.CorrelationId,
                    ErrorMessage = ErrorUtility.BuildExceptionDetail(ex)
                });
            }
        }

        private Customer GetCustomerObject(ICustomerCreateEvent context)
        {
            var customer = new Customer(
                context.FirstName
                , context.LastName
                , context.Dob
                , context.PhoneNumber.ToString()
                , context.EmailAddress
                , new Address(
                    context.BillingAddress.Street
                    , context.BillingAddress.City
                    , context.BillingAddress.Country
                    , context.BillingAddress.PostCode)
                , context.Consents.ContactByEmail
                , context.Consents.ContactByText
                , context.Consents.ContactByCall
                , context.Consents.ContactByPost);

            if (context.CardDetails != null)
            {
                customer.AddCardDetails(context.CardDetails.CardHolderName
                    , context.CardDetails.CardNumber
                    , context.CardDetails.Expiry
                    , context.CardDetails.SecurityNumber
                    , (CardType)context.CardDetails.CardType);
            }
            return customer;
        }
    }
}
