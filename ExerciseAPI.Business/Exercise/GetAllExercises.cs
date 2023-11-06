using AutoMapper;
using AutoMapper.QueryableExtensions;
using ExerciseAPI.Connectors.Database;
using ExerciseAPI.Entity.Models;
using Microsoft.EntityFrameworkCore;

namespace ExerciseAPI.Business.Exercise
{
    public class GetAllExercises : IGetAllExercises
    {
        private readonly ExerciseDbContext _exerciseContext;
        private IMapper _mapper;
        public GetAllExercises(ExerciseDbContext exerciseContext, IMapper mapper)
        {
            _exerciseContext = exerciseContext;
            _mapper = mapper;
        }

        public async Task<List<GetExerciseResponse>> GetAllExercisesAsync()
        {
            var exercises = await _exerciseContext.Exercises
                .ProjectTo<GetExerciseResponse>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return exercises;
        }

    }

    public interface IGetAllExercises
    {
        public Task<List<GetExerciseResponse>> GetAllExercisesAsync();
    }
}
