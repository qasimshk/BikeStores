using bs.component.integrations.Common;
using bs.component.integrations.Customers;
using bs.component.sharedkernal.Utility;
using bs.order.domain.Entities;
using bs.order.domain.Enums;
using bs.order.domain.Models;
using bs.order.domain.Repositories;
using MassTransit;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace bs.order.service.Consumers
{
    public class CustomerConsumer : IConsumer<ICustomerEvent>
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerConsumer(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task Consume(ConsumeContext<ICustomerEvent> context)
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

                await context.RespondAsync<ICustomerCreatedEvent>(new CustomerCreated
                {
                    CorrelationId = context.Message.CorrelationId,
                    CustomerId = result.Id
                });
            }
            catch (Exception ex)
            {
                await context.RespondAsync<IOrderProcessingFailedEvent>(new OrderProcessingFailed
                {
                    CorrelationId = context.Message.CorrelationId,
                    ErrorMessage = ErrorUtility.BuildExceptionDetail(ex)
                });
            }
        }

        private Customer GetCustomerObject(ICustomerEvent context)
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

            if (context.CardDetails != null && context.CardDetails.Any())
            {
                foreach (var cardDetail in context.CardDetails)
                {
                    customer.AddCardDetails(cardDetail.CardHolderName, cardDetail.CardNumber, cardDetail.Expiry, cardDetail.SecurityNumber, (CardType)cardDetail.CardType);
                }
            }
            return customer;
        }
    }
}
