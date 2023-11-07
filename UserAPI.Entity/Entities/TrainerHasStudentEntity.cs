namespace UserAPI.Entity.Entities
{
    public class TrainerHasStudentEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string TrainerId { get; set; }
        public string StudentId { get; set; }
        public bool Active { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
