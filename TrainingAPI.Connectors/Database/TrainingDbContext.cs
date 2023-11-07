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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.HasDefaultSchema(Schema);            

            modelBuilder.Entity<TrainingPresetEntity>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Title).IsRequired();
                entity.Property(e => e.PresetDefaultFlag).HasDefaultValue(true);
                entity.Property(e => e.TrainerId).IsRequired();
            });

            modelBuilder.Entity<StudentHasTrainingPreset>(entity =>
            {
                entity.HasKey(e => new { e.TrainingPresetId });

                entity.Property(e => e.ExpirationDate);
                entity.Property(e => e.AcquisitionType);

                entity.HasOne(e => e.TrainingPreset)
                    .WithMany() 
                    .HasForeignKey(e => e.TrainingPresetId);

            });

            modelBuilder.Entity<TrainingGroupHasExercise>(entity =>
            {
                entity.HasKey(e => new { e.TrainingGroupId, e.ExerciseId }); 
                entity.Property(e => e.Observation);
                entity.Property(e => e.TrainingSetJsonString);

                entity.HasOne(e => e.TrainingGroup)
                    .WithMany()
                    .HasForeignKey(e => e.TrainingGroupId);
            });

            base.OnModelCreating(modelBuilder);

        }


    }
}
