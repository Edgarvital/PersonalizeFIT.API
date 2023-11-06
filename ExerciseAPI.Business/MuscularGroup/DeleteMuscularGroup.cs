using ExerciseAPI.Entity.Models;

namespace ExerciseAPI.Business.MuscularGroup
{
    public class DeleteMuscularGroup : IDeleteMuscularGroup
    {
        public Task<GetMuscularGroupResponse> DeleteMuscularGroupAsync(int muscularGroupId)
        {
            throw new NotImplementedException();
        }
    }

    public interface IDeleteMuscularGroup
    {

        public Task<GetMuscularGroupResponse> DeleteMuscularGroupAsync (int muscularGroupId);

    }
}
