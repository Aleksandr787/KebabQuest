using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KebabQuest.Data.Models
{
    public class Game : Base
    {
        public string? Title { get; set; }
        public string? Plot { get; set; }
        public string? GameColors { get; set; }
        public MainPlayer? MainPlayer { get; set; }
        public ICollection<QuestStep>? Steps { get; set; }
    }

    public class QuestStep
    {
        public string? Question { get; set; }
        public Options? Options { get; set; }
        public string? Answer { get; set; }
        public string? Image { get; set; }
    }

    public class MainPlayer
    {
        public string Gender { get; set; } = "";
        public string Race { get; set; } = "";
        public MainPlayerAppearance Appearance { get; set; } = null!;
    }

    public class MainPlayerAppearance
    {
        public string Hair { get; set; } = "";
        public MainPlayerClothes Clothes { get; set; } = null!;
    }

    public class MainPlayerClothes
    {
        public string Top { get; set; } = "";
        public string Bottom { get; set; } = "";
        public string Accessories { get; set; } = "";
    }

    public class Options
    {
        public string Option1 { get; set; } = "";
        public string Option2 { get; set; } = "";
        public string Option3 { get; set; } = "";
    }
}
