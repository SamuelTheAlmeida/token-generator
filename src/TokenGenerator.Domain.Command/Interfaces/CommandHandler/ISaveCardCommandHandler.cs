using System.Threading.Tasks;
using TokenGenerator.Domain.Command.Commands.SaveCard;

namespace TokenGenerator.Domain.Command.Interfaces.CommandHandler
{
    public interface ISaveCardCommandHandler
    {
        Task<SaveCardCommandResponse> HandleAsync(SaveCardCommand command);
    }
}
