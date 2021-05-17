using Bogus;
using TokenGenerator.Domain.Command.Commands.SaveCard;

namespace TokenGenerator.UnitTests.Faker
{
    public static class SaveCardCommandFaker
    {
        public static Faker<SaveCardCommand> Produto() =>
       new Faker<SaveCardCommand>()
           .Rules((x, y) =>
           {
               y.CardNumber = long.Parse(x.Finance.CreditCardNumber());
               y.CustomerId = x.Random.Int(1);
               y.Cvv = int.Parse(x.Finance.CreditCardCvv());
           });
    }
}
