using Bogus;
using Bogus.DataSets;
using TokenGenerator.Domain.Command.Commands.SaveCard;

namespace TokenGenerator.Tests.Faker
{
    public static class SaveCardCommandFaker
    {
        public static Faker<SaveCardCommand> SaveCardCommand() =>
           new Faker<SaveCardCommand>()
               .Rules((x, y) =>
               {
                   var creditCardNumber = x.Finance.CreditCardNumber(CardType.Mastercard)
                       .Replace(" ", "")
                       .Replace("-", "");
                   y.CardNumber = long.Parse(creditCardNumber);
                   y.CustomerId = x.Random.Int(1);
                   y.Cvv = int.Parse(x.Finance.CreditCardCvv());
               });
    }
}
