using System;
using System.Linq;
using bs.component.integrations.Customers;
using MassTransit;
using System.Threading.Tasks;
using bs.component.integrations.Common;
using bs.order.domain.Entities;
using bs.order.domain.Enums;
using bs.order.domain.Repositories;

namespace bs.order.service.Consumers
{
    public class CustomerConsumer : IConsumer<ICustomer>
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerConsumer(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task Consume(ConsumeContext<ICustomer> context)
        {
            try
            {
                var customerEmail = await _customerRepository.FindByConditionAsync(c => c.EmailAddress == context.Message.EmailAddress);
            
                var customer = new Customer(
                    context.Message.FirstName
                    , context.Message.LastName
                    , context.Message.Dob
                    , context.Message.PhoneNumber
                    , context.Message.EmailAddress
                    , new Address(
                        context.Message.BillingAddress.Street
                        , context.Message.BillingAddress.City
                        , context.Message.BillingAddress.Country
                        , context.Message.BillingAddress.PostCode)
                    , context.Message.Consents.ContactByEmail
                    , context.Message.Consents.ContactByText
                    , context.Message.Consents.ContactByCall
                    , context.Message.Consents.ContactByPost);

                if (context.Message.CardDetails != null && context.Message.CardDetails.Any())
                {
                    foreach (var cardDetail in context.Message.CardDetails)
                    {
                        customer.AddCardDetails(cardDetail.CardHolderName, cardDetail.CardNumber, cardDetail.Expiry, cardDetail.SecurityNumber, (CardType)cardDetail.CardType);
                    }
                }

                if (!customerEmail.Any())
                {
                    _customerRepository.Add(customer);
                }
                else
                {
                    _customerRepository.Update(customer);
                }

                await _customerRepository.UnitOfWork.SaveEntitiesAsync();

                var result = (await _customerRepository.FindByConditionAsync(c => c.EmailAddress == context.Message.EmailAddress)).First();

                await context.RespondAsync<ICustomerCreated>(new
                {
                    context.Message.CorrelationId,
                    result.Id
                });
            }
            catch (Exception ex)
            {
                await context.RespondAsync<IOrderProcessingFailed>(new
                {
                    context.Message.CorrelationId,
                    ex.Message
                });
            }
        }
    }
}
