using ExerciseAPI.Entity.Models;

namespace ExerciseAPI.Business.MuscularGroup
{

    public class UpdateMuscularGroup : IUpdateMuscularGroup
    {
        public Task<bool> UpdateMuscularGroupAsync(int muscularGroupId, UpdateMuscularGroupRequest request)
        {
            throw new NotImplementedException();
        }
    }

    public interface IUpdateMuscularGroup
    {
        public Task<bool> UpdateMuscularGroupAsync(int muscularGroupId, UpdateMuscularGroupRequest request);
    }
}
