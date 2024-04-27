using KebabQuest.Data.Models;
using KebabQuest.Data.Repositories;
using KebabQuest.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KebabQuest.Services.Services
{
    public class GameRoomService : IGameRoomService
    {
        private readonly GameRoomRepository _gameRoomRepository;

        public GameRoomService(GameRoomRepository gameRoomRepository)
        {
            _gameRoomRepository = gameRoomRepository;
        }


        public async Task<string> CreateGameRoom(GameRoom newRoom)
        {
            return await _gameRoomRepository.CreateGameRoom(newRoom);
        }

        public async Task DeleteGameRoom(string id)
        {
            await _gameRoomRepository.Delete(id);
        }

        public async Task<GameRoom> GetById(string id)
        {
            var gameRoom = await _gameRoomRepository.GetById(id);

            if (gameRoom == null)
                throw new Exception();

            return gameRoom;
        }

        public async Task Update(string id, GameRoom gameRoom)
        {
            await _gameRoomRepository.UpdateEntity(id, gameRoom);
        }
    }
}
