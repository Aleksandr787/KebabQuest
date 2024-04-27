using KebabQuest.Data.Models;
using KebabQuest.Data.Repositories;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace KebabQuest.Services.Services
{
    public class UserService : IUserService
    {
        private readonly UserRepository _userRepository;

        public UserService(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<string> RegisterUserAsync()
        {
            string createdToken = ObjectId.GenerateNewId().ToString();
            await _userRepository.RegisterUserAsync(createdToken);
            return createdToken;
        }

        public async Task AddGameRoomId(string userToken, string gameRoomId)
        {
            var player = await _userRepository.GetById(userToken);

            if (player == null)
                throw new Exception();

            player.GameRoomIds.Append(gameRoomId);
            await _userRepository.UpdateEntity(player.Id, player);
        }

        public async Task<ICollection<string>?> GetAllGameRooms(string playerId)
        {
            var player = await _userRepository.GetById(playerId);

            if (player == null)
                throw new Exception();

            return player.GameRoomIds;
        }

        public async Task DeleteGameRoom(string userToken, string gameRoomId)
        {
            var player = await _userRepository.GetById(userToken);

            if (player == null)
                throw new Exception();

            player.GameRoomIds.Remove(gameRoomId);
            await _userRepository.UpdateEntity(player.Id, player);
        }
    }
}
