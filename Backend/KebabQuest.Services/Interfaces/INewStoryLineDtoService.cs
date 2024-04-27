using KebabQuest.Data.Dto;
using KebabQuest.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KebabQuest.Services.Interfaces
{
    public interface INewStoryLineDtoService
    {
        Task<NewStoryLineJsonDto> GetById(string id);
        Task Generate();
    }
}
