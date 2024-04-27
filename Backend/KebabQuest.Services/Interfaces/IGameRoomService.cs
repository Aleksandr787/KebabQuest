using KebabQuest.Data.Models;
using KebabQuest.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KebabQuest.Services.Interfaces
{
    public interface IGameRoomService
    {
        Task<string> CreateGameRoom(GameRoom newRoom);
        Task DeleteGameRoom(string id);
        Task<GameRoom> GetById(string id);
        Task Update(string id, GameRoom gameRoom);
    }
}
