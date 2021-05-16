using FluentValidation;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using TokenGenerator.Domain.Command.Commands.ValidateToken;
using TokenGenerator.Domain.Command.CreateToken;
using TokenGenerator.Domain.Command.Extensions;
using TokenGenerator.Domain.Command.Interfaces.CommandHandler;
using TokenGenerator.Domain.Command.Interfaces.Repositories;
using TokenGenerator.Domain.Command.Result;
using TokenGenerator.Domain.Enums;
using TokenGenerator.Domain.Models;

namespace TokenGenerator.Domain.Command.ValidateToken
{
    public class ValidateTokenCommandHandler : IValidateTokenCommandHandler
    {
        private readonly ICreateTokenCommandHandler _createTokenCommandHandler;
        private readonly ICardRepository _cardRepository;
        private readonly IValidator<ValidateTokenCommand> _validator;
        private readonly ILogger _logger;

        public ValidateTokenCommandHandler(
            ICreateTokenCommandHandler createTokenCommandHandler,
            ICardRepository cardRepository,
            IValidator<ValidateTokenCommand> validator,
            ILogger logger
            )
        {
            _createTokenCommandHandler = createTokenCommandHandler;
            _cardRepository = cardRepository;
            _validator = validator;
            _logger = logger;
        }

        public async Task<ApplicationResult<ValidateTokenCommandResponse>> HandleAsync(ValidateTokenCommand command)
        {
            // Validate the command
            _logger.LogInformation($"Processing token validation for Card: {command.CardId}");
            var validationResult = await _validator.ValidateAsync(command);
            if (!validationResult.IsValid)
            {
                return new ApplicationResult<ValidateTokenCommandResponse>(
                    success: false,
                    message: DefaultResults.ValidationErrors,
                    validationErrors: validationResult.Errors
                    );
            }

            // Find the card
            var card = await _cardRepository.FindOneAsync(x => x.CardId == command.CardId);
            if (card is null)
            {
                _logger.LogInformation($"Card not found in database");
                return new ApplicationResult<ValidateTokenCommandResponse>(
                    success: true,
                    message: DefaultResults.Success,
                    data: new ValidateTokenCommandResponse(false));
            }

            _logger.LogInformation($"Card found - CardNumber {card.CardNumber}");
            // Recreate the token
            var createTokenCommand = new CreateTokenCommand(
                card.CardNumber
                    .GetLastDigitsString(4)
                    .StringToIntList(),
                command.Cvv);

            var token = await _createTokenCommandHandler.HandleAsync(createTokenCommand);
            var data = new ValidateTokenCommandResponse(IsValid(token, command, card));

            _logger.LogInformation($"Finished token validation - Validated: {data.Validated}");
            return new ApplicationResult<ValidateTokenCommandResponse>(
                success: true,
                message: DefaultResults.Success,
                data: data);
        }

        /// <summary>
        /// Validates token data
        /// </summary>
        /// <param name="newToken">Regenerated token to compare</param>
        /// <param name="command">Command data</param>
        /// <param name="card">Card found in the database</param>
        /// <returns></returns>
        private bool IsValid(Guid newToken, ValidateTokenCommand command, Card card)
        {
            return
                newToken == command.Token &&
                card.CustomerId == command.CustomerId &&
                card.TokenCreationDate >= DateTime.Now.AddMinutes(-30);
        }
    }
}
