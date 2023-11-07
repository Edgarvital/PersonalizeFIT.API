namespace TrainingAPI.Entity.Entities
{
    public class StudentHasTrainingPresetEntity
    {
        public DateTime? ExpirationDate { get; set; }
        public string AcquisitionType { get; set; }
        public string StudentId { get; set; }
        public int TrainingPresetId { get; set; }

        public TrainingPresetEntity TrainingPreset { get; set; }
    }
}
