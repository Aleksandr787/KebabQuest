using KebabQuest.Data.Dto;
using KebabQuest.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KebabQuest.Services.Helpers
{
    public static class DataMapper
    {
        public static GameRoom MapToGameRoom(NewStoryLineJsonDto dto)
        {
            return new GameRoom
            {
                Title = dto.Title,
                Plot = dto.Plot,
                GameColors = dto.GameColors,
                MainPlayer = dto.MainPlayer,
            };
        }

        public static NewGameDto MapToNewGameDto(NewStoryLineJsonDto dto)
        {
            return new NewGameDto
            {
                Title = dto.Title,
                Plot = dto.Plot,
                Question = dto.Question,
                Options = dto.Options
            };
        }

        public static QuestStep MapToQuestStep(NewQuestionJsonDto questionDto, string image)
        {
            return new QuestStep
            {
                Question = questionDto.Question,
                Options = questionDto.Options,
                Image = image
            };
        }
    }
}
