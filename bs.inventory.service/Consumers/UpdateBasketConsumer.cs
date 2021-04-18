using System;
using System.Linq;
using bs.component.integrations.Basket;
using MassTransit;
using System.Threading.Tasks;
using bs.inventory.domain.Respositories;
using Microsoft.Extensions.Logging;
using bs.component.sharedkernal.Utility;
using bs.inventory.service.Events;
using bs.inventory.domain.Models;
using System.Collections.Generic;

namespace bs.inventory.service.Consumers
{
    public class UpdateBasketConsumer : IConsumer<IUpdateBasketEvent>
    {
        private readonly IBasketRepository _basketRepository;
        private readonly ILogger<UpdateBasketConsumer> _logger;
        private readonly IProductRepository _productRepository;

        public UpdateBasketConsumer(IBasketRepository basketRepository, ILogger<UpdateBasketConsumer> logger, IProductRepository productRepository)
        {
            _basketRepository = basketRepository;
            _logger = logger;
            _productRepository = productRepository;
        }

        public async Task Consume(ConsumeContext<IUpdateBasketEvent> context)
        {
            try
            {
                var basket = (await _basketRepository.FindByConditionAsync(b => b.BasketRef == context.Message.BasketRef)).Single();

                basket.AddBasketItem(
                    context.Message.ProductId
                    , context.Message.Quantity
                    , context.Message.ProductPrice);

                _basketRepository.Update(basket);

                _logger.LogInformation($"Basket ref: {context.Message.BasketRef} is about to update");

                await _basketRepository.UnitOfWork.SaveEntitiesAsync();

                var basketResult = (await _basketRepository.FindByConditionAsync(bk => bk.BasketRef == context.Message.BasketRef)).Single();

                var basketItems = new List<JsonBasketItem>();

                foreach (var item in basketResult.BasketItems)
                {
                    var product = (await _productRepository.FindByConditionAsync(p => p.Id == item.GetProductId)).Single();

                    basketItems.Add(new JsonBasketItem
                    {
                        Quantity = item.Quantity,
                        Price = item.Amount,
                        ProductName = product.Name,
                        ProductRef = product.ProductRef
                    });
                }

                await context.RespondAsync<IBasketPlacedSuccessfullyEvent>(
                    new BasketPlacedSuccessfullyEvent(context.Message.BasketRef, basketResult.GetTotal, basketItems));

                _logger.LogInformation($"Basket ref: {context.Message.BasketRef} is updated & notified");
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Basket ref: {context.Message.BasketRef} process failed with error {ErrorUtility.BuildExceptionDetail(ex)}");

                await context.RespondAsync<IBasketProcessFailedEvent>(
                    new BasketProcessFailedEvent(context.Message.BasketRef
                        , ErrorUtility.BuildExceptionDetail(ex)));
            }
        }
    }
}
