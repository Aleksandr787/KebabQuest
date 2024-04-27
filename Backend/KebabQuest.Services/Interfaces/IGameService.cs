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
        Task<NewGameDto> StartGameFromGameSample(string sampleId);
        Task<NewGameDto> GenerateNewGameRoom(string userId);
        Task<IList<NewGameDto>> GetAllGameRooms(string userId);
        Task RemoveRoom(string userId, string gameRoomId);
        Task<QuestStep> DoStep(string roomId, string answer);
    }
}
