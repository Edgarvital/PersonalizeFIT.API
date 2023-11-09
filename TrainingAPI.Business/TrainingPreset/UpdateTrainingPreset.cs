using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TrainingAPI.Business.Exceptions;
using TrainingAPI.Connectors.Database;
using TrainingAPI.Entity.Models.TrainingPreset;

namespace TrainingAPI.Business.TrainingGroup
{
    public class UpdateTrainingPreset : IUpdateTrainingPreset
    {
        private readonly IMapper _mapper;
        private readonly TrainingDbContext _context;
        public UpdateTrainingPreset(IMapper mapper, TrainingDbContext context)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<string> UpdateTrainingPresetAsync(int Id, UpdateTrainingPresetRequest request)
        {
            var TrainingPreset = await _context
                .TrainingPresets
                .FirstOrDefaultAsync(e => e.Id == Id) ?? throw new NotFoundException("Training Preset não encontrado.");

            _mapper.Map(request, TrainingPreset);

            await _context.SaveChangesAsync();

            return "Pré-definição de Treino Atualizada com Sucesso!";
        }
    }

    public interface IUpdateTrainingPreset
    {
        public Task<string> UpdateTrainingPresetAsync(int Id, UpdateTrainingPresetRequest request);
    }
}
