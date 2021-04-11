using bs.component.sharedkernal.Exceptions;
using bs.inventory.domain.Models;
using bs.inventory.domain.Respositories;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace bs.inventory.infrastructure.Persistence.Queries.GetBasketValidated
{
    public class GetBasketValidatedQueryHandler : IRequestHandler<GetBasketValidatedQuery, BasketValidateResultDto>
    {
        private readonly ILogger<GetBasketValidatedQueryHandler> _logger;
        private readonly IBasketRepository _basketRepository;
        private readonly IProductRepository _productRepository;

        private BasketValidateResultDto basketValidateResult;
        private const string InStock = "In Stock";
        private const string OutStock = "Out Of Stock";

        public GetBasketValidatedQueryHandler(ILogger<GetBasketValidatedQueryHandler> logger, IBasketRepository basketRepository, IProductRepository productRepository)
        {
            _logger = logger;
            _basketRepository = basketRepository;
            _productRepository = productRepository;
            basketValidateResult = new BasketValidateResultDto();
        }

        public async Task<BasketValidateResultDto> Handle(GetBasketValidatedQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"About to get basket with basket ref {request.BasketRef}");
            var basket = await _basketRepository.FindByConditionAsync(b => b.BasketRef == request.BasketRef);

            if (!basket.Any()) throw new NotFoundException("Basket does not exist with this reference");

            basketValidateResult.BasketRef = request.BasketRef;
            basketValidateResult.Total = basket.Single().GetTotal.RoundPrice();

            _logger.LogInformation($"basket total price is {basket.Single().GetTotal} for basket ref {request.BasketRef}");

            foreach (var item in basket.Single().BasketItems)
            {
                var product = (await _productRepository.FindByConditionAsync(p => p.Id == item.GetProductId)).Single();

                var basketItem = new BasketItemsDto
                {
                    ProductRef = product.ProductRef,
                    ProductName = product.Name,
                    Quantity = item.Quantity,
                    Price = product.ListPrice.RoundPrice(),
                    Status = item.Quantity > product.GetStock ? OutStock : InStock
                };
                    
                basketValidateResult.BasketItemsResults.Add(basketItem);
            }

            _logger.LogInformation($"Basket contains {basketValidateResult.BasketItemsResults.Count} items for basket ref {request.BasketRef}");

            return basketValidateResult;
        }
    }

    public static class RoundPriceExtension
    {
        public static double RoundPrice(this double price)
        {
            return Math.Round(price, 2);
        }
    }
}
