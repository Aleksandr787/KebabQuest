using KebabQuest.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KebabQuest.Services.Services
{
    public interface IUserService
    {
        Task<string> RegisterUserAsync();
        Task AddGameRoomId(string userToken, string gameRoomId);
        Task<ICollection<string>?> GetAllGameRoomsIds(string userToken);
        Task DeleteGameRoom(string userToken, string gameRoomId);
    }
}
