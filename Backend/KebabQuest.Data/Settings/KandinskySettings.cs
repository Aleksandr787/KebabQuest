using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KebabQuest.Data.Settings
{
    public class KandinskySettings
    {
        public string Url { get; init; } = "";
        public string ApiKey { get; set; } = "";
        public string SecretKey { get; set; } = "";
    }
}
