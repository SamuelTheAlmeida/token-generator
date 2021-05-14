namespace TokenGenerator.Domain.Command.CreateToken
{
    public class CreateTokenCommand
    {
        public int CustomerId { get; set; }
        public long CardNumber { get; set; }
        public int Cvv { get; set; }
    }
}
