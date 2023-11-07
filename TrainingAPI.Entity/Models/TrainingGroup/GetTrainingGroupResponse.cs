using System.Text.Json;
using System.Text.Json.Nodes;

namespace TrainingAPI.Entity.Models.TrainingGroup
{
    public class GetTrainingGroupResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public TrainingPresetResponse TrainingPreset { get; set; }
        public List<TrainingGroupHasExerciseResponse> TrainingGroupHasExercises { get; set; }
    }

    public class TrainingPresetResponse
    {
        public int Id;
        public string Title { get; set; }
    }
    public class TrainingGroupHasExerciseResponse
    {
        public int ExerciseId { get; set; }
        public string Observation { get; set; }
        public string TrainingSetJsonString { get; private set; }

        public JsonObject TrainingSetJson
        {
            get => JsonSerializer.Deserialize<JsonObject>(TrainingSetJsonString);
            set => TrainingSetJsonString = JsonSerializer.Serialize(value);
        }
    }
}
