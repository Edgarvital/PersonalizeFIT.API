using AutoMapper;
using ExerciseAPI.Connectors.Database;
using ExerciseAPI.Entity.Entities;
using ExerciseAPI.Entity.Models;

namespace ExerciseAPI.Business.MuscularGroup
{
    public class PostMuscularGroup : IPostMuscularGroup
    {

        private readonly ExerciseDbContext _exerciseDbContext;
        private readonly IMapper _mapper;

        public PostMuscularGroup(ExerciseDbContext exerciseDbContext, IMapper mapper)
        {
            _exerciseDbContext = exerciseDbContext;
            _mapper = mapper;
        }
        public async Task<string> CreateMuscularGroupAsync(PostMuscularGroupRequest request)
        {
            var MuscularGroup = _mapper.Map<MuscularGroupEntity>(request);

            _exerciseDbContext.MuscularGroups.Add(MuscularGroup);

            await _exerciseDbContext.SaveChangesAsync();

            return "Grupo Muscular cadastrado com sucesso!";
        }
    }

    public interface IPostMuscularGroup
    {
        public Task<string> CreateMuscularGroupAsync(PostMuscularGroupRequest request);
    }
}
