using AutoMapper;
using ExerciseAPI.Business.Exceptions;
using ExerciseAPI.Connectors.Database;
using ExerciseAPI.Entity.Models;
using Microsoft.EntityFrameworkCore;

namespace ExerciseAPI.Business.Exercise
{
    public class DeleteExercise : IDeleteExercise
    {
        private readonly ExerciseDbContext _exerciseContext;
        private IMapper _mapper;
        public DeleteExercise(ExerciseDbContext exerciseContext, IMapper mapper)
        {
            _exerciseContext = exerciseContext;
            _mapper = mapper;
        }
        public async Task<GetExerciseResponse> DeleteExerciseAsync(int exerciseId)
        {
            var exercise = await _exerciseContext.Exercises
                .Include(e => e.MuscularGroup)
                .Include(e => e.EquivalentExercises)
                .FirstOrDefaultAsync(e => e.Id == exerciseId) ?? throw new NotFoundException("Exercicio não encontrado.");
            _exerciseContext.Exercises.Remove(exercise);
            await _exerciseContext.SaveChangesAsync();

            return _mapper.Map<GetExerciseResponse>(exercise);

        }

    }

    public interface IDeleteExercise
    {
        public Task<GetExerciseResponse> DeleteExerciseAsync(int exerciseId);
    }
}
