using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingAPI.Connectors.Database;
using TrainingAPI.Entity.Entities;
using TrainingAPI.Entity.Models.TrainingPreset;

namespace TrainingAPI.Business.TrainingGroup
{
    public class PostTrainingPreset : IPostTrainingPreset
    {
        private readonly IMapper _mapper;
        private readonly TrainingDbContext _context;
        public PostTrainingPreset(IMapper mapper, TrainingDbContext context)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<string> PostTrainingPresetAsync(PostTrainingPresetRequest request)
        {
            var trainingPreset = _mapper.Map<TrainingPresetEntity>(request);

            _context.TrainingPresets.Add(trainingPreset);

            await _context.SaveChangesAsync();

            return "Pré-Definição de Treino Cadastrada com Sucesso!";
        }
    }

    public interface IPostTrainingPreset
    {
        public Task<string> PostTrainingPresetAsync(PostTrainingPresetRequest request);
    }
}
