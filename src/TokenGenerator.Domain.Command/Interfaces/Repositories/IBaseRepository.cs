using System.Threading.Tasks;

namespace TokenGenerator.Domain.Command.Interfaces.Repositories
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        Task<TEntity> FindOneAsync(System.Linq.Expressions.Expression<System.Func<TEntity, bool>> expressionWhere);
        Task InsertAsync(TEntity entity);
    }
}
