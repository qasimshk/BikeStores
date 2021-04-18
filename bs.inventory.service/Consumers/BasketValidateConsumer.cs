using System.Linq;
using bs.component.integrations.Basket;
using bs.inventory.domain.Respositories;
using MassTransit;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using bs.inventory.service.Events;

namespace bs.inventory.service.Consumers
{
    public class BasketValidateConsumer : IConsumer<IBasketValidateEvent>
    {
        private readonly ILogger<BasketValidateConsumer> _logger;
        private readonly IBasketRepository _basketRepository;
        private readonly IProductRepository _productRepository;

        public BasketValidateConsumer(ILogger<BasketValidateConsumer> logger, IBasketRepository basketRepository, IProductRepository productRepository)
        {
            _logger = logger;
            _basketRepository = basketRepository;
            _productRepository = productRepository;
        }

        public async Task Consume(ConsumeContext<IBasketValidateEvent> context)
        {
            _logger.LogInformation($"About to get basket with basket ref {context.Message.BasketRef}");

            var basket = (await _basketRepository.FindByConditionAsync(b => b.BasketRef == context.Message.BasketRef)).Single();

            foreach (var item in basket.BasketItems)
            {
                var product = (await _productRepository.FindByConditionAsync(p => p.Id == item.GetProductId)).Single();

                if (item.Quantity > product.GetStock)
                {
                    await context.RespondAsync<IBasketItemsStockCheckResultEvent>(new BasketItemsStockCheckResultEvent
                    {
                        BasketRef = basket.BasketRef, 
                        InStock = false
                    });
                    _logger.LogError($"Product {product.Name} with reference {product.ProductRef} is out of stock in basket ref {context.Message.BasketRef}");
                    return;
                }
            }

            await context.RespondAsync<IBasketItemsStockCheckResultEvent>(new BasketItemsStockCheckResultEvent
            {
                BasketRef = basket.BasketRef,
                InStock = true
            });

            _logger.LogInformation($"All items are in stock of basket with basket ref {context.Message.BasketRef}");
        }
    }
}
