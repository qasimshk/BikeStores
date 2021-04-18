﻿using bs.component.integrations.Common;
using bs.component.integrations.Payments;
using bs.component.sharedkernal.Utility;
using bs.order.domain.Entities;
using bs.order.domain.Enums;
using bs.order.domain.Repositories;
using bs.order.service.Events;
using MassTransit;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace bs.order.service.Consumers
{
    public class PaymentConsumer : IConsumer<IPaymentProcessEvent>
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly ILogger<PaymentConsumer> _logger;

        public PaymentConsumer(IPaymentRepository paymentRepository, ILogger<PaymentConsumer> logger)
        {
            _paymentRepository = paymentRepository;
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<IPaymentProcessEvent> context)
        {
            try
            {
                _logger.LogInformation($"Payment request received for order ref: {context.Message.CorrelationId}");

                // TODO: Transaction Reference will be generated by the third party payment provider.
                var transactionRef = Guid.NewGuid();

                var payment = new Payment(context.Message.CustomerId
                    , context.Message.Amount
                    , (PaymentType)context.Message.PaymentType
                    , transactionRef
                    , context.Message.CardDetailsId);

                if (context.Message.PaymentType == (int)PaymentType.Card)
                {
                    if (context.Message.CardDetail.CardHolderName.Contains("Test"))
                    {
                        payment.MarkTransactionAsDeclined();

                        _logger.LogError($"Payment request failed for order ref: {context.Message.CorrelationId}");
                    }
                    else
                    {
                        payment.MarkTransactionSuccessful();

                        _logger.LogInformation($"Payment request is successful for order ref: {context.Message.CorrelationId}");
                    }
                }
                else
                {
                    payment.MarkTransactionSuccessful();

                    _logger.LogInformation($"Payment request is successful for order ref: {context.Message.CorrelationId}");
                }

                _paymentRepository.Add(payment);
                await _paymentRepository.UnitOfWork.SaveEntitiesAsync();

                _logger.LogInformation($"Payment request successfully stored for order ref: {context.Message.CorrelationId}");

                var result = (await _paymentRepository.FindByConditionAsync(p => p.PaymentRef == transactionRef)).Single();

                if (result.Status == TransactionStatus.Successful)
                {
                    await context.RespondAsync<IPaymentCreatedEvent>(new PaymentCreatedEvent
                    {
                        CorrelationId = context.Message.CorrelationId,
                        TransactionRef = result.PaymentRef,
                        PaymentId = result.Id
                    });
                }
                else
                {
                    await context.RespondAsync<IPaymentFailedEvent>(new PaymentFailedEvent
                    {
                        CorrelationId = context.Message.CorrelationId,
                        TransactionRef = result.PaymentRef,
                        PaymentId = result.Id,
                        ErrorMessage = "Payment failed, card holder contains test"
                    });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Payment process failed for order ref: {context.Message.CorrelationId} with error: {ErrorUtility.BuildExceptionDetail(ex)}");

                await context.RespondAsync<IOrderProcessingFailedEvent>(new OrderProcessingFailedEvent
                {
                    OrderRef = context.Message.CorrelationId,
                    ErrorMessage = ErrorUtility.BuildExceptionDetail(ex)
                });
            }
        }
    }
}
