using FluentValidation;
using System.Threading.Tasks;
using TokenGenerator.Domain.Command.CreateToken;
using TokenGenerator.Domain.Command.Extensions;
using TokenGenerator.Domain.Command.Interfaces.CommandHandler;
using TokenGenerator.Domain.Command.Interfaces.Repositories;
using TokenGenerator.Domain.Command.Result;
using TokenGenerator.Domain.Enums;
using TokenGenerator.Domain.Models;

namespace TokenGenerator.Domain.Command.Commands.SaveCard
{
    public class SaveCardCommandHandler : ISaveCardCommandHandler
    {
        private readonly ICreateTokenCommandHandler _createTokenCommandHandler;
        private readonly ICardRepository _cardRepository;
        private readonly IValidator<SaveCardCommand> _validator;

        public SaveCardCommandHandler(
            ICreateTokenCommandHandler createTokenCommandHandler,
            ICardRepository cardRepository,
            IValidator<SaveCardCommand> validator)
        {
            _createTokenCommandHandler = createTokenCommandHandler;
            _cardRepository = cardRepository;
            _validator = validator;
        }

        public async Task<ApplicationResult<SaveCardCommandResponse>> HandleAsync(SaveCardCommand command)
        {
            // Validate the command
            var validationResult = await _validator.ValidateAsync(command);
            if (!validationResult.IsValid)
            {
                return new ApplicationResult<SaveCardCommandResponse>(
                    success: false,
                    message: DefaultResults.ValidationErrors,
                    validationErrors: validationResult.Errors
                    );
            }

            // Generate token
            var createTokenCommand = new CreateTokenCommand(
                command.CardNumber
                    .GetLastDigitsString(4)
                    .StringToIntList(), 
                command.Cvv);
            var token = await _createTokenCommandHandler.HandleAsync(createTokenCommand);

            // Insert card
            var card = new Card(command.CustomerId, command.CardNumber);
            await _cardRepository.InsertAsync(card);

            return new ApplicationResult<SaveCardCommandResponse>(
                success: true, 
                message: DefaultResults.Success, 
                data: new SaveCardCommandResponse(token, card.CardId)
                );
        }
    }
}
