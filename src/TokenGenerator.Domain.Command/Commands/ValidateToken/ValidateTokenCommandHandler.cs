﻿using AutoMapper;
using System.Threading.Tasks;
using TokenGenerator.Domain.Command.Commands.ValidateToken;
using TokenGenerator.Domain.Command.CreateToken;
using TokenGenerator.Domain.Command.Extensions;
using TokenGenerator.Domain.Command.Interfaces.CommandHandler;
using TokenGenerator.Domain.Command.Interfaces.Repositories;

namespace TokenGenerator.Domain.Command.ValidateToken
{
    public class ValidateTokenCommandHandler : IValidateTokenCommandHandler
    {
        private readonly ICreateTokenCommandHandler _createTokenCommandHandler;
        private readonly ICardRepository _cardRepository;
        private readonly IMapper _mapper;

        public ValidateTokenCommandHandler(
            ICreateTokenCommandHandler createTokenCommandHandler,
            ICardRepository cardRepository,
            IMapper mapper
            )
        {
            _createTokenCommandHandler = createTokenCommandHandler;
            _cardRepository = cardRepository;
            _mapper = mapper;
        }

        public async Task<ValidateTokenCommandResponse> HandleAsync(ValidateTokenCommand command)
        {
            var card = await _cardRepository.FindOneAsync(x => x.CardId == command.CardId);
            var createTokenCommand = new CreateTokenCommand(
                card.CardNumber
                    .GetLastDigitsString(4)
                    .StringToIntList(),
                command.Cvv);

            var token = await _createTokenCommandHandler.HandleAsync(createTokenCommand);

            return new ValidateTokenCommandResponse(token == command.Token);

        }
    }
}
