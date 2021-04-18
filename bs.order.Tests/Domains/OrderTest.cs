using bs.order.domain.Entities;
using bs.order.domain.Enums;
using bs.order.domain.Models;
using bs.order.infrastructure.Persistence.Context;
using bs.order.Tests.Seed;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace bs.order.Tests.Domains
{
    public class OrderTest : IDisposable
    {
        private readonly OrderDbContext _context;

        public OrderTest()
        {
            var dbContextOptions = new DbContextOptionsBuilder<OrderDbContext>().UseInMemoryDatabase("Mock");
            _context = new OrderDbContext(dbContextOptions.Options);
            _context.Database.EnsureCreated();
        }

        [Fact]
        public void Create_Order_Should_be_Successful()
        {
            //Arrange
            var customerId = 1;
            var mockOrder = GetTestData.GetFakeOrders(customerId);
            var mockOrderItem = mockOrder.MockOrderItems.First();

            //Act
            var order = new Order(mockOrder.OrderRef,
                mockOrder.PaymentId,
                mockOrder.CustomerId,
                new Address(mockOrder.DeliveryAddress.Street,
                    mockOrder.DeliveryAddress.City,
                    mockOrder.DeliveryAddress.Country,
                    mockOrder.DeliveryAddress.PostCode),
                new List<OrderItemEntry>
                {
                    new(mockOrderItem.ProductRef,
                        mockOrderItem.ProductName,
                        mockOrderItem.Quantity,
                        mockOrderItem.IndividualPrice)
                });

            _context.Orders.Add(order);

            _context.SaveChanges();

            var result = _context.Orders.First(o => o.OrderRef == mockOrder.OrderRef);
            
            //Assert
            result.Id.Should().NotBe(0);
            
            result.Status.Should().Be(OrderStatus.Paid);
        }

        [Fact]
        public void Existing_Order_Mark_Cancelled_Should_be_Successfully_Cancelled()
        {
            //Arrange
            var customerId = 1;
            var mockOrder = GetTestData.GetFakeOrders(customerId);
            var mockOrderItem = mockOrder.MockOrderItems.First();

            //Act
            var order = new Order(mockOrder.OrderRef,
                mockOrder.PaymentId,
                mockOrder.CustomerId,
                new Address(mockOrder.DeliveryAddress.Street,
                    mockOrder.DeliveryAddress.City,
                    mockOrder.DeliveryAddress.Country,
                    mockOrder.DeliveryAddress.PostCode),
                new List<OrderItemEntry>
                {
                    new(mockOrderItem.ProductRef,
                        mockOrderItem.ProductName,
                        mockOrderItem.Quantity,
                        mockOrderItem.IndividualPrice)
                });

            _context.Orders.Add(order);

            _context.SaveChanges();

            var result = _context.Orders.First(o => o.OrderRef == mockOrder.OrderRef);

            result.MarkOrderCancelled("Testing");

            //Assert
            result.Status.Should().Be(OrderStatus.Cancelled);
        }

        [Fact]
        public void Existing_Order_Mark_Delivered_Should_be_Successfully_Delivered()
        {
            //Arrange
            var customerId = 1;
            var mockOrder = GetTestData.GetFakeOrders(customerId);
            var mockOrderItem = mockOrder.MockOrderItems.First();

            //Act
            var order = new Order(mockOrder.OrderRef,
                mockOrder.PaymentId,
                mockOrder.CustomerId,
                new Address(mockOrder.DeliveryAddress.Street,
                    mockOrder.DeliveryAddress.City,
                    mockOrder.DeliveryAddress.Country,
                    mockOrder.DeliveryAddress.PostCode),
                new List<OrderItemEntry>
                {
                    new(mockOrderItem.ProductRef,
                        mockOrderItem.ProductName,
                        mockOrderItem.Quantity,
                        mockOrderItem.IndividualPrice)
                });

            _context.Orders.Add(order);

            _context.SaveChanges();

            var result = _context.Orders.First(o => o.OrderRef == mockOrder.OrderRef);

            result.MarkOrderDelivered();
            
            //Assert
            result.Status.Should().Be(OrderStatus.Delivered);
        }

        [Fact]
        public void Existing_Order_Mark_Refund_Should_be_Successfully_Refund()
        {
            //Arrange
            var customerId = 1;
            var mockOrder = GetTestData.GetFakeOrders(customerId);
            var mockOrderItem = mockOrder.MockOrderItems.First();

            //Act
            var order = new Order(mockOrder.OrderRef,
                mockOrder.PaymentId,
                mockOrder.CustomerId,
                new Address(mockOrder.DeliveryAddress.Street,
                    mockOrder.DeliveryAddress.City,
                    mockOrder.DeliveryAddress.Country,
                    mockOrder.DeliveryAddress.PostCode),
                new List<OrderItemEntry>
                {
                    new(mockOrderItem.ProductRef,
                        mockOrderItem.ProductName,
                        mockOrderItem.Quantity,
                        mockOrderItem.IndividualPrice)
                });

            _context.Orders.Add(order);

            _context.SaveChanges();

            var result = _context.Orders.First(o => o.OrderRef == mockOrder.OrderRef);

            result.MarkOrderDelivered();

            result.MarkOrderRefund("Testing");

            //Assert
            result.Status.Should().Be(OrderStatus.Refund);
        }
        
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
