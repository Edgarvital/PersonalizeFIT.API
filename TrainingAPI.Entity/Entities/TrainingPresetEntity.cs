using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainingAPI.Entity.Entities
{
    public class TrainingPresetEntity : BaseEntity
    {
        public string Title { get; set; }
        public bool PresetDefaultFlag { get; set; }
        public string TrainerId { get; set; }
    }

    public class StudentHasTrainingPreset
    {
        public DateTime? ExpirationDate { get; set; }
        public string AcquisitionType { get; set; }
        public string StudentId { get; set; }
        public int TrainingPresetId { get; set; }
        public TrainingPresetEntity TrainingPreset { get; set; }
    }
}
