using AutoMapper;
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

        public Task UpdateTrainingPresetAsync(int Id, UpdateTrainingPresetRequest request)
        {
            throw new NotImplementedException();
        }
    }

    public interface IUpdateTrainingPreset
    {
        public Task UpdateTrainingPresetAsync(int Id, UpdateTrainingPresetRequest request);
    }
}
