using AutoMapper;
using ExerciseAPI.Business.Exceptions;
using ExerciseAPI.Connectors.Database;
using ExerciseAPI.Entity.Models;
using Microsoft.EntityFrameworkCore;

namespace ExerciseAPI.Business.MuscularGroup
{
    public class DeleteMuscularGroup : IDeleteMuscularGroup
    {
        private readonly ExerciseDbContext _exerciseContext;
        private readonly IMapper _mapper;

        public DeleteMuscularGroup(ExerciseDbContext exerciseContext, IMapper mapper)
        {
            _exerciseContext = exerciseContext;
            _mapper = mapper;
        }

        public async Task<GetMuscularGroupResponse> DeleteMuscularGroupAsync(int muscularGroupId)
        {
            var muscularGroup = await _exerciseContext
                .MuscularGroups
                .FirstOrDefaultAsync(e => e.Id == muscularGroupId) ?? throw new NotFoundException("Grupo Muscular não encontrado.");

            _exerciseContext.MuscularGroups.Remove(muscularGroup);
            await _exerciseContext.SaveChangesAsync();

            return _mapper.Map<GetMuscularGroupResponse>(muscularGroup);
        }
    }

    public interface IDeleteMuscularGroup
    {

        public Task<GetMuscularGroupResponse> DeleteMuscularGroupAsync (int muscularGroupId);

    }
}
