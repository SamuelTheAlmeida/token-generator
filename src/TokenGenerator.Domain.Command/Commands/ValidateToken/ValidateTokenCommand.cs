using System;

namespace TokenGenerator.Domain.Command.ValidateToken
{
    public class ValidateTokenCommand
    {
        public int CustomerId { get; set; }
        public Guid CardId { get; set; }
        public Guid Token { get; set; }
        public int Cvv { get; set; }
    }
}
