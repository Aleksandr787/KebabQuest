using KebabQuest.Data.DataContext;
using KebabQuest.Data.Dto;
using KebabQuest.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;

namespace KebabQuest.Data.Repositories
{
    public class GameSampleRepository : BaseRepository<GameRoomSample>
    {
        private MongoDataContext _dataContext;

        public GameSampleRepository(MongoDataContext context) : base(context, "gameRoomSamples")
        {
            _dataContext = context;
        }

        public void CleanUpDocument()
        {
            _collection.DeleteMany(new BsonDocument());
        }
    }
}
