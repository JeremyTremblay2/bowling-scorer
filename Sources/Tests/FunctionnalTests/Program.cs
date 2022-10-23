using Entities;
using Entities.Entities;
using Entities.Entities.Game;
using Entity2Model;
using FrameModel.Frame;
using FrameModel.Frame.ThrowResults;
using Microsoft.EntityFrameworkCore;
using Model.Games;
using Model.Players;
using Model.Score;
using Model.Score.Rules;
using Stub;
using static System.Console;

namespace FunctionnalTests
{
    class Program
    {
        static void Main(string[] args)
        {
            var games = new Stub.Stub().GetGames(0, 10).Result;
            using (BowlingDbContext db = new())
            {
                WriteLine("Opening the connection to the database.");
                WriteLine("Cleaning tables...");
                if (db.GamePlayers.Any())
                {
                    foreach (var gp in db.GamePlayers)
                    {
                        db.GamePlayers.Remove(gp);
                    }
                }
                if (db.Games.Any())
                {
                    foreach (var g in db.Games)
                    {
                        db.Games.Remove(g);
                    }
                }
                if (db.Players.Any())
                {
                    foreach( var player in db.Players)
                    {
                        db.Players.Remove(player);
                    }
                }
                db.SaveChanges();
                WriteLine("Add New Games, Players in the db...");
                foreach (var game in games)
                {
                    GameEntity gameEntity = game.ToEntity();
                    foreach(var player in game.Players)
                    {
                        gameEntity.GamePlayer.Add(
                            new GamePlayer()
                            {
                                GameEntity = gameEntity,
                                PlayerEntity = player.ToEntity()
                            }
                        );
                    }
                    db.Games.Add(gameEntity);
                }
                db.SaveChanges();

                /*WriteLine("Games in the db :");
                foreach (var gameEnt in db.Games.Include(g => g.GamePlayer).Include(g => g.GamePlayer.Select(gp => gp.PlayerEntity)))
                {
                    WriteLine(gameEnt.ToModel());
                }*/
            }
        }

        private static void FrameTROneToManyFunctionnalTests()
        {
            ScoreTable scoreTable = new ScoreTable(new ClassicRules());
            scoreTable.WriteValue(scoreTable.Frames[0], 0, ThrowResult.SIX);
            scoreTable.WriteValue(scoreTable.Frames[0], 1, ThrowResult.TWO);
            scoreTable.WriteValue(scoreTable.Frames[1], 0, ThrowResult.THREE);
            scoreTable.WriteValue(scoreTable.Frames[1], 1, ThrowResult.FOUR);
            scoreTable.WriteValue(scoreTable.Frames[2], 0, ThrowResult.NONE);
            scoreTable.WriteValue(scoreTable.Frames[2], 1, ThrowResult.STRIKE);
            scoreTable.WriteValue(scoreTable.Frames[3], 0, ThrowResult.NONE);
            scoreTable.WriteValue(scoreTable.Frames[3], 1, ThrowResult.STRIKE);
            scoreTable.WriteValue(scoreTable.Frames[4], 0, ThrowResult.NONE);
            scoreTable.WriteValue(scoreTable.Frames[4], 1, ThrowResult.STRIKE);
            scoreTable.WriteValue(scoreTable.Frames[5], 0, ThrowResult.NONE);
            scoreTable.WriteValue(scoreTable.Frames[5], 1, ThrowResult.STRIKE);
            scoreTable.WriteValue(scoreTable.Frames[6], 0, ThrowResult.NONE);
            scoreTable.WriteValue(scoreTable.Frames[6], 1, ThrowResult.STRIKE);
            scoreTable.WriteValue(scoreTable.Frames[7], 0, ThrowResult.NONE);
            scoreTable.WriteValue(scoreTable.Frames[7], 1, ThrowResult.STRIKE);
            scoreTable.WriteValue(scoreTable.Frames[8], 0, ThrowResult.NONE);
            scoreTable.WriteValue(scoreTable.Frames[8], 1, ThrowResult.STRIKE);
            scoreTable.WriteValue(scoreTable.Frames[9], 0, ThrowResult.STRIKE);
            scoreTable.WriteValue(scoreTable.Frames[9], 1, ThrowResult.STRIKE);
            scoreTable.WriteValue(scoreTable.Frames[9], 2, ThrowResult.STRIKE);
            scoreTable.UpdateAll();
            using (BowlingDbContext db = new())
            {
                WriteLine("Opening the connection to the database.");

                if (db.Frames.Any())
                {
                    WriteLine("There is some frames in the db, clean it");
                    foreach (var frame in db.Frames)
                    {
                        WriteLine("Delete one frame : " + frame);
                        db.Frames.Remove(frame);
                    }
                    db.SaveChanges();
                }

                WriteLine("Start to write the ScoreTable's scores");
                foreach (AFrame frameToSave in scoreTable.Frames)
                {
                    WriteLine("Add : " + frameToSave);
                    db.Frames.Add(frameToSave.ToEntity());
                }
                db.SaveChanges();

                WriteLine("All frames in the db :");
                foreach (var frameToShow in db.Frames.Include(f => f.ThrowResultEntitys))
                {
                    WriteLine(frameToShow.ToModel());
                }
            }
        }

        private static void PlayerFuctionnalTests()
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

                if (db.Players is null)
                {
                    WriteLine("----- ERROR: The player DBSET is null, program termination.-----");
                    return;
                }

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
