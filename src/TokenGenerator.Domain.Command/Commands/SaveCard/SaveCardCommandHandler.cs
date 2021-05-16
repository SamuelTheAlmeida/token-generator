using AutoMapper;
using System.Threading.Tasks;
using TokenGenerator.Domain.Command.CreateToken;
using TokenGenerator.Domain.Command.Extensions;
using TokenGenerator.Domain.Command.Interfaces.CommandHandler;
using TokenGenerator.Domain.Command.Interfaces.Repositories;
using TokenGenerator.Domain.Models;

namespace TokenGenerator.Domain.Command.Commands.SaveCard
{
    public class SaveCardCommandHandler : ISaveCardCommandHandler
    {
        private readonly ICreateTokenCommandHandler _createTokenCommandHandler;
        private readonly ICardRepository _cardRepository;
        private readonly IMapper _mapper;

        public SaveCardCommandHandler(
            ICreateTokenCommandHandler createTokenCommandHandler,
            ICardRepository cardRepository,
            IMapper mapper)
        {
            _createTokenCommandHandler = createTokenCommandHandler;
            _cardRepository = cardRepository;
            _mapper = mapper;
        }

        public async Task<SaveCardCommandResponse> HandleAsync(SaveCardCommand command)
        {
            var createTokenCommand = new CreateTokenCommand(
                command.CardNumber
                    .GetLastDigitsString(4)
                    .StringToIntList(), 
                command.Cvv);

            var token = await _createTokenCommandHandler.HandleAsync(createTokenCommand);

            var card = new Card(command.CustomerId, command.CardNumber);
            await _cardRepository.InsertAsync(card);

            return new SaveCardCommandResponse(token, card.CardId);
        }
    }
}
