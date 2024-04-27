using KebabQuest.Data.DataContext;
using KebabQuest.Data.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KebabQuest.Data.Repositories
{
    public class UserRepository : BaseRepository<Player>
    {
        public UserRepository(MongoDataContext context) : base(context, "users")
        {}

        public async Task RegisterUserAsync(string userNewToken)
        {
            var playerEntity = new Player() { Id = userNewToken };
            await AddEntity(playerEntity);
        }

        public async Task<Player> GetById(string userToken)
        {
            return await GetById(userToken);
        }

        public async Task Update(string userToken, string gameRoomId)
        {
            await Update(userToken, gameRoomId); 
        }
    }
}
