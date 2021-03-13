using bs.order.domain.Entities;
using System;
using System.Collections.Generic;
using bs.order.domain.Enums;

namespace bs.order.Tests.Seed
{
    public static class GetTestData
    {
        public static List<Customer> GetFakeCustomer()
        {
            return new List<Customer>
            {
                new Customer("Peter", "Parker", DateTime.Parse("1990-01-01"), "123456789", "test1@test.com", GetFakeAddress()),
                
                new Customer("James", "Mary", DateTime.Now, "123456789", "test2@test.com", GetFakeAddress()),
                
                new Customer("John", 
                    "Elizabeth", 
                    DateTime.Parse("1986-02-02"), 
                    "123456789", 
                    "test3@test.com", 
                    GetFakeAddress(),
                    "John",
                    1234567890123456,
                    DateTime.Parse("2022-02-02"),
                    123,
                    CardType.Visa)
            };
        }

        public static Address GetFakeAddress()
        {
            return new Address("Test Street", "Test City", "Test Country", "Test PostCode");
        }
    }
}
