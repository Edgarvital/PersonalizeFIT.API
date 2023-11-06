using ExerciseAPI.Entity.Models;

namespace ExerciseAPI.Business.Exercise
{
    public class UpdateExercise : IUpdateExercise
    {
        public Task<bool> UpdateExerciseAsync(int exerciseId, UpdateExerciseRequest request)
        {
            throw new NotImplementedException();
        }
    }

    public interface IUpdateExercise
    {
        public Task<bool> UpdateExerciseAsync(int exerciseId, UpdateExerciseRequest request);
    }
}
