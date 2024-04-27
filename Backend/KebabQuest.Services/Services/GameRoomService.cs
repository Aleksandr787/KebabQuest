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
        private readonly UserRepository _userRepository;

        public GameRoomService(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }


    }
}
