using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainingAPI.Entity.Models.TrainingPreset
{
    public class GetTrainingPresetResponse
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string TrainerId { get; set; }
    }
}
