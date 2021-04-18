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

namespace bs.order.service.Consumers
{
    public class CustomerConsumer : IConsumer<ICustomerCreateEvent>
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerConsumer(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task Consume(ConsumeContext<ICustomerCreateEvent> context)
        {
            try
            {
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
