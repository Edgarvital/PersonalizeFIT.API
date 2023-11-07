using AutoMapper;
using TrainingAPI.Connectors.Database;

namespace TrainingAPI.Business.TrainingGroup
{
    public class PostTrainingGroup : IPostTrainingGroup
    {
        private readonly IMapper _mapper;
        private readonly TrainingDbContext _context;
        public PostTrainingGroup(IMapper mapper, TrainingDbContext context)
        {
            _context = context;
            _mapper = mapper;
        }

        public Task PostTrainingGroupAsync(string request)
        {
            throw new NotImplementedException();
        }
    }

    public interface IPostTrainingGroup
    {
        public Task PostTrainingGroupAsync(string request);
    }
}
