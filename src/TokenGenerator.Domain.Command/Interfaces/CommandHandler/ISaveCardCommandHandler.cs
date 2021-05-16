using System.Threading.Tasks;
using TokenGenerator.Domain.Command.Commands.SaveCard;
using TokenGenerator.Domain.Command.Result;

namespace TokenGenerator.Domain.Command.Interfaces.CommandHandler
{
    public interface ISaveCardCommandHandler
    {
        Task<ApplicationResult<SaveCardCommandResponse>> HandleAsync(SaveCardCommand command);
    }
}
