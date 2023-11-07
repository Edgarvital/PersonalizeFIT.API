using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainingAPI.Entity.Models
{
    public class UpdateTrainingGroupRequest
    {
        public string Name { get; set; }
        public int TrainingPresetId { get; set; }
        public List<UpdateTrainingGroupHasExerciseRequest> TrainingGroupHasExercises { get; set;}
    }

    public class UpdateTrainingGroupHasExerciseRequest
    {
        public int ExerciseId { get; set; }
        public string Observation { get; set; }
        public string TrainingSetJsonString { get; private set; }
    }
}
