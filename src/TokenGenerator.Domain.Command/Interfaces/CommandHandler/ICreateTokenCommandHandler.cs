using System;
using System.Threading.Tasks;
using TokenGenerator.Domain.Command.CreateToken;

namespace TokenGenerator.Domain.Command.Interfaces.CommandHandler
{
    public interface ICreateTokenCommandHandler
    {
        Task<Guid> HandleAsync(CreateTokenCommand command);
    }
}
