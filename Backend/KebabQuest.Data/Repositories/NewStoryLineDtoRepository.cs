using KebabQuest.Data.DataContext;
using KebabQuest.Data.Dto;
using KebabQuest.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KebabQuest.Data.Repositories
{
    public class NewStoryLineDtoRepository : BaseRepository<NewStoryLineJsonDto>
    {
        public NewStoryLineDtoRepository(MongoDataContext context) : base(context, "new-story-line")
        { }


    }
}
