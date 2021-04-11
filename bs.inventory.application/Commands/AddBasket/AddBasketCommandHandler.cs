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

        public async Task<Unit> Handle(AddBasketCommand request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.FindByConditionAsync(p => p.Id == request.BasketItem.ProductId);

            if (!product.Any())
            {
                throw new BadRequestException("Requested product is not found");
            }

            if (request.BasketItem.Quantity > product.Single().InStock)
            {
                throw new BadRequestException("Requested quantity is greater then product stock");
            }

            var basket = await _basketRepository.FindByConditionAsync(b => b.BasketRef == request.BasketRef);

            if (basket.Any())
            {
                basket.Single().AddBasketItem(product.Single().Id, request.BasketItem.Quantity, product.Single().ListPrice);

                _basketRepository.Update(basket.Single());
            }
            else
            {
                _basketRepository.Add(new Basket(request.BasketRef, product.Single().Id, request.BasketItem.Quantity, product.Single().ListPrice));
            }

            await _basketRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
