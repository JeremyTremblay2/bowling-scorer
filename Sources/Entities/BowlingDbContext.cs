using Entities.Frame;
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

        public DbSet<FrameEntity> Frames { get; set; }

        public DbSet<ThrowResultEntity> ThrowResults { get; set; }

        public BowlingDbContext()
        { }

        public BowlingDbContext(DbContextOptions<BowlingDbContext> options)
            : base(options)
        { }

        /// <summary>
        /// Used to configure the database.
        /// </summary>
        /// <param name="optionsBuilder">The options that can be configured.</param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite($"Data Source=Entities.BowlingDatabase.db");
            }
        }

        /// <summary>
        /// Method call when the model is creating, and allow us to define some settings and constraints 
        /// about our entities before creating them.
        /// </summary>
        /// <param name="modelBuilder">The model builder.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Players
            modelBuilder.Entity<PlayerEntity>().ToTable("Player");

            modelBuilder.Entity<PlayerEntity>().Property(n => n.Name)
                                               .IsRequired()
                                               .HasMaxLength(256);

            modelBuilder.Entity<PlayerEntity>().Property(n => n.ID)
                                               .ValueGeneratedOnAdd();

            // Frames
            modelBuilder.Entity<FrameEntity>().ToTable("Frame"); //table definition
            modelBuilder.Entity<FrameEntity>().HasKey(f => f.FrameId); //PK definition
            modelBuilder.Entity<FrameEntity>().Property(f => f.FrameId)
                .ValueGeneratedOnAdd(); //PK Generation directive

            // ThrowResults
            modelBuilder.Entity<ThrowResultEntity>().ToTable("ThrowResult"); //table definition
            modelBuilder.Entity<ThrowResultEntity>().HasKey(tr => tr.ThrowResultId); //PK definition
            modelBuilder.Entity<ThrowResultEntity>().Property(tr => tr.ThrowResultId)
                .ValueGeneratedOnAdd(); //PK Generation directive

            // Add the shadow property to the model (shadow = not hard-writed, it is created thanks to this line
            modelBuilder.Entity<ThrowResultEntity>()
                .Property<int>("FrameForeignKey");
            // Use the shadow property as a foreign key
            modelBuilder.Entity<ThrowResultEntity>()
                .HasOne(tr => tr.FrameEntity)
                .WithMany(f => f.ThrowResultEntitys)
                .HasForeignKey("FrameForeignKey");


            base.OnModelCreating(modelBuilder);
        }
    }
}
