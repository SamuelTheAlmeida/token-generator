using FluentValidation;
using TokenGenerator.Domain.Command.ValidateToken;

namespace TokenGenerator.Domain.Command.Commands.ValidateToken
{
    public class ValidateTokenCommandValidator : AbstractValidator<ValidateTokenCommand>
    {
        public ValidateTokenCommandValidator()
        {
            RuleFor(c => c.CardId)
                .NotNull()
                .NotEmpty()
                .WithMessage("Card ID is required");

            RuleFor(c => c.CustomerId)
                .NotNull()
                .NotEmpty()
                .WithMessage("Customer ID is required");

            RuleFor(c => c.Cvv)
                .LessThanOrEqualTo(99999)
                .WithMessage("CVV length is up to 5 characters");

            RuleFor(c => c.Token)
                .NotNull()
                .NotEmpty()
                .WithMessage("Token is required");
        }
    }
}
