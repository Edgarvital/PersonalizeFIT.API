namespace ExerciseAPI.Entity.Entities
{
    public class ExerciseEntity : BaseEntity
    {

        public string Name { get; set; }
        public string Status { get; set; }
        public string TrainerId { get; set; }
        public int MuscularGroupId { get; set; }
        public MuscularGroupEntity MuscularGroup { get; set; }
        public List<ExerciseEntity> EquivalentExercises { get; set; } = new();
        public List<ExerciseEntity> Exercises { get; set; } = new();

    }

    public class ExerciseHasEquivalentExercise : BaseEntity
    {
        public int ExerciseId { get; set; }
        public ExerciseEntity Exercise { get; set; }
        public int EquivalentExerciseId { get; set; }
        public ExerciseEntity EquivalentExercise { get; set; }
    }
}
