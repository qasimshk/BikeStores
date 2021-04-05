using bs.component.sharedkernal.Abstractions;
using bs.component.sharedkernal.Common;
using bs.order.domain.Enums;
using bs.order.domain.Exceptions;
using System;
using System.Collections.Generic;

namespace bs.order.domain.Entities
{
    public sealed class Customer : Entity, IAggregateRoot
    {
        private readonly List<Order> _orders;
        private readonly List<Payment> _payments;
        
        private Customer()
        {
            _orders = new List<Order>();
            _payments = new List<Payment>();
        }

        public Customer(string firstName, string lastName, DateTime dob, string phoneNumber, string emailAddress, Address billingAddress, bool contactByEmail, bool contactByText, bool contactByCall, bool contactByPost, string cardHolderName = null, long? cardNumber = 0, DateTime? expiration = null, int? securityNumber = 0, CardType? cardType = null) : this()
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
            Consents = new Consent(contactByEmail, contactByText, contactByCall, contactByPost, Id);
            
            if (!string.IsNullOrEmpty(cardHolderName) && cardNumber is not null && expiration is not null && securityNumber is not null && cardType is not null)
            {
                CardDetails = new List<CardDetail>
                {
                    new(cardHolderName, cardNumber.Value, expiration.Value, securityNumber.Value, cardType.Value, Id)
                };
            }
        }

        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public DateTime Dob { get; private set; }
        public string PhoneNumber { get; private set; }
        public string EmailAddress { get; private set; }
        public Address BillingAddress { get; private set; }
        public Consent Consents { get; private set; }
        public IList<CardDetail> CardDetails { get; private set; }
        public IReadOnlyCollection<Order> Orders => _orders;
        public IReadOnlyCollection<Payment> Payments => _payments;
        public string GetFullName => $"{FirstName} {LastName}";

        public int GetAge => DateTime.Now.Year - Dob.Year;

        public Customer UpdatePersonalDetails(Customer customer)
        {
            if (!string.IsNullOrEmpty(customer.FirstName) && FirstName != customer.FirstName)
            {
                FirstName = customer.FirstName;
            }

            if (!string.IsNullOrEmpty(customer.LastName) && FirstName != customer.LastName)
            {
                FirstName = customer.FirstName;
            }

            if (!string.IsNullOrEmpty(customer.PhoneNumber) && FirstName != customer.PhoneNumber)
            {
                PhoneNumber = customer.PhoneNumber;
            }

            return this;
        }

        public void AddCardDetails(string cardHolderName, long cardNumber, DateTime expiration, int securityNumber, CardType cardType)
        {
            CardDetails = new List<CardDetail>
            {
                new(cardHolderName, cardNumber, expiration, securityNumber, cardType, Id)
            };
        }
    }
}
