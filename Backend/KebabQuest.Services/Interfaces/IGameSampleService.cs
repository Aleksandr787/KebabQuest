using KebabQuest.Data.Dto;
using KebabQuest.Data.Repositories;
using KebabQuest.Services.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KebabQuest.Data.Models;

namespace KebabQuest.Services.Interfaces
{
    public interface IGameSampleService
    {
        Task<GameRoomSample> GetById(string sampleId);
        Task<ICollection<NewGameDto>> GetGameSamples();
        Task SeedData();
    }
}
