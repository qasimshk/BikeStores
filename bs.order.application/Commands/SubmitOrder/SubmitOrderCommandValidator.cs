using FluentValidation;
using System;
using System.Linq;

namespace bs.order.application.Commands.SubmitOrder
{
    public class SubmitOrderCommandValidator : AbstractValidator<SubmitOrderCommand>
    {
        public SubmitOrderCommandValidator()
        {
            // Customer
            RuleFor(x => x.Customer.FirstName)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Please provide customer first name")
                .Length(2, 20).WithMessage("Customer first name length is invalid - {TotalLength}")
                .Must(MustBeValidName).WithMessage("Customer first name contains invalid characters");

            RuleFor(x => x.Customer.LastName)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Please provide customer last name")
                .Length(2, 20).WithMessage("Customer last name length is invalid - {TotalLength}")
                .Must(MustBeValidName).WithMessage("Customer last name contains invalid characters");

            RuleFor(x => x.Customer.DateOfBirth)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Please provide valid customer date of birth")
                .Must(MustBeValidAge).WithMessage("Customer date of birth is invalid");

            RuleFor(x => x.Customer.PhoneNumber)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Please provide customer phone number");

            RuleFor(x => x.Customer.EmailAddress)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Please provide customer email address")
                .MaximumLength(50).NotEmpty()
                .EmailAddress();

            RuleFor(x => x.Customer.BillingAddress.City)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Please provide customer billing address city")
                .Length(2, 20).WithMessage("Customer billing address city length is invalid - {TotalLength}")
                .Must(MustBeValidName).WithMessage("Customer billing address city contains invalid characters");

            RuleFor(x => x.Customer.BillingAddress.Country)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Please provide customer billing address country")
                .Length(2, 20).WithMessage("Customer billing address country length is invalid - {TotalLength}")
                .Must(MustBeValidName).WithMessage("Customer billing address country contains invalid characters");

            RuleFor(x => x.Customer.BillingAddress.PostCode)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Please provide customer billing address postcode")
                .Length(2, 20).WithMessage("Customer billing address postcode length is invalid - {TotalLength}");

            RuleFor(x => x.Customer.BillingAddress.Street)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Please provide customer billing address street")
                .Length(2, 20).WithMessage("Customer billing address street length is invalid - {TotalLength}")
                .Must(MustBeValidName).WithMessage("Customer billing address street contains invalid characters");

            // Card Details - Optional
            When(x => x.Customer.CardDetails is not null, () =>
            {
                RuleFor(x => x.Customer.CardDetails.CardHolderName)
                    .Cascade(CascadeMode.Stop)
                    .NotEmpty().WithMessage("Please provide card holder name")
                    .Must(MustBeValidName).WithMessage("Card holder name contains invalid characters"); 

                RuleFor(x => x.Customer.CardDetails.Expiration)
                    .Cascade(CascadeMode.Stop)
                    .NotEmpty().WithMessage("Please provide card expiration date")
                    .Must(MustBeValidCard).WithMessage("Card expiration date contains invalid expiry date");

                RuleFor(x => x.Customer.CardDetails.SecurityNumber)
                    .Cascade(CascadeMode.Stop)
                    .NotEmpty().WithMessage("Please provide card security number");

                RuleFor(x => x.Customer.CardDetails.CardNumber)
                    .Cascade(CascadeMode.Stop)
                    .CreditCard()
                    .NotEmpty().WithMessage("Please provide card number");
            });

            // Payment
            RuleFor(x => x.Payment.PaymentType)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Please provide payment type");
            
            // Basket
            RuleFor(x => x.BasketRef)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Please provide basket reference");
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
