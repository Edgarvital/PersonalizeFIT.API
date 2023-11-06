namespace Entity.Models
{
    public class PostTrainerHasStudentsRequest
    {     
        public Guid TrainerId { get; set; }

        public Guid StudentId { get; set; }

        public bool Active = true;
    }
}
