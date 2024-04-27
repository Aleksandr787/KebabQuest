using Amazon.Runtime.SharedInterfaces;
using KebabQuest.Data.Dto;
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
    public class GameSampleService : IGameSampleService
    {
        private readonly GameSampleRepository _gameSampleRepository;
        private readonly IGameLogicService _gameLogicService;

        public GameSampleService(GameSampleRepository gameSampleRepository, IGameLogicService gameLogicService)
        {
            _gameSampleRepository = gameSampleRepository;
            _gameLogicService = gameLogicService;
        }

        public async Task<ICollection<NewGameDto>> GetGameSamples()
        {
            return (await _gameSampleRepository.GetAll()).Select(f => DataMapper.MapToNewGameDto(f)).ToList();
        }

        public async Task Generate()
        {
            for(int i = 0; i < 6; i++)
            {
                var newStoryLine = await _gameLogicService.GenerateNewStory();
                var image = await _gameLogicService.GenerateInitialImage(newStoryLine);
                var gameRoom = DataMapper.MapToGameRoom(newStoryLine, image);

                var result = DataMapper.MapToGameSample(newStoryLine);
                result.Image = image;

                await _gameSampleRepository.AddEntity(result);
            }
        }
    }
}
