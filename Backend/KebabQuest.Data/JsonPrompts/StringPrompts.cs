using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace KebabQuest.Data.JsonPrompts
{
    public static class StringPrompts
    {
        private static readonly JObject JsonObject = new()
        {
            { "newStoryLine", "Твой ответ заполненный этот JSON объект на русском" },
            { "newQuestion", "Сгенерируй новый вопрос для игры и верни мне в виде данного джсон объекта" },
            { "initialImage", "Обложка для квест игры" },
            { "imagePerStep", "Рисуй данную ситуацию для игры" }
        };

        public static string GetPrompts => JsonObject.ToString();
    }
}
