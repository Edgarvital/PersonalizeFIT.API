using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Text.Json;
using System.Threading.Tasks;

namespace TrainingAPI.Entity.Entities
{
    public class TrainingGroupEntity : BaseEntity
    {
        public int TrainingPresetId { get; set; }
        public string Name { get; set; }
        public TrainingPresetEntity TrainingPreset { get; set; }
    }

    public class TrainingGroupHasExercise
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
