using FluentValidation;

namespace bs.inventory.application.Commands.AddBasket
{
    public class AddBasketCommandValidator : AbstractValidator<AddBasketCommand>
    {
        public AddBasketCommandValidator()
        {
            RuleFor(x => x.BasketRef)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Please provide basket reference");

            RuleFor(x => x.BasketItem.Quantity)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Please provide product quantity");

            RuleFor(x => x.BasketItem.ProductId)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Please provide product id");
        }
    }
}
