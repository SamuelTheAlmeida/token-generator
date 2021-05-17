using System.Collections.Generic;

namespace TokenGenerator.Domain.Command.CreateToken
{
    public class CreateTokenCommand
    {
        public List<int> CardLastFourDigits { get; set; }
        public int Cvv { get; set; }
        public CreateTokenCommand()
        {

        }

        public CreateTokenCommand(
            List<int> cardLastFourDigits, 
            int cvv
            )
        {
            CardLastFourDigits = cardLastFourDigits;
            Cvv = cvv;
        }
    }
}
