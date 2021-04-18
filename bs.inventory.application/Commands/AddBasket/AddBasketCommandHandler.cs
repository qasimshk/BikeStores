using bs.component.integrations.Basket;
using bs.component.sharedkernal.Exceptions;
using bs.inventory.application.Events;
using bs.inventory.domain.Respositories;
using MassTransit;
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
        private readonly IProductRepository _productRepository;
        private readonly IPublishEndpoint _publishEndpoint;

        public AddBasketCommandHandler(ILogger<AddBasketCommandHandler> logger, IProductRepository productRepository, IPublishEndpoint publishEndpoint)
        {
            _logger = logger;
            _productRepository = productRepository;
            _publishEndpoint = publishEndpoint;
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

            var result = await _productRepository.FindByConditionAsync(p => p.ProductRef == request.BasketItem.ProductRef);
            
            if (!result.Any())
            {
                _logger.LogError($"Product not found with Id: {request.BasketItem.ProductRef} with basket ref: {request.BasketRef}");

                throw new BadRequestException("Requested product is not found");
            }

            var product = result.Single();

            if (request.BasketItem.Quantity > product.GetStock)
            {
                _logger.LogError($"Product quantity excited the value in stock: {request.BasketItem.Quantity} with basket ref: {request.BasketRef}");

                throw new BadRequestException("Requested quantity is greater then product stock");
            }

            await _publishEndpoint.Publish<ISubmitBasketEvent>(new SubmitBasketEvent
            {
                ProductPrice = product.ListPrice,
                ProductId = product.Id,
                Quantity = request.BasketItem.Quantity,
                BasketRef = request.BasketRef
            }, cancellationToken);
            
            return Unit.Value;
        }
    }
}
