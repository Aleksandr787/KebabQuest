using KebabQuest.Data.Dto;
using KebabQuest.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace KebabQuest.Services.Helpers
{
    public static class DataMapper
    {
        public static GameRoom MapToGameRoom(NewStoryLineJsonDto dto, string image, NewQuestionJsonDto? questionDto = null)
        {
            return new()
            {
                Title = dto.Title,
                Plot = dto.Plot,
                GameColors = dto.GameColors,
                MainPlayer = dto.MainPlayer,
                Steps = new List<QuestStep>()
                {
                    new ()
                    {
                        Question = questionDto?.Question ?? dto.Question,
                        Options = questionDto?.Options ?? dto.Options,
                        Image = image
                    }
                }
            };
        }

        public static NewGameDto MapToNewGameDto(NewStoryLineJsonDto dto, string image)
        {
            return new()
            {
                Title = dto.Title,
                Plot = dto.Plot,
                Question = dto.Question,
                Options = dto.Options,
                Image = image
            };
        }
        
        public static NewGameDto MapToNewGameDto(GameRoom gameRoom, QuestStep lastQuestStep)
        {
            return new()
            {
                Id = gameRoom.Id,
                Title = gameRoom.Title,
                Plot = gameRoom.Plot,
                Question = lastQuestStep.Question,
                Options = lastQuestStep.Options,
                Image = lastQuestStep.Image
            };
        }

        public static QuestStep MapToQuestStep(NewQuestionJsonDto questionDto, string image)
        {
            return new()
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

        public static NewStoryLineJsonDto MapToNewStoryLineJsonDto(GameRoomSample gameRoomSample)
        {
            return new()
            {
                Title = gameRoomSample.Title,
                Plot = gameRoomSample.Plot,
                MainPlayer = gameRoomSample.MainPlayer
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
