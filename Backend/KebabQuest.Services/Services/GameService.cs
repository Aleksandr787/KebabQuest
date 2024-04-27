using KebabQuest.Data.Dto;
using KebabQuest.Data.Models;
using KebabQuest.Data.Repositories;
using KebabQuest.Services.Helpers;
using KebabQuest.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KebabQuest.Services.Services
{
    public class GameService : IGameService
    {
        private readonly IGameRoomService _gameRoomService;
        private readonly IGameLogicService _gameLogicService;
        private readonly IUserService _userService;
        private readonly IGameSampleService _gameSampleService;

        public GameService(
            IGameRoomService gameRoomService,
            IGameLogicService gameLogicService,
            IUserService userService,
            IGameSampleService gameSampleService)
        {
            _gameRoomService = gameRoomService;
            _gameLogicService = gameLogicService;
            _userService = userService;
            _gameSampleService = gameSampleService;
        }
        
        public async Task<NewGameDto> StartGameFromGameSample(string sampleId)
        {
            var gameSample = await _gameSampleService.GetById(sampleId);
            var newStoryLineJsonDto = DataMapper.MapToNewStoryLineJsonDto(gameSample);
            var newQuestion = await _gameLogicService.GenerateFirstQuestion(newStoryLineJsonDto);
            var newGameRoomDto = DataMapper.MapToGameRoom(newStoryLineJsonDto, gameSample.Image!, newQuestion);
            var gameId = await _gameRoomService.CreateGameRoom(newGameRoomDto);
            newGameRoomDto.Id = gameId;
            return DataMapper.MapToNewGameDto(newGameRoomDto, newGameRoomDto.Steps!.First());
        }

        public async Task<NewGameDto> GenerateNewGameRoom(string userId)
        {
            var newStory = await _gameLogicService.GenerateNewStory();
            var image = await _gameLogicService.GenerateInitialImage(newStory);
            var gameRoom = DataMapper.MapToGameRoom(newStory, image);
            var gameRoomId = await _gameRoomService.CreateGameRoom(gameRoom);
            await _userService.AddGameRoomId(userId, gameRoomId);

            var newGameDto = DataMapper.MapToNewGameDto(newStory, image);
            newGameDto.Id = gameRoomId;
            return newGameDto;
        }

        public async Task RemoveRoom(string userId, string gameRoomId)
        {
            await _gameRoomService.DeleteGameRoom(gameRoomId);
            await _userService.DeleteGameRoom(userId, gameRoomId);
        }

        public async Task<IList<NewGameDto>> GetAllGameRooms(string userId)
        {
            var gameRoomsIds = await _userService.GetAllGameRoomsIds(userId);
            var result = new List<NewGameDto>();

            if (gameRoomsIds is null)
            {
                return result;
            }

            foreach (var item in gameRoomsIds)
            {
                var gameRoom = await _gameRoomService.GetById(item);
                var lastStep = gameRoom.Steps!.Last();
                result.Add(DataMapper.MapToNewGameDto(gameRoom, lastStep));
            }

            return result;
        }

        public async Task<QuestStep> DoStep(string roomId, string answer)
        {
            var gameRoom = await _gameRoomService.GetById(roomId);
            gameRoom.Steps!.Last().Answer = answer;


            var newQuestion = _gameLogicService.GenerateNewQuestion(gameRoom);
            var lastStep = gameRoom.Steps!.Last();
            var image = _gameLogicService.GenerateImagePerStep(gameRoom, lastStep);

            await Task.WhenAll(newQuestion, image);
            var questStep = DataMapper.MapToQuestStep(newQuestion.Result, image.Result);
            gameRoom.Steps!.Add(questStep);
            
            await _gameRoomService.Update(roomId, gameRoom);
            return questStep;
        }
    }
}
