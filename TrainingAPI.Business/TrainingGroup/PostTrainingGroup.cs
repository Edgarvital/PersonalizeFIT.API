using AutoMapper;
using TrainingAPI.Connectors.Database;
using TrainingAPI.Entity.Entities;
using TrainingAPI.Entity.Models.TrainingGroup;

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

        public async Task<string> PostTrainingGroupAsync(PostTrainingGroupRequest request)
        {
            var TrainingGroup = _mapper.Map<TrainingGroupEntity>(request);

            _context.TrainingGroups.Add(TrainingGroup);            

            await _context.SaveChangesAsync();

            return "Grupo do Treinamento Cadastrado com Sucesso!";
        }
    }

    public interface IPostTrainingGroup
    {
        public Task<string> PostTrainingGroupAsync(PostTrainingGroupRequest request);
    }
}
