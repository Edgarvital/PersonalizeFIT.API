﻿using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Nodes;
using System.Text.Json;

namespace TrainingAPI.Entity.Entities
{
    public class TrainingGroupEntity : BaseEntity
    {
        public int TrainingPresetId { get; set; }
        public string Name { get; set; }
        public TrainingPresetEntity TrainingPreset { get; set; }
        public List<TrainingGroupHasExercise> TrainingGroupHasExercises { get; set; } = new();
    }

    public class TrainingGroupHasExercise : BaseEntity
    {
        public int TrainingGroupId { get; set; }
        public int ExerciseId { get; set; }
        public string Observation { get; set; }
        public string TrainingSetJsonString { get; private set; }
        public TrainingGroupEntity TrainingGroup { get; set; }
        [NotMapped]
        public JsonObject TrainingSetJson
        {
            //get => JsonSerializer.Deserialize<JsonObject>(TrainingSetJsonString);
            set => TrainingSetJsonString = JsonSerializer.Serialize(value);
        }
    }
}
