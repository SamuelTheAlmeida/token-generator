using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using System.Threading.Tasks;
using TokenGenerator.Domain.Command.Commands.ValidateToken;
using TokenGenerator.Domain.Command.CreateToken;
using TokenGenerator.Domain.Command.Interfaces.CommandHandler;
using TokenGenerator.Domain.Command.Interfaces.Repositories;
using TokenGenerator.Domain.Command.ValidateToken;
using TokenGenerator.Domain.Models;

namespace TokenGenerator.Tests.CommandHandler
{
    [TestFixture]
    public class ValidateTokenCommandHandlerTests
    {
        private readonly Mock<ICardRepository> _cardRepository;
        private readonly Mock<ICreateTokenCommandHandler> _createTokenCommandHandler;
        private readonly IValidateTokenCommandHandler _validateTokenCommandHandler;
        public ValidateTokenCommandHandlerTests()
        {
            _cardRepository = new Mock<ICardRepository>();
            _createTokenCommandHandler = new Mock<ICreateTokenCommandHandler>();

            var logger = new Mock<ILogger>();
            _validateTokenCommandHandler = new ValidateTokenCommandHandler(
                _createTokenCommandHandler.Object,
                _cardRepository.Object,
                new ValidateTokenCommandValidator(),
                logger.Object);
        }

        [Test]
        public async Task Should_Return_Not_Valid_If_Card_Not_Found()
        {
            // arrange
            var validateTokenCommand = Faker.ValidateTokenCommandFaker
                .ValidateTokenCommand()
                .Generate();

            _createTokenCommandHandler.Setup(x =>
                x.HandleAsync(It.IsAny<CreateTokenCommand>()))
                    .Returns(Task.FromResult(Guid.NewGuid()));

            _cardRepository.Setup(x =>
                x.FindOneAsync(y => y.CardId == validateTokenCommand.CardId))
                .Returns(Task.FromResult((Card)null));

            // act
            var result = await _validateTokenCommandHandler.HandleAsync(validateTokenCommand);

            // assert
            Assert.IsTrue(result.Success);
            Assert.IsTrue(result.ValidationErrors.Count == 0);
            Assert.IsTrue(result.Data != null);
            Assert.IsFalse(result.Data.Validated);
        }


        [Test]
        public async Task Should_Return_Not_Valid_If_Customer_Is_Not_The_Card_Owner()
        {
            // arrange
            var validateTokenCommand = Faker.ValidateTokenCommandFaker
                .ValidateTokenCommand()
                .Generate();
            validateTokenCommand.CustomerId = 1;

            _createTokenCommandHandler.Setup(x =>
                x.HandleAsync(It.IsAny<CreateTokenCommand>()))
                    .Returns(Task.FromResult(Guid.NewGuid()));

            var card = new Card(2, 99999999);
            _cardRepository.Setup(x =>
                x.FindOneAsync(y => y.CardId == validateTokenCommand.CardId))
                .Returns(Task.FromResult(card));

            // act
            var result = await _validateTokenCommandHandler.HandleAsync(validateTokenCommand);

            // assert
            Assert.IsTrue(result.Success);
            Assert.IsTrue(result.ValidationErrors.Count == 0);
            Assert.IsTrue(result.Data != null);
            Assert.IsFalse(result.Data.Validated);
        }


        [Test]
        public async Task Should_Return_Not_Valid_If_Token_Expired()
        {
            // arrange
            var validateTokenCommand = Faker.ValidateTokenCommandFaker
                .ValidateTokenCommand()
                .Generate();

            _createTokenCommandHandler.Setup(x =>
                x.HandleAsync(It.IsAny<CreateTokenCommand>()))
                    .Returns(Task.FromResult(Guid.NewGuid()));

            var card = new Card(validateTokenCommand.CustomerId, 99999999);
            card.TokenCreationDate = DateTime.Now.AddMinutes(-31);
            _cardRepository.Setup(x =>
                x.FindOneAsync(y => y.CardId == validateTokenCommand.CardId))
                .Returns(Task.FromResult(card));

            // act
            var result = await _validateTokenCommandHandler.HandleAsync(validateTokenCommand);

            // assert
            Assert.IsTrue(result.Success);
            Assert.IsTrue(result.ValidationErrors.Count == 0);
            Assert.IsTrue(result.Data != null);
            Assert.IsFalse(result.Data.Validated);
        }

        [Test]
        public async Task Should_Return_Not_Valid_If_Token_Doesnt_Match()
        {
            // arrange
            var validateTokenCommand = Faker.ValidateTokenCommandFaker
                .ValidateTokenCommand()
                .Generate();

            _createTokenCommandHandler.Setup(x =>
                x.HandleAsync(It.IsAny<CreateTokenCommand>()))
                    .Returns(Task.FromResult(Guid.NewGuid()));

            var card = new Card(validateTokenCommand.CustomerId, 99999999);
            _cardRepository.Setup(x =>
                x.FindOneAsync(y => y.CardId == validateTokenCommand.CardId))
                .Returns(Task.FromResult(card));

            // act
            var result = await _validateTokenCommandHandler.HandleAsync(validateTokenCommand);

            // assert
            Assert.IsTrue(result.Success);
            Assert.IsTrue(result.ValidationErrors.Count == 0);
            Assert.IsTrue(result.Data != null);
            Assert.IsFalse(result.Data.Validated);
        }

         [Test]
         public async Task Should_Return_Validation_Errors_If_Invalid()
         {
             // Arrange
             var validateTokenCommand = new ValidateTokenCommand();

             // Act
             var result = await _validateTokenCommandHandler.HandleAsync(validateTokenCommand);

             // Assert
             Assert.IsFalse(result.Success);
             Assert.IsTrue(result.ValidationErrors.Count > 0);
             Assert.IsTrue(result.Data == null);
         }
        
    }
}
