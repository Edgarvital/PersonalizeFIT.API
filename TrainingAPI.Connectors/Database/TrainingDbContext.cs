using Microsoft.EntityFrameworkCore;
using TrainingAPI.Entity.Entities;

namespace TrainingAPI.Connectors.Database
{
    public class TrainingDbContext : DbContext
    {

        public const string Schema = "PersonalizeFit.Training";

        public TrainingDbContext (DbContextOptions<TrainingDbContext> options) : base(options) { }

        public DbSet<TrainingPresetEntity> TrainingPresets { get; set; }
        public DbSet<StudentHasTrainingPresetEntity> StudentHasTrainingPresets { get; set; }
        public DbSet<TrainingGroupEntity> TrainingGroups { get; set; }
        public DbSet<TrainingGroupHasExerciseEntity> TrainingGroupHasExercises { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.HasDefaultSchema(Schema);

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<StudentHasTrainingPresetEntity>().HasNoKey();
            modelBuilder.Entity<TrainingGroupHasExerciseEntity>().HasNoKey();
        }


    }
}
