using FluentValidation;

namespace TokenGenerator.Domain.Command.Commands.SaveCard
{
    public class SaveCardCommandValidator : AbstractValidator<SaveCardCommand>
    {
        public SaveCardCommandValidator()
        {
            RuleFor(c => c.CardNumber)
                .NotNull()
                .NotEmpty()
                .WithMessage("Card Number is required");

            RuleFor(c => c.CardNumber)
                .LessThanOrEqualTo(9999999999999999)
                .WithMessage("Card Number length is up to 16 characters");

            RuleFor(c => c.Cvv)
                .LessThanOrEqualTo(99999)
                .WithMessage("CVV length is up to 5 characters");

            RuleFor(c => c.CustomerId)
                .NotNull()
                .NotEmpty()
                .WithMessage("Customer ID is required");
        }
    }
}
