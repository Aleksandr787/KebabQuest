using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KebabQuest.Data.Dto
{
    public class StringPromptsDto
    {
        public string NewStoryLine { get; set; } = "";
        public string NewQuestion { get; set; } = "";
        public string InitialImage { get; set; } = "";
        public string ImagePerStep { get; set; } = "";
        public string InitialQuestion { get; set; } = "";
        public string ValidateAnswer { get; set; } = "";
    }
}
