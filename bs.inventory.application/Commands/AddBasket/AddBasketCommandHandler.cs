using bs.component.sharedkernal.Exceptions;
using bs.inventory.domain.Entities;
using bs.inventory.domain.Respositories;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace bs.inventory.application.Commands.AddBasket
{
    public class AddBasketCommandHandler : IRequestHandler<AddBasketCommand>
    {
        private readonly ILogger<AddBasketCommandHandler> _logger;
        private readonly IBasketRepository _basketRepository;
        private readonly IProductRepository _productRepository;

        public AddBasketCommandHandler(ILogger<AddBasketCommandHandler> logger, IBasketRepository basketRepository, IProductRepository productRepository)
        {
            _logger = logger;
            _basketRepository = basketRepository;
            _productRepository = productRepository;
        }

        /// <summary>
        /// Only one product can be added to the basket at one time. To add more products to the basket pass the product reference and quantity with the same basket ref.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<Unit> Handle(AddBasketCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"About to retrieve product with product ref: {request.BasketItem.ProductRef} with basket ref: {request.BasketRef}");

            var product = await _productRepository.FindByConditionAsync(p => p.ProductRef == request.BasketItem.ProductRef);
            

            if (!product.Any())
            {
                _logger.LogError($"Product not found with Id: {request.BasketItem.ProductRef} with basket ref: {request.BasketRef}");

                throw new BadRequestException("Requested product is not found");
            }

            if (request.BasketItem.Quantity > product.Single().GetStock)
            {
                _logger.LogError($"Product quantity excited the value in stock: {request.BasketItem.Quantity} with basket ref: {request.BasketRef}");

                throw new BadRequestException("Requested quantity is greater then product stock");
            }

            var basket = await _basketRepository.FindByConditionAsync(b => b.BasketRef == request.BasketRef);

            if (basket.Any())
            {
                basket.Single().AddBasketItem(product.Single().Id, request.BasketItem.Quantity, product.Single().ListPrice);

                _basketRepository.Update(basket.Single());

                _logger.LogInformation($"Basket ref: {request.BasketRef} is about to be updated");
            }
            else
            {
                _basketRepository.Add(new Basket(request.BasketRef, product.Single().Id, request.BasketItem.Quantity, product.Single().ListPrice));

                _logger.LogInformation($"Basket ref: {request.BasketRef} is about to be created");
            }

            await _basketRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
