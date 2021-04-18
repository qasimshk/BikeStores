using System;
using System.Collections.Generic;
using System.Linq;
using bs.component.integrations.Basket;
using MassTransit;
using System.Threading.Tasks;
using bs.component.sharedkernal.Utility;
using Microsoft.Extensions.Logging;
using bs.inventory.domain.Respositories;
using bs.inventory.domain.Entities;
using bs.inventory.service.Events;
using bs.inventory.domain.Models;

namespace bs.inventory.service.Consumers
{
    public class CreateBasketConsumer : IConsumer<ICreateBasketEvent>
    {
        private readonly IBasketRepository _basketRepository;
        private readonly ILogger<CreateBasketConsumer> _logger;
        private readonly IProductRepository _productRepository;

        public CreateBasketConsumer(IBasketRepository basketRepository, ILogger<CreateBasketConsumer> logger, IProductRepository productRepository)
        {
            _basketRepository = basketRepository;
            _logger = logger;
            _productRepository = productRepository;
        }

        public async Task Consume(ConsumeContext<ICreateBasketEvent> context)
        {
            try
            {
                var basket = new Basket(
                    context.Message.BasketRef
                    , context.Message.ProductId
                    , context.Message.Quantity
                    , context.Message.ProductPrice);

                _basketRepository.Add(basket);

                _logger.LogInformation($"Basket ref: {context.Message.BasketRef} is about to create");

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

                _logger.LogInformation($"Basket ref: {context.Message.BasketRef} is created & notified");
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
