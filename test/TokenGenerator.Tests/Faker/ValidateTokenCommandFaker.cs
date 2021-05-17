using Bogus;
using TokenGenerator.Domain.Command.ValidateToken;

namespace TokenGenerator.Tests.Faker
{
    public static class ValidateTokenCommandFaker
    {
        public static Faker<ValidateTokenCommand> ValidateTokenCommand() =>
           new Faker<ValidateTokenCommand>()
               .Rules((x, y) =>
               {
                   y.CardId = x.Random.Guid();
                   y.Token = x.Random.Guid();
                   y.CustomerId = x.Random.Int(1);
                   y.Cvv = int.Parse(x.Finance.CreditCardCvv());
               });
    }
}
