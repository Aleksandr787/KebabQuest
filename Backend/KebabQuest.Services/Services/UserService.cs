using KebabQuest.Data.Repositories;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}
