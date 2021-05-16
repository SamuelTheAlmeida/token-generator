using Bogus;
using TokenGenerator.Domain.Command.CreateToken;
using TokenGenerator.Domain.Command.Extensions;

namespace TokenGenerator.Tests.Faker
{
    public static class CreateTokenCommandFaker
    {
        public static Faker<CreateTokenCommand> CreateTokenCommand() =>
           new Faker<CreateTokenCommand>()
               .Rules((x, y) =>
               {
                   y.CardLastFourDigits = x.Random
                    .Long(1, 9999)
                    .GetLastDigitsString(4)
                    .StringToIntList();

                   y.Cvv = int.Parse(x.Finance.CreditCardCvv());
               });
    }
}
