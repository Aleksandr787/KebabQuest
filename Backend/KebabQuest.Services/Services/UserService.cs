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
            var user = await _userRepository.GetById(userToken) ?? 
                       throw new Exception("User not found");

            user.GameRoomIds ??= new List<string>();
            user.GameRoomIds.Add(gameRoomId);
    
            await _userRepository.UpdateEntity(user.Id!, user);
        }

        public async Task<ICollection<string>?> GetAllGameRooms(string userToken)
        {
            var user = await _userRepository.GetById(userToken);

            if (user == null)
                throw new Exception();

            return user.GameRoomIds;
        }

        public async Task DeleteGameRoom(string userToken, string gameRoomId)
        {
            var user = await _userRepository.GetById(userToken);

            if (user == null)
                throw new Exception();

            user.GameRoomIds.Remove(gameRoomId);
            await _userRepository.UpdateEntity(user.Id, user);
        }
    }
}
