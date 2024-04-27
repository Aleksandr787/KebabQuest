using KebabQuest.Data.Dto;
using KebabQuest.Data.Repositories;
using KebabQuest.Services.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KebabQuest.Services.Interfaces
{
    public interface IGameSampleService
    {
        Task<ICollection<NewGameDto>> GetGameSamples();
        Task Generate();
    }
}
