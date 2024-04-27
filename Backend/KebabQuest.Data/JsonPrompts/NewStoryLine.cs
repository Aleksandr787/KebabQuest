using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace KebabQuest.Data.JsonPrompts
{
    public static class NewStoryLine
    {
        private static readonly JObject JsonObject = new()
        {
            { "title", "" },
            { "plot", "" },
            { "gameColors", "" },
            {
                "mainPlayer",
                new JObject
            {
              { "gender", "" },
              { "race", "" },
              {
                "appearance", new JObject
                {
                  { "hair", "" },
                  {
                    "clothes", new JObject
                    {
                      { "top", "" },
                      { "bottom", "" },
                      { "accessories", "" }
                    }
                  }
                }
              }
            }
            },
            { "question", "" },
            {
                "options",
                new JObject
            {
              { "option1", "" },
              { "option2", "" },
              { "option3", "" }
            }
            }
        };

        public static string JsonPrompt => JsonObject.ToString();
    }
}
