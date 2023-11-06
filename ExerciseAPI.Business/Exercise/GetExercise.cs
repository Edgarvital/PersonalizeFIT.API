using AutoMapper;
using ExerciseAPI.Business.Exceptions;
using ExerciseAPI.Connectors.Database;
using ExerciseAPI.Entity.Entities;
using ExerciseAPI.Entity.Models;
using Microsoft.EntityFrameworkCore;

namespace ExerciseAPI.Business.Exercise
{
    public class GetExercise : IGetExercise
    {

        private readonly ExerciseDbContext _exerciseContext;
        private readonly IMapper _mapper;

        public GetExercise(ExerciseDbContext exerciseContext, IMapper mapper)
        {
            _exerciseContext = exerciseContext;
            _mapper = mapper;
        }

        public async Task<GetExerciseResponse> GetExerciseAsync(int exerciseId)
        {
            var exercise = await _exerciseContext
                .Exercises
                .Include(e => e.MuscularGroup)
                .Include(e => e.EquivalentExercises)
                .FirstOrDefaultAsync(e => e.Id == exerciseId) ?? throw new NotFoundException("Exercicio não encontrado.");

            return _mapper.Map<GetExerciseResponse>(exercise);
        }

    }

    public interface IGetExercise
    {
        public Task<GetExerciseResponse> GetExerciseAsync(int exerciseId);
    }
}
