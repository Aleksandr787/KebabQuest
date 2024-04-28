using KebabQuest.Data.Models;
using KebabQuest.Data.Repositories;
using KebabQuest.Services.Interfaces;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KebabQuest.Services.Services
{
    public class ScreenCastService : IScreenCastService
    {
        private readonly GameRoomRepository _gameRoomRepository;

        public ScreenCastService(GameRoomRepository gameRoomRepository)
        {
            _gameRoomRepository = gameRoomRepository;
        }

        public async Task<ICollection<QuestStep>> GetScreenCastCollection()
        {
            return (await _gameRoomRepository.GetAll())
                .Where(q => q.Steps is not null && q.Steps?.Last() is not null)
                .Select(q => q.Steps!.Last())
                .Take(24)
                .ToList();
        }
    }
}
