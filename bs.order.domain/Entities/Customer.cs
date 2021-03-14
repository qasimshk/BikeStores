using bs.component.sharedkernal.Abstractions;
using bs.component.sharedkernal.Common;
using bs.order.domain.Enums;
using bs.order.domain.Events;
using bs.order.domain.Exceptions;
using System;
using System.Collections.Generic;

namespace bs.order.domain.Entities
{
    public sealed class Customer : Entity, IAggregateRoot
    {
        private readonly List<CardDetail> _cardDetails;

        private Customer()
        {
            _cardDetails = new List<CardDetail>();
        }

        public Customer(string firstName, string lastName, DateTime dob, string phoneNumber, string emailAddress, Address billingAddress, bool contactByEmail, bool contactByText, bool contactByCall, bool contactByPost, string cardHolderName = null, long? cardNumber = 0, DateTime? expiration = null, int? securityNumber = 0, CardType? cardType = null)
        {
            if (dob.Date >= DateTime.Now.Date)
            {
                throw new CustomerDomainException("Customer date of birth is not valid");
            }

            FirstName = firstName;
            LastName = lastName;
            PhoneNumber = phoneNumber;
            EmailAddress = emailAddress;
            BillingAddress = billingAddress;
            Dob = dob;
            AddDomainEvent(new AddOrUpdateCustomerConsentDomainEvent(contactByEmail, contactByText, contactByCall, contactByPost, Id));
            
            if (!string.IsNullOrEmpty(cardHolderName) && cardNumber is not null && expiration is not null && securityNumber is not null && cardType is not null)
            {
                AddDomainEvent(new AddCardDetailsDomainEvent(cardHolderName, cardNumber.Value, expiration.Value, securityNumber.Value, cardType.Value, Id));
            }
        }

        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public DateTime Dob { get; private set; }
        public string PhoneNumber { get; private set; }
        public string EmailAddress { get; private set; }
        public Address BillingAddress { get; private set; }
        public Consent Consents { get; }
        public IReadOnlyCollection<CardDetail> CardDetails => _cardDetails;

        public string GetFullName => $"{FirstName} {LastName}";

        public int GetAge => DateTime.Now.Year - Dob.Year;

        public void AddCardDetails(string cardHolderName, long cardNumber, DateTime expiration, int securityNumber, CardType cardType) =>
            AddDomainEvent(new AddCardDetailsDomainEvent(cardHolderName, cardNumber, expiration, securityNumber, cardType, Id));
    }
}
