using KebabQuest.Data.Interfaces;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using KebabQuest.Data.DataContext;

namespace KebabQuest.Data.Repositories
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        private readonly IMongoCollection<TEntity> _collection;
        private readonly MongoDataContext _context;

        public BaseRepository(MongoDataContext context, string collection)
        {
            _context = context;
            _collection = _context.GetCollection<TEntity>(collection);
        }

        public async Task AddEntity(TEntity entity)
        {
            await _collection.InsertOneAsync(entity);
        }

        public async Task Delete(string id)
        {
            await _collection.DeleteOneAsync(GetObjectId(id));
        }

        public async Task<bool> DoesExist(string id)
        {
            var exists = await GetById(id);
            return exists != null;
        }

        public async Task<List<TEntity>> GetAll()
        {
            var allEntities = await _collection.FindAsync(Builders<TEntity>.Filter.Empty);
            return await allEntities.ToListAsync();
        }

        public async Task<TEntity> GetById(string id)
        {
            return await _collection.FindAsync(GetObjectId(id)).Result.FirstOrDefaultAsync();
        }

        public async Task<List<TEntity>> SearchEntities(Func<TEntity, bool> predicate)
        {
            Expression<Func<TEntity, bool>> expression = x => predicate(x);
            return await _collection.FindAsync(expression).Result.ToListAsync();
        }

        public async Task UpdateEntity(string id, TEntity entity)
        {
            await _collection.ReplaceOneAsync(GetObjectId(id), entity);
        }

        private FilterDefinition<TEntity> GetObjectId(string id)
        {
            var objectId = new ObjectId(id);
            return Builders<TEntity>.Filter.Eq("_id", objectId);
        }
    }
}
