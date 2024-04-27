using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KebabQuest.Data.JsonPrompts
{
    public static class NewStoryLine
    {
        public static string Json = @"
        {
          ""title"": """",
          ""plot"": """",
          ""gameColors"": """",
          ""mainPlayer"": {
            ""gender"": """",
            ""race"": """",
            ""appearance"": {
              ""hair"": """",
              ""clothes"": {
                ""top"": """",
                ""bottom"": """",
                ""accessories"": """"
              }
            }
          },
          ""question"": """",
          ""options"": {
            ""option1"": """",
            ""option2"": """",
            ""option3"": """"
          }
        }";
    }
}
