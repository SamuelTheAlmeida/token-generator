namespace TokenGenerator.Domain.Command.Commands.SaveCard
{
    public class SaveCardCommand
    {
        public int CustomerId { get; set; }
        public long CardNumber { get; set; }
        public int Cvv { get; set; }
    }
}
