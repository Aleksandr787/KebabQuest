using KebabQuest.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KebabQuest.Services.Interfaces
{
    public interface IScreenCastService
    {
        Task<ICollection<QuestStep>> GetScreenCastCollection();
    }
}
