using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KebabQuest.Data.Models
{
    public class GameRoomSample : Base
    {
        public string? Title { get; set; }
        public string? Plot { get; set; }
        public string? GameColors { get; set; }
        public MainPlayer? MainPlayer { get; set; }
        public string? Image { get; set; }
    }
}
