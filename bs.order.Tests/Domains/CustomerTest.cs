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
        private const string emailOne = "test1@test.com";
        private const string emailTwo = "test2@test.com";
        private const string emailThree = "test3@test.com";

        public CustomerTest()
        {
            var dbContextOptions = new DbContextOptionsBuilder<OrderDbContext>().UseInMemoryDatabase("Mock");
            _context = new OrderDbContext(dbContextOptions.Options);
            _context.Database.EnsureCreated();
        }

        [Fact]
        public void Create_Customer_Without_Card_Details_Should_Be_Success()
        {
            //Arrange
            var fakeCustomer = GetTestData.GetFakeCustomer().First(x => x.EmailAddress == emailOne);
            var fakeAddress = GetTestData.GetFakeAddress();

            //Act
            var customer = new Customer(fakeCustomer.FirstName,
                fakeCustomer.LastName,
                fakeCustomer.Dob,
                fakeCustomer.PhoneNumber,
                fakeCustomer.EmailAddress,
                new Address(fakeAddress.Street, fakeAddress.City, fakeAddress.Country, fakeAddress.PostCode),
                true, true, true, true);

            _context.Customers.Add(customer);

            _context.SaveChanges();

            var result = _context.Customers.First(x => x.EmailAddress == emailOne);

            //Assert
            result.Id.Should().NotBe(0);

            result.GetAge.Should().Be(31);

            result.BillingAddress.ToString().Should().BeEquivalentTo($"{fakeAddress.Street} {fakeAddress.PostCode} {fakeAddress.City} {fakeAddress.Country}");

            result.GetFullName.Should().Be($"{fakeCustomer.FirstName} {fakeCustomer.LastName}");
        }

        [Fact]
        public void Create_Customer_With_Card_Details_Should_Be_Success()
        {
            //Arrange
            var fakeCustomer = GetTestData.GetFakeCustomer().First(x => x.EmailAddress == emailThree);
            var fakeAddress = GetTestData.GetFakeAddress();
            var fakeCardDetails = GetTestData.GetFakeCardDetails(CardStatus.Valid);

            //Act
            var customer = new Customer(fakeCustomer.FirstName,
                fakeCustomer.LastName,
                fakeCustomer.Dob.Date,
                fakeCustomer.PhoneNumber,
                fakeCustomer.EmailAddress,
                new Address(fakeAddress.Street, fakeAddress.City, fakeAddress.Country, fakeAddress.PostCode),
                true, true, true, true,
                fakeCardDetails.CardHolderName,
                fakeCardDetails.CardNumber,
                fakeCardDetails.Expiration,
                fakeCardDetails.SecurityNumber,
                fakeCardDetails.CardType);

            _context.Customers.Add(customer);

            _context.SaveChanges();

            var result = _context.Customers.First(x => x.EmailAddress == emailThree);

            //Assert
            result.CardDetails.First().GetCardNumber.Should().Be("xxxx xxxx xxxx 4444");

            result.CardDetails.First().GetExpiration.Should().Be("09/50");

            result.Id.Should().NotBe(0);
        }


        [Fact]
        public void Create_Customer_With_Invalid_DOB_Should_Be_Fail()
        {
            //Arrange
            var fakeCustomer = GetTestData.GetFakeCustomer().First(x => x.EmailAddress == emailTwo);
            var fakeAddress = GetTestData.GetFakeAddress();

            //Act
            Action action = () => new Customer(fakeCustomer.FirstName,
                fakeCustomer.LastName,
                fakeCustomer.Dob,
                fakeCustomer.PhoneNumber,
                fakeCustomer.EmailAddress,
                new Address(fakeAddress.Street, fakeAddress.City, fakeAddress.Country, fakeAddress.PostCode),
                true, true, true, true);

            //Assert
            action.Should().Throw<CustomerDomainException>().WithMessage("Customer date of birth is not valid");
        }

        [Fact]
        public void Create_Customer_With_Expired_Card_Should_Be_Fail()
        {
            //Arrange
            var fakeCardDetails = GetTestData.GetFakeCardDetails(CardStatus.Expired);

            //Act
            Action action = () => new CardDetail(
                fakeCardDetails.CardHolderName
                , fakeCardDetails.CardNumber
                , fakeCardDetails.Expiration
                , fakeCardDetails.SecurityNumber
                , fakeCardDetails.CardType
                , 1);

            //Assert
            action.Should().Throw<OrderingDomainException>().WithMessage("This card is no longer valid");
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
