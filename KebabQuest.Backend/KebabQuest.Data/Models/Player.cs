using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KebabQuest.Data.Models
{
    internal class Player : Base
    {
        public ICollection<string>? GameIds { get; set; }
    }
}
