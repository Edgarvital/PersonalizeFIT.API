using AutoMapper;
using ExerciseAPI.Business.Exceptions;
using ExerciseAPI.Connectors.Database;
using ExerciseAPI.Entity.Models;
using Microsoft.EntityFrameworkCore;

namespace ExerciseAPI.Business.MuscularGroup
{
    public class GetMuscularGroup : IGetMuscularGroup
    {

        private readonly ExerciseDbContext _exerciseContext;
        private readonly IMapper _mapper;

        public GetMuscularGroup(ExerciseDbContext exerciseContext, IMapper mapper)
        {
            _exerciseContext = exerciseContext;
            _mapper = mapper;
        }     

        public async Task<GetMuscularGroupResponse> GetMuscularGroupAsync(int muscularGroupId)
        {
            var muscularGroup = await _exerciseContext
                .MuscularGroups
                .FirstOrDefaultAsync(e => e.Id == muscularGroupId) ?? throw new NotFoundException("Grupo Muscular não encontrado.");
            return _mapper.Map<GetMuscularGroupResponse>(muscularGroup);
        }
    }

    public interface IGetMuscularGroup
    {
        public Task<GetMuscularGroupResponse> GetMuscularGroupAsync(int muscularGroupId);
    }
}
