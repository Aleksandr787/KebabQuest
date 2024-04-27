using KebabQuest.Data.Dto;
using KebabQuest.Data.Models;
using KebabQuest.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KebabQuest.Services.Services
{
    public class GameService : IGameService
    {
        private readonly GameRoomRepository _gameRoomRepository;


        public GameService() { }


        public async Task<NewStoryLineJsonDto> GenerateNewGameRoom()
        {

        }

        public async Task<GameRoom> GetGameRoom(string gameRoomId)
        {

        }

        public async Task DeleteRoom(string gameRoomId)
        {

        }

        public async Task<ICollection<string>?> GetAllGameRooms(string userToken)
        {

        }
    }
}
