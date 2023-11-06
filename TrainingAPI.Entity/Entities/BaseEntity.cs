using System.ComponentModel.DataAnnotations;

namespace TrainingAPI.Entity.Entities
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }
    }
}
