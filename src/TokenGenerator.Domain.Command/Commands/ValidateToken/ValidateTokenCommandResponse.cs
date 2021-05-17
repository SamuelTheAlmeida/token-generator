namespace TokenGenerator.Domain.Command.Commands.ValidateToken
{
    public class ValidateTokenCommandResponse
    {
        public bool Validated { get; set; }

        public ValidateTokenCommandResponse(bool validated)
        {
            Validated = validated;
        }
    }
}
