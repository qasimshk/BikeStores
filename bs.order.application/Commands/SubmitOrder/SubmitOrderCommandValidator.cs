using FluentValidation;
using System;
using System.Linq;

namespace bs.order.application.Commands.SubmitOrder
{
    public class SubmitOrderCommandValidator : AbstractValidator<SubmitOrderCommand>
    {
        public SubmitOrderCommandValidator()
        {
            RuleFor(x => x.Request.CorrelationId)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Please provide customer {PropertyName}");

            // Customer
            RuleFor(x => x.Request.Customer.FirstName)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Please provide customer first name")
                .Length(2, 20).WithMessage("Customer first name length is invalid - {TotalLength}")
                .Must(MustBeValidName).WithMessage("Customer first name contains invalid characters");

            RuleFor(x => x.Request.Customer.LastName)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Please provide customer last name")
                .Length(2, 20).WithMessage("Customer last name length is invalid - {TotalLength}")
                .Must(MustBeValidName).WithMessage("Customer last name contains invalid characters");

            RuleFor(x => x.Request.Customer.DateOfBirth)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Please provide valid customer date of birth")
                .Must(MustBeValidAge).WithMessage("Customer date of birth is invalid");

            RuleFor(x => x.Request.Customer.PhoneNumber)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Please provide customer phone number");

            RuleFor(x => x.Request.Customer.EmailAddress)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Please provide customer email address")
                .MaximumLength(50).NotEmpty()
                .EmailAddress();

            RuleFor(x => x.Request.Customer.BillingAddress.City)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Please provide customer billing address city")
                .Length(2, 20).WithMessage("Customer billing address city length is invalid - {TotalLength}")
                .Must(MustBeValidName).WithMessage("Customer billing address city contains invalid characters");

            RuleFor(x => x.Request.Customer.BillingAddress.Country)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Please provide customer billing address country")
                .Length(2, 20).WithMessage("Customer billing address country length is invalid - {TotalLength}")
                .Must(MustBeValidName).WithMessage("Customer billing address country contains invalid characters");

            RuleFor(x => x.Request.Customer.BillingAddress.PostCode)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Please provide customer billing address postcode")
                .Length(2, 20).WithMessage("Customer billing address postcode length is invalid - {TotalLength}")
                .Must(MustBeValidName).WithMessage("Customer billing address postcode contains invalid characters");

            RuleFor(x => x.Request.Customer.BillingAddress.Street)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Please provide customer billing address street")
                .Length(2, 20).WithMessage("Customer billing address street length is invalid - {TotalLength}")
                .Must(MustBeValidName).WithMessage("Customer billing address street contains invalid characters");

            // Card Details - Optional
            When(x => x.Request.Customer.CardDetails is not null, () =>
            {
                RuleFor(x => x.Request.Customer.CardDetails.CardHolderName)
                    .Cascade(CascadeMode.Stop)
                    .NotEmpty().WithMessage("Please provide card holder name");

                RuleFor(x => x.Request.Customer.CardDetails.Expiration)
                    .Cascade(CascadeMode.Stop)
                    .NotEmpty().WithMessage("Please provide card expiration date")
                    .Must(MustBeValidCard).WithMessage("Card expiration date contains invalid expiry date");

                RuleFor(x => x.Request.Customer.CardDetails.SecurityNumber)
                    .Cascade(CascadeMode.Stop)
                    .NotEmpty().WithMessage("Please provide card security number");

                RuleFor(x => x.Request.Customer.CardDetails.CardNumber)
                    .Cascade(CascadeMode.Stop)
                    .CreditCard()
                    .NotEmpty().WithMessage("Please provide card number");
            });

            // Payment
            RuleFor(x => x.Request.PaymentRequest.PaymentRef)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Please provide payment reference");

            RuleFor(x => x.Request.PaymentRequest.Amount)
                .Cascade(CascadeMode.Stop)
                .GreaterThan(0)
                .NotEmpty().WithMessage("Please provide payment amount");

            RuleFor(x => x.Request.PaymentRequest.TransactionDate)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Please provide payment transaction date");

            RuleFor(x => x.Request.PaymentRequest.PaymentType)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Please provide payment type");

            RuleFor(x => x.Request.PaymentRequest.TransactionStatus)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Please provide payment transaction status");

            // Order
            RuleFor(x => x.Request.Order.OrderRef)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Please provide order reference");

            RuleFor(x => x.Request.Order.OrderStatus)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Please provide order status");
            
            RuleFor(x => x.Request.Order.DeliveryAddress.City)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Please provide order delivery address city")
                .Length(2, 20).WithMessage("Delivery address city length is invalid - {TotalLength}")
                .Must(MustBeValidName).WithMessage("Delivery address city contains invalid characters");

            RuleFor(x => x.Request.Order.DeliveryAddress.Country)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Please provide order delivery address country")
                .Length(2, 20).WithMessage("Delivery address country length is invalid - {TotalLength}")
                .Must(MustBeValidName).WithMessage("Delivery address country contains invalid characters");

            RuleFor(x => x.Request.Order.DeliveryAddress.PostCode)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Please provide order delivery address postcode")
                .Length(2, 20).WithMessage("Delivery address postcode length is invalid - {TotalLength}")
                .Must(MustBeValidName).WithMessage("Delivery address postcode contains invalid characters");

            RuleFor(x => x.Request.Order.DeliveryAddress.Street)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Please provide order delivery address street")
                .Length(2, 20).WithMessage("Delivery address street length is invalid - {TotalLength}")
                .Must(MustBeValidName).WithMessage("Delivery address street contains invalid characters");
            
            // Order Items
            RuleForEach(x => x.Request.Order.OrderItemDtos)
                .ChildRules(oi =>
                {
                    oi.RuleFor(x => x.Quantity)
                        .GreaterThan(0)
                        .NotEmpty()
                        .WithMessage("Please provide product quantity");

                    oi.RuleFor(x => x.ProductRef)
                        .Cascade(CascadeMode.Stop)
                        .NotEmpty().WithMessage("Please provide product reference");

                    oi.RuleFor(x => x.ProductName)
                        .Cascade(CascadeMode.Stop)
                        .NotEmpty().WithMessage("Please provide product name");

                    oi.RuleFor(x => x.Price)
                        .Cascade(CascadeMode.Stop)
                        .GreaterThan(0)
                        .NotEmpty().WithMessage("Please provide product price");
                })
                .NotEmpty().WithMessage("Please provide order items");
        }

        private bool MustBeValidName(string name)
        {
            name = name.Replace(" ", "");
            name = name.Replace("-", "");
            return name.All(char.IsLetter);
        }

        private bool MustBeValidAge(DateTime date)
        {
            var currentYear = DateTime.Now.Year;
            var dobYear = date.Year;

            return dobYear <= currentYear && dobYear > (currentYear - 120);
        }

        private bool MustBeValidCard(DateTime date)
        {
            return date > DateTime.Now.Date;
        }
    }
}
