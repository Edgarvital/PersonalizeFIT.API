using AutoMapper;
using TrainingAPI.Connectors.Database;

namespace TrainingAPI.Business.TrainingGroup
{
    public class UpdateTrainingGroup : IUpdateTrainingGroup
    {
        private readonly IMapper _mapper;
        private readonly TrainingDbContext _context;
        public UpdateTrainingGroup(IMapper mapper, TrainingDbContext context)
        {
            _context = context;
            _mapper = mapper;
        }

        public Task UpdateTrainingGroupAsync(int Id, string request)
        {
            throw new NotImplementedException();
        }
    }

    public interface IUpdateTrainingGroup
    {
        public Task UpdateTrainingGroupAsync(int Id, string request);
    }
}
