using ExerciseAPI.Entity.Entities;
using Microsoft.EntityFrameworkCore;

namespace ExerciseAPI.Connectors.Database
{
    public class ExerciseDbContext : DbContext
    {

        public const string Schema = "Exercise";

        public ExerciseDbContext (DbContextOptions<ExerciseDbContext> options) : base(options) { }

        public DbSet<ExerciseEntity> Exercises { get; set; }
        public DbSet<MuscularGroupEntity> MuscularGroups { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema(Schema);            

            modelBuilder.Entity<ExerciseEntity>()
                .HasMany(e => e.EquivalentExercises)
                .WithMany(e => e.Exercises)
                .UsingEntity(
                "ExerciseHasEquivalentExercise",
                l => l.HasOne(typeof(ExerciseEntity)).WithMany().HasForeignKey("EquivalentExerciseId").HasPrincipalKey(nameof(ExerciseEntity.Id)),
                r => r.HasOne(typeof(ExerciseEntity)).WithMany().HasForeignKey("ExerciseId").HasPrincipalKey(nameof(ExerciseEntity.Id)),
                k => k.HasKey("EquivalentExerciseId", "ExerciseId"));

            base.OnModelCreating(modelBuilder);
        }


    }
}
