using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace TrainingAPI.Entity.Entities
{
    public class TrainingGroupHasExerciseEntity
    {
        public int TrainingGroupId { get; set; }
        public int ExerciseId { get; set; }
        public string Observation { get; set; }
        public string TrainingSetJsonString { get; private set; }
        public TrainingGroupEntity TrainingGroup { get; set; }

        [NotMapped]
        public JsonObject TrainingSetJson
        {
            get => JsonSerializer.Deserialize<JsonObject>(TrainingSetJsonString);
            set => TrainingSetJsonString = JsonSerializer.Serialize(value);
        }
    }
}
