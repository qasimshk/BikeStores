using bs.order.domain.Entities;
using bs.order.domain.Enums;
using bs.order.domain.Exceptions;
using bs.order.Tests.Seed;
using FluentAssertions;
using System;
using Xunit;

namespace bs.order.Tests.Domains
{
    public class CustomerTest
    {
        [Fact]
        public void Create_Customer_Without_Card_Details_Success()
        {
            //Act
            var customer = new Customer("Peter", "Parker", DateTime.Parse("1990-01-01"), "123456789", "test1@test.com",
                GetTestData.GetFakeAddress());

            //Assert
            customer.Should().NotBe(null);
        }

        [Fact]
        public void Create_Customer_With_Card_Details_Success()
        {
            //Act
            var customer = new Customer("John",
                "Elizabeth",
                DateTime.Parse("1986-02-02"),
                "123456789",
                "test3@test.com",
                GetTestData.GetFakeAddress(),
                "John",
                1234567890123456,
                DateTime.Parse("2022-02-02"),
                123,
                CardType.Visa);

            //Assert
            customer.DomainEvents.Count.Should().Be(1);
        }

        [Fact]
        public void Create_Customer_With_Invalid_DOB_Fail()
        {
            //Act
            Action action = () => new Customer("James", "Mary", DateTime.Now, "123456789", "test2@test.com", GetTestData.GetFakeAddress());

            //Assert
            action.Should().Throw<CustomerDomainException>().WithMessage("Customer date of birth is not valid");
        }
    }
}
