using System.Threading.Tasks;
using TokenGenerator.Domain.Command.Commands.ValidateToken;
using TokenGenerator.Domain.Command.Result;
using TokenGenerator.Domain.Command.ValidateToken;

namespace TokenGenerator.Domain.Command.Interfaces.CommandHandler
{
    public interface IValidateTokenCommandHandler
    {
        Task<ApplicationResult<ValidateTokenCommandResponse>> HandleAsync(ValidateTokenCommand command);
    }
}
