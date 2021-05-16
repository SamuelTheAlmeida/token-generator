using NUnit.Framework;
using System;
using System.Threading.Tasks;
using TokenGenerator.Domain.Command.CreateToken;
using TokenGenerator.Domain.Command.Interfaces.CommandHandler;
using TokenGenerator.Domain.Command.Extensions;

namespace TokenGenerator.Tests.CommandHandler
{
    [TestFixture]
    public class CreateTokenCommandHandlerTests
    {
        private readonly ICreateTokenCommandHandler _createTokenCommandHandler;
        public CreateTokenCommandHandlerTests()
        {
            _createTokenCommandHandler = new CreateTokenCommandHandler();
        }

        [Test]
        public async Task Should_Generate_Token()
        {
            // arrange
            var createTokenCommand = Faker.CreateTokenCommandFaker.CreateTokenCommand().Generate();

            // act
            var result = await _createTokenCommandHandler.HandleAsync(createTokenCommand);

            // assert
            Assert.IsTrue(result != null);
            Assert.IsTrue(result != Guid.Empty);
        }

        [Test]
        public async Task Should_Return_Same_Token_If_Regenerated()
        {
            // arrange
            var createTokenCommand = new CreateTokenCommand(((long)12341234).
                    GetLastDigitsString(4)
                    .StringToIntList(), 999);
            var createTokenCommand2 = new CreateTokenCommand(((long)12341234).
                    GetLastDigitsString(4)
                    .StringToIntList(), 999);

            // act
            var token1 = await _createTokenCommandHandler.HandleAsync(createTokenCommand);
            var token2 = await _createTokenCommandHandler.HandleAsync(createTokenCommand2);

            // assert
            Assert.IsTrue(token1 != null);
            Assert.IsTrue(token1 != Guid.Empty);
            Assert.IsTrue(token2 != null);
            Assert.IsTrue(token2 != Guid.Empty);
            Assert.IsTrue(token1 == token2);
        }
    }
}
