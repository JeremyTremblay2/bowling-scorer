using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    /// <summary>
    /// Represent a DBcontext class for our BowlingScorer app.
    /// </summary>
    public class BowlingDbContext : DbContext
    {
        /// <summary>
        /// Contains a set of PlayerEntity which represents the players contained in our application.
        /// </summary>
        public DbSet<PlayerEntity>? Players { get; set; }

        /// <summary>
        /// Used to configure the database.
        /// </summary>
        /// <param name="optionsBuilder">The options that can be configured.</param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Data Source=Entities.BowlingDatabase.db");
        }

        /// <summary>
        /// Method call when the model is creating, and allow us to define some settings and constraints 
        /// about our entities before creating them.
        /// </summary>
        /// <param name="modelBuilder">The model builder.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PlayerEntity>().ToTable("Player");

            modelBuilder.Entity<PlayerEntity>().Property(n => n.Name)
                                               .IsRequired()
                                               .HasMaxLength(256);

            modelBuilder.Entity<PlayerEntity>().Property(n => n.ID)
                                               .ValueGeneratedOnAdd();

            base.OnModelCreating(modelBuilder);
        }
    }
}
