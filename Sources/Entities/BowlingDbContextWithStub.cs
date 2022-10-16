using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class BowlingDbContextWithStub : BowlingDbContext
    {
        public BowlingDbContextWithStub()
        {
            Database.EnsureCreated();
        }

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
