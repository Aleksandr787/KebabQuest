using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KebabQuest.Data.Dto;
using KebabQuest.Data.Models;

namespace KebabQuest.Services.Interfaces
{
    public interface IGameService
    {
        Task<NewGameDto> GenerateNewGameRoom(string userId);
        Task<ICollection<GameRoom>?> GetAllGameRooms(string userId);
        Task RemoveRoom(string userId, string gameRoomId);
        Task<QuestStep> DoStep(string roomId, QuestStep step);
    }
}
