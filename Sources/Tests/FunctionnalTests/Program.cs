using Entities;
using Entity2Model;
using Microsoft.EntityFrameworkCore;
using Model.Games;
using Model.Players;
using Stub;
using static System.Console;

namespace FunctionnalTests
{
    class Program
    {
        static void Main(string[] args)
        {
            PlayerEntity toto = new PlayerEntity { Name = "Toto", Image = "girafe.png" };
            PlayerEntity antoine = new PlayerEntity { Name = "Antoine", Image = "mickaelJackson.png" };
            PlayerEntity francis = new PlayerEntity { Name = "Francis", Image = "corrida.png" };

            Player joker = new Player("Joker", "scary.png");
            Player bluffer = new Player("Bluffer", "trollface.png");
            Player misterQI = new Player("misterQI", "darkMask.png");

            using (BowlingDbContextWithStub db = new())
            {
                WriteLine("Opening the connection to the database.");

                if (db.Players.Any())
                {
                    WriteLine("The databse is not empty, it has already some players.");
                    WriteLine("Here is the base content with PlayerEntities transformed to Players:");
                    DisplayPlayerCollectionToModel(db.Players);

                    WriteLine("Start of cleaning....");

                    foreach (var player in db.Players)
                    {
                        WriteLine($"Suppression of {player.Name}.");
                        db.Players.Remove(player);
                    }

                    WriteLine("Database before saving the changes:");
                    DisplayPlayerCollectionToModel(db.Players);

                    db.SaveChanges();

                    WriteLine("Database after saving the changes:");
                    DisplayPlayerCollectionToModel(db.Players);
                }

                WriteLine("Now we will add 3 new Players Entity to the database: ");
                db.Players.AddRange(new PlayerEntity[] { toto, antoine, francis });
                db.SaveChanges();
                DisplayPlayerCollectionToModel(db.Players);

                WriteLine("We will now add 3 more players, which will be transformed into PlayerEntities. Here there are: ");
                DisplayPlayerCollectionToEntities(joker, bluffer, misterQI);
                db.Players.AddRange(new PlayerEntity[] { joker.ToEntity(), bluffer.ToEntity(), misterQI.ToEntity() });
                db.SaveChanges();

                WriteLine("Here is the final collection of players present in the database: ");
                DisplayPlayerCollectionToModel(db.Players);
            }
            WriteLine("Connection closed.");
        }

        private static void DisplayPlayerCollectionToEntities(params Player[] players)
        {
            foreach (Player playerEntity in players)
            {
                PlayerEntity entity = playerEntity.ToEntity();
                WriteLine($"\t[{entity.ID}] - {entity.Name} : {entity.Image}");
            }
        }

        private static void DisplayPlayerCollectionToModel(DbSet<PlayerEntity> entities)
        {
            foreach (PlayerEntity playerEntity in entities)
            {
                Player player = playerEntity.ToModel();
                WriteLine($"\t{player}");
            }
        }
    }
}
