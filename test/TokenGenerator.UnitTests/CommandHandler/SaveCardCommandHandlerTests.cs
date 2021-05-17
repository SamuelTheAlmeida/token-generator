using Moq;
using TokenGenerator.Domain.Command.Interfaces.CommandHandler;
using TokenGenerator.Domain.Command.Interfaces.Repositories;

namespace TokenGenerator.UnitTests.CommandHandler
{
    [TestFixture]
    public class SaveCardCommandHandlerTests
    {
        private readonly IMock<ICardRepository> _cardRepository;
        private readonly IMock<ICreateTokenCommandHandler> _createTokenCommandHandler;
        public SaveCardCommandHandlerTests()
        {
            _cardRepository = new Mock<ICardRepository>();
            _createTokenCommandHandler = new Mock<ICreateTokenCommandHandler>();
        }

        [Test]
        public async Task Is
    }
}
