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


        public GameService(IGameRoomService gameRoomService, IGameLogicService gameLogicService, IUserService userService)
        {
            _gameRoomService = gameRoomService;
            _gameLogicService = gameLogicService;
            _userService = userService;
        }


        public async Task<NewGameDto> GenerateNewGameRoom(string userId)
        {
            var newStory = await _gameLogicService.GenerateNewStory();
            var gameRoom = DataMapper.MapToGameRoom(newStory);
            var image = await _gameLogicService.GenerateInitialImage(gameRoom);
            gameRoom.Steps!.First().Image = image;

            var gameRoomId = await _gameRoomService.CreateGameRoom(gameRoom);
            await _userService.AddGameRoomId(userId, gameRoomId);

            var result = DataMapper.MapToNewGameDto(newStory);
            result.Id = gameRoomId;
            result.Image = image;
            return result;
        }

        public async Task RemoveRoom(string userId, string gameRoomId)
        {
            await _gameRoomService.DeleteGameRoom(gameRoomId);
            await _userService.DeleteGameRoom(userId, gameRoomId);
        }

        public async Task<ICollection<GameRoom>?> GetAllGameRooms(string userId)
        {
            var gameRoomsIds = await _userService.GetAllGameRooms(userId);
            var result = new List<GameRoom>();

            if(gameRoomsIds == null)
                return result;

            foreach (var item in gameRoomsIds)
            {
                result.Append(await _gameRoomService.GetById(item));
            }

            return result;
        }

        public async Task<QuestStep> DoStep(string roomId, QuestStep step)
        {
            var gameRoom = await _gameRoomService.GetById(roomId);
            gameRoom.Steps.Add(step);
            await _gameRoomService.Update(roomId, gameRoom);

            var newQuestion = _gameLogicService.GenerateNewQuestion(gameRoom);
            var image = _gameLogicService.GenerateImagePerStep(gameRoom, step);

            var result = DataMapper.MapToQuestStep(newQuestion.Result, image.Result);
            return result;
        }
    }
}
