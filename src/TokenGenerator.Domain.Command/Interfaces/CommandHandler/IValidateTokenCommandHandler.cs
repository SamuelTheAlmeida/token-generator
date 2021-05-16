using System.Threading.Tasks;
using TokenGenerator.Domain.Command.Commands.ValidateToken;
using TokenGenerator.Domain.Command.ValidateToken;

namespace TokenGenerator.Domain.Command.Interfaces.CommandHandler
{
    public interface IValidateTokenCommandHandler
    {
        Task<ValidateTokenCommandResponse> HandleAsync(ValidateTokenCommand command);
    }
}
