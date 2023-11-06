using ExerciseAPI.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExerciseAPI.Entity.Models
{
    public class GetExerciseResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        public string TrainerId { get; set; }
        public GetMuscularGroupResponse MuscularGroup { get; set; }
        public List<ExerciseAttributesResponse> EquivalentExercises { get; set; }
    }

    public class ExerciseAttributesResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }

    }

}
