using ExerciseAPI.Entity.Models;

namespace ExerciseAPI.Business.Exercise
{
    public class DeleteExercise : IDeleteExercise
    {
        public Task<GetExerciseResponse> DeleteExerciseAsync(int exerciseId)
        {
            throw new NotImplementedException();
        }
    }

    public interface IDeleteExercise
    {
        public Task<GetExerciseResponse> DeleteExerciseAsync(int exerciseId);
    }
}
