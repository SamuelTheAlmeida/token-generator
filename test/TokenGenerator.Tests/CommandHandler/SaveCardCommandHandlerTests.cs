using Moq;
using NUnit.Framework;
using System;
using System.Threading.Tasks;
using TokenGenerator.Domain.Command.Commands.SaveCard;
using TokenGenerator.Domain.Command.CreateToken;
using TokenGenerator.Domain.Command.Interfaces.CommandHandler;
using TokenGenerator.Domain.Command.Interfaces.Repositories;

namespace TokenGenerator.Tests.CommandHandler
{
    [TestFixture]
    public class SaveCardCommandHandlerTests
    {
        private readonly Mock<ICardRepository> _cardRepository;
        private readonly Mock<ICreateTokenCommandHandler> _createTokenCommandHandler;
        private readonly ISaveCardCommandHandler _saveCardCommandHandler;
        public SaveCardCommandHandlerTests()
        {
            _cardRepository = new Mock<ICardRepository>();
            _createTokenCommandHandler = new Mock<ICreateTokenCommandHandler>();
            _saveCardCommandHandler = new SaveCardCommandHandler(
                _createTokenCommandHandler.Object, 
                _cardRepository.Object,
                new SaveCardCommandValidator());
        }

        [Test]
        public async Task Should_Save_Card_And_Return_Token()
        {
            // arrange
            //_createTokenCommandHandler.Setup(p => p.Charge(It.IsAny<double>(), cardMock.Object)).Returns(false);
            var saveCardCommand = Faker.SaveCardCommandFaker.SaveCardCommand().Generate();
            _createTokenCommandHandler.Setup(x => 
                x.HandleAsync(It.IsAny<CreateTokenCommand>()))
                    .Returns(Task.FromResult(Guid.NewGuid()));

            // act
            var result = await _saveCardCommandHandler.HandleAsync(saveCardCommand);


            // assert
            //shipmentServiceMock.Verify(s => s.Ship(addressInfoMock.Object, items.AsEnumerable()), Times.Never());
            Assert.IsTrue(result.Success);
            Assert.IsTrue(result.ValidationErrors.Count == 0);
            Assert.IsTrue(result.Data.CardId != null && result.Data.CardId != Guid.Empty);
            Assert.IsTrue(result.Data.Token != null && result.Data.Token != Guid.Empty);
            Assert.IsTrue(result.Data.CreationDate != null);
        }

        [Test]
        public async Task Should_Return_Validation_Errors_If_Invalid()
        {
            // Arrange
            var saveCardCommand = new SaveCardCommand();

            // Act
            var result = await _saveCardCommandHandler.HandleAsync(saveCardCommand);

            // Assert
            Assert.IsFalse(result.Success);
            Assert.IsTrue(result.ValidationErrors.Count > 0);
            Assert.IsTrue(result.Data == null);
        }

    }
}
