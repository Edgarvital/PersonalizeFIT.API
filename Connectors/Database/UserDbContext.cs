﻿using Entity.Entities;
using Microsoft.EntityFrameworkCore;

namespace Connectors.Database
{
    public class UserDbContext : DbContext
    {
        public const string Schema = "PersonalizeFit.User";

        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options) {}

        public DbSet<TrainerHasStudentEntity> TrainerHasStudent { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema(Schema);

            base.OnModelCreating(modelBuilder);
        }

    }
}
