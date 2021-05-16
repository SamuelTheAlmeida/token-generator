using TokenGenerator.Domain.Command.Interfaces.Repositories;
using TokenGenerator.Domain.Models;
using TokenGenerator.Infrastructure.Data.Context;

namespace TokenGenerator.Infrastructure.Data.Repositories
{
    public class CardRepository : BaseRepository<Card>, ICardRepository
    {
        public CardRepository(DatabaseContext context) : base(context)
        {

        }
    }
}
