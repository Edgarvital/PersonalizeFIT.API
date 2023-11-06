using System.ComponentModel.DataAnnotations;

namespace ExerciseAPI.Entity.Entities
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }
    }
}
