using System;
using bs.component.integrations.Orders;
using bs.order.domain.Entities;
using bs.order.domain.Models;
using bs.order.domain.Repositories;
using MassTransit;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;
using bs.component.sharedkernal.Utility;
using bs.order.service.Events;
using bs.component.integrations.Common;

namespace bs.order.service.Consumers
{
    public class OrderConsumer : IConsumer<IOrderCreateEvent>
    {
        private readonly ILogger<OrderConsumer> _logger;
        private readonly IOrderRepository _orderRepository;
        private readonly ICustomerRepository _customerRepository;

        public OrderConsumer(ILogger<OrderConsumer> logger, IOrderRepository orderRepository, ICustomerRepository customerRepository)
        {
            _logger = logger;
            _orderRepository = orderRepository;
            _customerRepository = customerRepository;
        }

        public async Task Consume(ConsumeContext<IOrderCreateEvent> context)
        {
            try
            {
                _logger.LogInformation($"Order create request received for order ref: {context.Message.OrderRef}");

                var orderItems = context.Message.OrderItems.Select(item =>
                    new OrderItemEntry(item.ProductRef, item.ProductName, item.Quantity, item.Price)).ToList();

                var order = new Order(
                    context.Message.OrderRef
                    , context.Message.PaymentId
                    , context.Message.CustomerId
                    , new Address(
                        context.Message.DeliveryAddress.Street
                        , context.Message.DeliveryAddress.City
                        , context.Message.DeliveryAddress.Country
                        , context.Message.DeliveryAddress.PostCode)
                    , orderItems);

                _orderRepository.Add(order);
                await _orderRepository.UnitOfWork.SaveEntitiesAsync();

                _logger.LogInformation($"Order request saved for order ref: {context.Message.OrderRef}");

                var result = (await _orderRepository.FindByConditionAsync(or => or.OrderRef == context.Message.OrderRef)).Single();

                var customer = (await _customerRepository.FindByConditionAsync(cs => cs.Id == context.Message.CustomerId)).Single();

                await context.RespondAsync<IOrderCreatedEvent>(new OrderCreatedEvent
                {
                    OrderId = result.Id,
                    OrderRef = context.Message.OrderRef,
                    CustomerEmail = customer.EmailAddress,
                    CustomerName = customer.GetFullName
                });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Customer process failed for order ref: {context.Message.OrderRef} with error: {ErrorUtility.BuildExceptionDetail(ex)}");

                await context.RespondAsync<IOrderProcessingFailedEvent>(new OrderProcessingFailedEvent
                {
                    OrderRef = context.Message.OrderRef,
                    ErrorMessage = ErrorUtility.BuildExceptionDetail(ex)
                });
            }
        }
    }
}
