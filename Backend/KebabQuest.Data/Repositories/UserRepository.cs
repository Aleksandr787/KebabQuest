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
        public UserRepository(MongoDataContext context, string collection) : base(context, collection)
        {}

        public async Task RegisterUserAsync(string userNewToken)
        {
            var playerEntity = new Player() { Id = userNewToken };
            await AddEntity(playerEntity);
        }
    }
}
