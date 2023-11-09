using Microsoft.EntityFrameworkCore;
using TrainingAPI.Entity.Entities;

namespace TrainingAPI.Connectors.Database
{
    public class TrainingDbContext : DbContext
    {

        public const string Schema = "PersonalizeFit.Training";

        public TrainingDbContext (DbContextOptions<TrainingDbContext> options) : base(options) { }

        public DbSet<TrainingPresetEntity> TrainingPresets { get; set; }
        public DbSet<TrainingGroupEntity> TrainingGroups { get; set; }
        public DbSet<TrainingGroupHasExercise> TrainingGroupHasExercise { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.HasDefaultSchema(Schema);

            modelBuilder.Entity<TrainingGroupEntity>()
                .HasMany(e => e.TrainingGroupHasExercises)
                .WithOne(e => e.TrainingGroup)
                .HasForeignKey(e => e.TrainingGroupId);

            modelBuilder.Entity<TrainingPresetEntity>()
                .HasMany(e => e.StudentHasTrainingPresets)
                .WithOne(e => e.TrainingPreset)
                .HasForeignKey(e => e.TrainingPresetId); ;                    

            base.OnModelCreating(modelBuilder);

        }


    }
}
