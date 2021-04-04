using bs.component.integrations.Common;
using bs.component.integrations.Customers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace bs.order.domain.Models
{
    public class CreateCustomer : ICustomer
    {
        private readonly ICustomer _customer;
        private List<ICardDetail> _cardDetailList;

        public CreateCustomer(ICustomer customer)
        {
            _customer = customer;
            _cardDetailList = new List<ICardDetail>();

            if (_customer.CardDetails != null && _customer.CardDetails.Any())
            {
                foreach (var cardDetail in _customer.CardDetails)
                {
                    _cardDetailList.Add(new AddCardDetail(cardDetail));
                }
            }
        }
        
        public Guid CorrelationId => _customer.CorrelationId;
        public string FirstName => _customer.FirstName;
        public string LastName => _customer.LastName;
        public DateTime Dob => _customer.Dob;
        public string PhoneNumber => _customer.PhoneNumber;
        public string EmailAddress => _customer.EmailAddress;
        public IAddress BillingAddress => new AddAddress(_customer.BillingAddress);
        public IConsent Consents => new AddConsent(_customer.Consents);
        public IList<ICardDetail> CardDetails => _cardDetailList;
    }
}
