using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KebabQuest.Data.Models
{
    public class User : Base
    {
        public ICollection<string>? GameRoomIds { get; set; }
    }
}
