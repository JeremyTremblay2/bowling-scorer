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
    /// Represent a DBcontext class for our BowlingScorer app that will also contains some data when initialized.
    /// </summary>
    public class BowlingDbContextWithStub : BowlingDbContext
    {
        /// <summary>
        /// Create a new instance of BowlingDbContextWithStub.
        /// Ensure that the database is created.
        /// </summary>
        public BowlingDbContextWithStub()
        {
            Database.EnsureCreated();
        }

        /// <summary>
        /// Method call when the model is creating, and allow us to define some settings and constraints 
        /// about our entities before creating them.
        /// </summary>
        /// <param name="modelBuilder">The model builder.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<PlayerEntity>().HasData(
                new PlayerEntity { ID = 1, Name = "Mickael", Image = "imageMickael.png" },
                new PlayerEntity { ID = 2, Name = "Jeremy", Image = "imageJeremy.png" },
                new PlayerEntity { ID = 3, Name = "Lucas", Image = "imageLucas.png" }
            );
        }
    }
}
