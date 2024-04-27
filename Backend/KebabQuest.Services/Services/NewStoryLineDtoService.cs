using Amazon.Runtime.SharedInterfaces;
using KebabQuest.Data.Dto;
using KebabQuest.Data.Repositories;
using KebabQuest.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KebabQuest.Services.Services
{
    public class NewStoryLineDtoService : INewStoryLineDtoService
    {
        private readonly NewStoryLineDtoRepository _newStoryLineDtoRepository;
        private readonly IGameLogicService _gameLogicService;

        public NewStoryLineDtoService(NewStoryLineDtoRepository newStoryLineDtoRepository, IGameLogicService gameLogicService)
        {
            _newStoryLineDtoRepository = newStoryLineDtoRepository;
            _gameLogicService = gameLogicService;
        }

        public async Task<NewStoryLineJsonDto> GetById(string id)
        {
            return await _newStoryLineDtoRepository.GetById(id);
        }

        public async Task Generate()
        {
            for(int i = 0; i < 6; i++)
            {
                var newStoryLine = await _gameLogicService.GenerateNewStory();
                await _newStoryLineDtoRepository.AddEntity(newStoryLine);
            }
        }
    }
}
