using bs.component.integrations.Customers;
using bs.component.integrations.Requests;
using bs.order.application.Events;
using bs.order.domain.Enums;
using bs.order.domain.Models;
using MassTransit;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace bs.order.application.Commands.SubmitOrder
{
    public class SubmitOrderCommandHandler : IRequestHandler<SubmitOrderCommand, SubmitOrderResultDto>
    {
        private readonly IPublishEndpoint _publishEndpoint;
        private ICardDetailEvent _cardDetails;
        
        public SubmitOrderCommandHandler(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }

        public async Task<SubmitOrderResultDto> Handle(SubmitOrderCommand request, CancellationToken cancellationToken)
        {   
            if (request.Customer.CardDetails != null)
            {
                _cardDetails = new AddCardDetailEvent
                {
                    CardHolderName = request.Customer.CardDetails.CardHolderName,
                    CardNumber = long.Parse(request.Customer.CardDetails.CardNumber),
                    CardType = (int)request.Customer.CardDetails.CardType,
                    Expiry = request.Customer.CardDetails.Expiration,
                    SecurityNumber = request.Customer.CardDetails.SecurityNumber
                };
            }
            
            await _publishEndpoint.Publish<IOrderSubmitEvent>(new OrderSubmitEvent
            {
                OrderRef = request.OrderRef,
                Customer = new CreateCustomerEvent
                {
                    CorrelationId = request.OrderRef,
                    FirstName = request.Customer.FirstName,
                    LastName = request.Customer.LastName,
                    Dob = request.Customer.DateOfBirth,
                    EmailAddress = request.Customer.EmailAddress,
                    PhoneNumber = request.Customer.PhoneNumber,
                    BillingAddress = new AddAddress
                    {
                        City = request.Customer.BillingAddress.City,
                        Street = request.Customer.BillingAddress.Street,
                        PostCode = request.Customer.BillingAddress.PostCode,
                        Country = request.Customer.BillingAddress.Country
                    },
                    Consents = new AddConsent
                    {
                        ContactByCall = request.Customer.CustomerConsent.ContactByCall,
                        ContactByEmail = request.Customer.CustomerConsent.ContactByEmail,
                        ContactByPost = request.Customer.CustomerConsent.ContactByPost,
                        ContactByText = request.Customer.CustomerConsent.ContactByText
                    },
                    CardDetails = _cardDetails
                },
                BasketRef = request.BasketRef,
                Payment = new PaymentEvent
                {
                    PaymentType = (int)request.Payment.PaymentType
                }
            }, cancellationToken);

            return new SubmitOrderResultDto
            {   
                OrderRef = request.OrderRef,
                OrderStatus = OrderStatus.Paid.ToString()
            };
        }
    }
}
