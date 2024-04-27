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
    public class UserRepository : BaseRepository<User>
    {
        public UserRepository(MongoDataContext context) : base(context, "users")
        { }

        public async Task RegisterUserAsync(string userNewToken)
        {
            var userEntity = new User() { Id = userNewToken };
            await AddEntity(userEntity);
        }
    }
}
