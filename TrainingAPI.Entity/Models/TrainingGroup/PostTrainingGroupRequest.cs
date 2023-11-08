using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainingAPI.Entity.Models.TrainingGroup
{
    public class PostTrainingGroupRequest
    {
        public string Name { get; set; }
        public int TrainingPresetId { get; set; }
        public List<PostTrainingGroupHasExerciseRequest> TrainingGroupHasExercises { get; set; }
    }

    public class PostTrainingGroupHasExerciseRequest
    {
        public int ExerciseId { get; set; }
        public string Observation { get; set; }
        public string TrainingSetJsonString { get; private set; }
    }
}
