using bs.order.infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using bs.order.domain.Entities;
using bs.order.domain.Enums;
using bs.order.Tests.Seed;
using FluentAssertions;
using Xunit;
using bs.order.domain.Exceptions;

namespace bs.order.Tests.Domains
{
    public class PaymentTest : IDisposable
    {
        private readonly OrderDbContext _context;

        public PaymentTest()
        {
            var dbContextOptions = new DbContextOptionsBuilder<OrderDbContext>().UseInMemoryDatabase("Mock");
            _context = new OrderDbContext(dbContextOptions.Options);
            _context.Database.EnsureCreated();
        }

        [Fact]
        public void Create_Payment_Should_Be_Successful_And_Transaction_Successful_And_Place_Order()
        {
            //Arrange
            var customerId = 1;
            var mockPayment = GetTestData.GetFakeCustomerPayments(customerId);
            var mockOrder = GetTestData.GetFakeOrders(customerId);
            var mockOrderItem = mockOrder.MockOrderItems.First();

            //Act
            var payment = new Payment(mockPayment.CustomerId,
                mockPayment.Amount,
                mockPayment.PaymentType,
                mockPayment.PaymentRef,
                mockPayment.CardDetailId);

            payment.MarkTransactionSuccessful();

            _context.Payments.Add(payment);

            _context.SaveChanges();

            var result = _context.Payments.First(p => p.PaymentRef == mockPayment.PaymentRef);

            //Assert
            result.Id.Should().NotBe(0);

            result.Status.Should().Be(TransactionStatus.Successful);
        }

        [Theory]
        [InlineData(3)]
        public void Create_Payment_Should_Be_Failed_With_Exception(int customerId)
        {
            //Arrange
            var mockPayment = GetTestData.GetFakeCustomerPayments(customerId);
            
            //Act
            Action action = () => new Payment(mockPayment.CustomerId,
                mockPayment.Amount,
                mockPayment.PaymentType,
                mockPayment.PaymentRef,
                mockPayment.CardDetailId);

            //Assert
            action.Should().Throw<PaymentDomainException>().WithMessage("Insufficient Amount");
        }

        [Fact]
        public void Create_Payment_Should_Be_Failed_And_Transaction_Declined()
        {
            //Arrange
            var customerId = 1;
            var mockPayment = GetTestData.GetFakeCustomerPayments(customerId);
            
            //Act
            var payment = new Payment(mockPayment.CustomerId,
                mockPayment.Amount,
                mockPayment.PaymentType,
                mockPayment.PaymentRef,
                mockPayment.CardDetailId);

            payment.MarkTransactionAsDeclined();

            _context.Payments.Add(payment);

            _context.SaveChanges();

            var result = _context.Payments.First(p => p.PaymentRef == mockPayment.PaymentRef);

            //Assert
            result.Id.Should().NotBe(0);

            result.Status.Should().Be(TransactionStatus.Declined);
        }

        [Fact]
        public void Create_Payment_Should_Be_Failed_And_Transaction_Refunded()
        {
            //Arrange
            var customerId = 1;
            var mockPayment = GetTestData.GetFakeCustomerPayments(customerId);
            var mockOrder = GetTestData.GetFakeOrders(customerId);
            var mockOrderItem = mockOrder.MockOrderItems.First();

            //Act
            var payment = new Payment(mockPayment.CustomerId,
                mockPayment.Amount,
                mockPayment.PaymentType,
                mockPayment.PaymentRef,
                mockPayment.CardDetailId);

            _context.Payments.Add(payment);

            _context.SaveChanges();

            payment.MarkTransactionSuccessful();
            
            var result = _context.Payments.First(p => p.PaymentRef == mockPayment.PaymentRef);

            result.MarkTransactionAsRefunded();

            //Assert
            result.Id.Should().NotBe(0);

            result.Status.Should().Be(TransactionStatus.Refund);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
