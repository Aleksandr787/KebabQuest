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
        Task<NewGameDto> GetById(string gameRoomId);
        Task<NewGameDto> StartGameFromGameSample(string sampleId, string userId);
        Task<NewGameDto> GenerateNewGameRoom(string userId);
        Task<IList<NewGameDto>> GetAllGameRooms(string userId);
        Task RemoveRoom(string userId, string gameRoomId);
        Task<QuestStep> DoStep(string roomId, string answer);
    }
}
