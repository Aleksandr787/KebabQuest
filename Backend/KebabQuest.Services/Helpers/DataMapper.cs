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

        public static NewGameDto MapToNewGameDto(GameRoomSample gameRoomSample)
        {
            return new NewGameDto
            {
                Id = gameRoomSample.Id,
                Image = gameRoomSample.Image,
                Plot = gameRoomSample.Plot,
                Title = gameRoomSample.Title
            };
        }

        public static GameRoomSample MapToGameSample(NewStoryLineJsonDto newStoryLineJsonDto)
        {
            return new GameRoomSample
            {
                Plot = newStoryLineJsonDto.Plot,
                Title = newStoryLineJsonDto.Title,
                GameColors = newStoryLineJsonDto.GameColors,
                MainPlayer = newStoryLineJsonDto.MainPlayer
            };
        }
    }
}
