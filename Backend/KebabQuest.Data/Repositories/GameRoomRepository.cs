using KebabQuest.Data.DataContext;
using KebabQuest.Data.Models;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KebabQuest.Data.Repositories
{
    public class GameRoomRepository : BaseRepository<GameRoom>
    {
        public GameRoomRepository(MongoDataContext context) : base(context, "gamerooms")
        { }

        public async Task<string> CreateGameRoom(GameRoom newRoom)
        {
            string id = ObjectId.GenerateNewId().ToString();
            newRoom.Id = id;
            await AddEntity(newRoom);
            return id;
        }
    }
}
