using AutoMapper;
using ExerciseAPI.Business.Exceptions;
using ExerciseAPI.Connectors.Database;
using ExerciseAPI.Entity.Entities;
using ExerciseAPI.Entity.Models;
using Microsoft.EntityFrameworkCore;

namespace ExerciseAPI.Business.MuscularGroup
{

    public class UpdateMuscularGroup : IUpdateMuscularGroup
    {

        private readonly ExerciseDbContext _exerciseContext;
        private readonly IMapper _mapper;

        public UpdateMuscularGroup(ExerciseDbContext exerciseContext, IMapper mapper)
        {
            _exerciseContext = exerciseContext;
            _mapper = mapper;
        }

        public async Task<string> UpdateMuscularGroupAsync(int muscularGroupId, UpdateMuscularGroupRequest request)
        {
            var muscularGroup = await _exerciseContext
               .MuscularGroups
               .FirstOrDefaultAsync(e => e.Id == muscularGroupId) ?? throw new NotFoundException("Grupo Muscular não encontrado.");

            _mapper.Map(request, muscularGroup);

            await _exerciseContext.SaveChangesAsync();

            return "Grupo Muscular cadastrado com sucesso!";
        }
    }

    public interface IUpdateMuscularGroup
    {
        public Task<string> UpdateMuscularGroupAsync(int muscularGroupId, UpdateMuscularGroupRequest request);
    }
}
