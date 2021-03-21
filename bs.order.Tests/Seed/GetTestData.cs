using bs.order.domain.Enums;
using bs.order.Tests.Models;
using System;
using System.Collections.Generic;

namespace bs.order.Tests.Seed
{
    public enum CardStatus
    {
        Valid,
        Expired
    }

    public static class GetTestData
    {
        public static List<MockCustomer> GetFakeCustomer()
        {
            return new()
            {
                new MockCustomer
                {
                    FirstName = "Peter",
                    LastName = "Parker",
                    Dob = DateTime.Parse("1990-01-01"),
                    EmailAddress = "test1@test.com",
                    PhoneNumber = "123456789"
                },
                new MockCustomer
                {
                    FirstName = "James",
                    LastName = "Mary",
                    Dob = DateTime.Now.Date,
                    EmailAddress = "test2@test.com",
                    PhoneNumber = "123456789"
                },
                new MockCustomer
                {
                    FirstName = "John",
                    LastName = "Elizabeth",
                    Dob = DateTime.Parse("1986-02-02"),
                    EmailAddress = "test3@test.com",
                    PhoneNumber = "123456789"
                }
            };
        }

        public static MockAddress GetFakeAddress()
        {
            return new()
            {
                Street = "Test Street",
                City = "Test City",
                PostCode = "Test PostCode",
                Country = "Test PostCode",

            };
        }

        public static MockCardDetails GetFakeCardDetails(CardStatus status)
        {
            return status switch
            {
               CardStatus.Valid => new MockCardDetails
               {
                   CardHolderName = "Peter Parker",
                   CardNumber = 5555555555554444,
                   CardType = CardType.Master,
                   Expiration = DateTime.Parse("2050-09-09"),
                   SecurityNumber = 123
               },
               CardStatus.Expired => new MockCardDetails
               {
                   CardHolderName = "John Elizabeth",
                   CardNumber = 4012888888881881,
                   CardType = CardType.Visa,
                   Expiration = DateTime.Parse("2010-09-09"),
                   SecurityNumber = 123
               },
               _ => default
            };
        }

        public static MockPayment GetFakeCustomerPayments(int customerId)
        {
            return customerId switch
            {
                0 => new MockPayment
                {
                    PaymentType = PaymentType.Cash,
                    Amount = 0,
                    CustomerId = customerId,
                    PaymentRef = Guid.NewGuid()
                },
                1 => new MockPayment
                {
                    PaymentType = PaymentType.Card,
                    Amount = 1200,
                    CardDetailId = 1,
                    CustomerId = customerId,
                    PaymentRef = Guid.NewGuid()
                },
                2 => new MockPayment
                {
                    PaymentType = PaymentType.Cash,
                    Amount = 500,
                    CustomerId = customerId,
                    PaymentRef = Guid.NewGuid()
                },
                3 => new MockPayment
                {
                    PaymentType = PaymentType.Cash,
                    Amount = 0,
                    CustomerId = customerId,
                    PaymentRef = Guid.NewGuid()
                },
                _ => default
            };
        }

        public static MockOrder GetFakeOrders(int customerId)
        {
            return customerId switch
            {
                0 => new MockOrder
                {
                    Status = OrderStatus.Paid,
                    CustomerId = customerId,
                    OrderRef = Guid.NewGuid(),
                    PaymentId = 1,
                    DeliveryAddress = GetFakeAddress(),
                    MockOrderItems = new List<MockOrderItem>
                    {
                        new()
                        {
                            IndividualPrice = 500,
                            OrderId = 1,
                            ProductName = "Product One",
                            ProductRef = Guid.NewGuid(),
                            Quantity = 1
                        }
                    }
                },
                1 => new MockOrder
                {
                    Status = OrderStatus.Paid,
                    CustomerId = customerId,
                    OrderRef = Guid.NewGuid(),
                    PaymentId = 1,
                    DeliveryAddress = GetFakeAddress(),
                    MockOrderItems = new List<MockOrderItem>
                    {
                        new()
                        {
                            IndividualPrice = 500,
                            OrderId = 1,
                            ProductName = "Product One",
                            ProductRef = Guid.NewGuid(),
                            Quantity = 1
                        }
                    }
                },
                2 => new MockOrder
                {
                    Status = OrderStatus.Paid,
                    CustomerId = customerId,
                    OrderRef = Guid.NewGuid(),
                    PaymentId = 0,
                    DeliveryAddress = GetFakeAddress(),
                    MockOrderItems = new List<MockOrderItem>
                    {
                        new()
                        {
                            IndividualPrice = 500,
                            OrderId = 1,
                            ProductName = "Product One",
                            ProductRef = Guid.NewGuid(),
                            Quantity = 1
                        }
                    }
                },
                _ => default
            };

        }
    }
}
