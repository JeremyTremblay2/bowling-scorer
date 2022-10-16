using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class BowlingDbContext : DbContext
    {
        public DbSet<PlayerEntity> Players { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Data Source=Entities.BowlingDatabase.db");
        }

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
