using bs.order.domain.Entities;
using bs.order.domain.Exceptions;
using bs.order.infrastructure.Persistence.Context;
using bs.order.Tests.Seed;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using Xunit;

namespace bs.order.Tests.Domains
{
    public class CustomerTest : IDisposable
    {
        private readonly OrderDbContext _context;

        public CustomerTest()
        {
            var dbContextOptions = new DbContextOptionsBuilder<OrderDbContext>().UseInMemoryDatabase("Mock");
            _context = new OrderDbContext(dbContextOptions.Options);
            _context.Database.EnsureCreated();
        }

        [Fact]
        public void Create_Customer_Without_Card_Details_Success()
        {
            //Arrange
            var mockCustomer = GetTestData.GetFakeCustomer().First(x => x.EmailAddress == "test1@test.com");
            var mockAddress = GetTestData.GetFakeAddress();

            //Act
            var customer = new Customer(mockCustomer.FirstName,
                mockCustomer.LastName,
                mockCustomer.Dob,
                mockCustomer.PhoneNumber,
                mockCustomer.EmailAddress,
                new Address(mockAddress.Street,mockAddress.City,mockAddress.Country,mockAddress.PostCode),
                true,true,true,true);

            _context.Customers.Add(customer);
            
            _context.SaveChanges();

            var result = _context.Customers.First(x => x.EmailAddress == "test1@test.com");

            //Assert
            result.DomainEvents.Count.Should().Be(1);
            
            result.Id.Should().NotBe(0);
            
            result.GetAge.Should().Be(31);
            
            result.GetFullName.Should().Be($"{mockCustomer.FirstName} {mockCustomer.LastName}");
        }
        
        [Fact]
        public void Create_Customer_With_Card_Details_Success()
        {
            //Arrange
            var mockCustomer = GetTestData.GetFakeCustomer().First(x => x.EmailAddress == "test3@test.com");
            var mockAddress = GetTestData.GetFakeAddress();
            var mockCard = GetTestData.GetFakeCardDetails(1);

            //Act
            var customer = new Customer(mockCustomer.FirstName,
                mockCustomer.LastName,
                mockCustomer.Dob.Date,
                mockCustomer.PhoneNumber,
                mockCustomer.EmailAddress,
                new Address(mockAddress.Street, mockAddress.City, mockAddress.Country, mockAddress.PostCode),
                true, true, true, true,
                mockCard.CardHolderName,
                mockCard.CardNumber,
                mockCard.Expiration,
                mockCard.SecurityNumber,
                mockCard.CardType);

            _context.Customers.Add(customer);

            _context.SaveChanges();

            var result = _context.Customers.First(x => x.EmailAddress == "test3@test.com");

            //Assert
            result.Id.Should().NotBe(0);

            result.DomainEvents.Count.Should().Be(2);
        }

        
        [Fact]
        public void Create_Customer_With_Invalid_DOB_Fail()
        {
            //Arrange
            var mockCustomer = GetTestData.GetFakeCustomer().First(x => x.EmailAddress == "test2@test.com");
            var mockAddress = GetTestData.GetFakeAddress();
            
            //Act
            Action action = () => new Customer(mockCustomer.FirstName,
                mockCustomer.LastName,
                mockCustomer.Dob,
                mockCustomer.PhoneNumber,
                mockCustomer.EmailAddress,
                new Address(mockAddress.Street, mockAddress.City, mockAddress.Country, mockAddress.PostCode),
                true, true, true, true);
            
            //Assert
            action.Should().Throw<CustomerDomainException>().WithMessage("Customer date of birth is not valid");
        }

        [Fact]
        public void Create_Customer_With_Expired_Card_Fail()
        {
            //Arrange
            var mockCustomer = GetTestData.GetFakeCustomer().First(x => x.EmailAddress == "test3@test.com");
            var mockAddress = GetTestData.GetFakeAddress();
            var mockCard = GetTestData.GetFakeCardDetails(2);

            //Act
            Action action = () => new Customer(mockCustomer.FirstName,
                mockCustomer.LastName,
                mockCustomer.Dob.Date,
                mockCustomer.PhoneNumber,
                mockCustomer.EmailAddress,
                new Address(mockAddress.Street, mockAddress.City, mockAddress.Country, mockAddress.PostCode),
                true, true, true, true,
                mockCard.CardHolderName,
                mockCard.CardNumber,
                mockCard.Expiration,
                mockCard.SecurityNumber,
                mockCard.CardType);

            //Assert
            action.Should().Throw<OrderingDomainException>().WithMessage("This card is no longer valid");
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
