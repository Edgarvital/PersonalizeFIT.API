using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExerciseAPI.Entity.Models
{
    public class PostExerciseRequest
    {
        public string Name { get; set; }
        public string Status { get; set; }
        public int MuscularGroupId { get; set; }
        public string TrainerId { get; set; }
        public List<int> EquivalentExerciseIds { get; set; }
    }
}
