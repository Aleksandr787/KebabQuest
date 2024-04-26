using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KebabQuest.Data.Interfaces
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        Task<TEntity> GetById(string id);
        Task<List<TEntity>> SearchEntities(Func<TEntity, bool> predicate);
        Task<List<TEntity>> GetAll();
        Task<bool> DoesExist(string id);
        Task AddEntity(TEntity entity);
        Task UpdateEntity(string id, TEntity entity);
        Task Delete(string id);
    }
}
