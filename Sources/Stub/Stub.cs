using Business;
using FrameModel.Frame.ThrowResults;
using Model.Games;
using Model.Players;
using Model.Score;
using Model.Score.Rules;

namespace Stub
{
    /// <summary>
    /// Used to generate fake data for the tests
    /// </summary>
    public class Stub : IDataManager
    {
        /// <summary>
        /// Do not call this method here, a stub only generate fake data, he can't simulate the other aspects of
        /// a persistence layer.
        /// </summary>
        /// <param name="game"></param>
        /// <exception cref="NotImplementedException">In all cases, the exception is throw.</exception>
        /// <returns></returns>
        public Task<bool> AddGame(Game game)
        {
            throw new NotImplementedException("This is a Stub, you can just use it to get fake data");
        }

        /// <summary>
        /// Do not call this method here, a stub only generate fake data, he can't simulate the other aspects of
        /// a persistence layer.
        /// </summary>
        /// <param name="player"></param>
        /// <exception cref="NotImplementedException">In all cases, the exception is throw.</exception>
        /// <returns></returns>
        public Task<bool> AddPlayer(Player player)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Do not call this method here, a stub only generate fake data, he can't simulate the other aspects of
        /// a persistence layer.
        /// </summary>
        /// <param name="players"></param>
        /// <exception cref="NotImplementedException">In all cases, the exception is throw.</exception>
        /// <returns></returns>
        public Task<IEnumerable<Player>> AddPlayers(Player[] players)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Do not call this method here, a stub only generate fake data, he can't simulate the other aspects of
        /// a persistence layer.
        /// </summary>
        /// <param name="player"></param>
        /// <param name="image"></param>
        /// <param name="name"></param>
        /// <exception cref="NotImplementedException">In all cases, the exception is throw.</exception>
        /// <returns></returns>
        public Task<bool> EditPlayer(Player player, string name, string image)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Do not call this method here, a stub only generate fake data, he can't simulate the other aspects of
        /// a persistence layer.
        /// </summary>
        /// <param name="id"></param>
        /// <exception cref="NotImplementedException">In all cases, the exception is throw.</exception>
        /// <returns></returns>
        public Task<Game> GetGameFromID(int id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get a collection of game.
        /// </summary>
        /// <param name="index">This parameter is not taken in parameter for the stub.</param>
        /// <param name="count">This parameter is not taken in parameter for the stub.</param>
        /// <returns>A collection of games.</returns>
        public Task<IEnumerable<Game>> GetGames(int index, int count)
        {
            IList<Game> games = new List<Game>();
            Player p1 = new Player(2, "Elliot", "elliotImage");
            Player p2 = new Player(4, "Harvey", "harveyImage");
            Player p3 = new Player(9, "Haley", "haleyImage");
            Player p4 = new Player(1, "Baptiste", "baptisteImage");
            Player p5 = new Player(7, "Lewis", "lewisImage");

            Game game1 = new Game(new ClassicRules(), new Dictionary<Player, ScoreTable>()
            {
                { p1, scoreTableComplete1 },
                { p2, scoreTableComplete2 }
            }, true);
            
            p1.AddGame(game1);
            p2.AddGame(game1);

            Game game2 = new Game(new ClassicRules(), new Dictionary<Player, ScoreTable>()
            {
                { p2, scoreTableNotComplete1 },
                { p3, scoreTableNotComplete2 },
                { p4, scoreTableNotComplete3 },
            }, false);

            Game game3 = new Game(new ClassicRules(), new Dictionary<Player, ScoreTable>()
            {
                { p2, scoreTableComplete4 },
                { p3, scoreTableComplete5 },
                { p4, scoreTableComplete6 },
                { p5, scoreTableComplete7 },
            }, true);

            p2.AddGame(game3);
            p3.AddGame(game3);
            p4.AddGame(game3);
            p5.AddGame(game3);

            games.Add(game1);
            games.Add(game2);
            games.Add(game3);
            
            return Task.Run(() => games.AsEnumerable());
        }

        /// <summary>
        /// Do not call this method here, a stub only generate fake data, he can't simulate the other aspects of
        /// a persistence layer.
        /// </summary>
        /// <param name="player"></param>
        /// <param name="index"></param>
        /// <param name="count"></param>
        /// <exception cref="NotImplementedException">In all cases, the exception is throw.</exception>
        /// <returns></returns>
        public Task<IEnumerable<Game>> GetGamesFromPlayer(Player player, int index, int count)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Do not call this method here, a stub only generate fake data, he can't simulate the other aspects of
        /// a persistence layer.
        /// </summary>
        /// <param name="id"></param>
        /// <exception cref="NotImplementedException">In all cases, the exception is throw.</exception>
        /// <returns></returns>
        public Task<Player> GetPlayerFromID(int id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Do not call this method here, a stub only generate fake data, he can't simulate the other aspects of
        /// a persistence layer.
        /// </summary>
        /// <param name="substring"></param>
        /// <param name="count"></param>
        /// <param name="index"></param>
        /// <exception cref="NotImplementedException">In all cases, the exception is throw.</exception>
        /// <returns></returns>
        public Task<IEnumerable<Player>> GetPlayerFromName(string substring, int index, int count)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get a collection of players.
        /// </summary>
        /// <param name="index">This parameter is not taken in parameter for the stub.</param>
        /// <param name="count">This parameter is not taken in parameter for the stub.</param>
        /// <returns></returns>
        public Task<IEnumerable<Player>> GetPlayers(int index, int count)
        {
            return Task.Run(() => new Player[]
            {
                new Player(2, "Elliot", "elliotImage"),
                new Player(4, "Harvey", "harveyImage"),
                new Player(9, "Haley", "haleyImage"),
                new Player(1, "Baptiste", "baptisteImage"),
                new Player(7, "Lewis", "lewisImage"),
                new Player(33, "Franck", "franckImage"),
                new Player(10, "Leo", "leoImage"),
                new Player(15, "Willy", "willyImage"),
                new Player(11, "Marnie", "marnieImage"),
                new Player(50, "Elvis", "elvisImage"),
                new Player(45, "Emily", "emilyImage"),
                new Player(25, "Leah", "leahImage"),
                new Player(22, "Gus", "gusImage"),
                new Player(71, "Clint", "clintImage"),
                new Player(99, "Sebastian", "sebastianImage"),
            }.AsEnumerable());
        }

        /// <summary>
        /// Do not call this method here, a stub only generate fake data, he can't simulate the other aspects of
        /// a persistence layer.
        /// </summary>
        /// <param name="game"></param>
        /// <exception cref="NotImplementedException">In all cases, the exception is throw.</exception>
        /// <returns></returns>
        public Task<bool> RemoveGame(Game game)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Do not call this method here, a stub only generate fake data, he can't simulate the other aspects of
        /// a persistence layer.
        /// </summary>
        /// <param name="player"></param>
        /// <exception cref="NotImplementedException">In all cases, the exception is throw.</exception>
        /// <returns></returns>
        public Task<bool> RemovePlayer(Player player)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Fill in a score table given in parameters with the throw results specified.
        /// </summary>
        /// <param name="scoreTable">The score table to fill in.</param>
        /// <param name="throwResults">The throw results to write into.</param>
        /// <returns>The score table updated.</returns>
        private static ScoreTable ToScoreTable(ScoreTable scoreTable, ThrowResult[][] throwResults)
        {
            for (int i = 0; i < throwResults.Length; i++)
            {
                var currentFrame = throwResults[i];
                for (int j = 0; j < currentFrame.Length; j++)
                {
                    scoreTable.WriteValue(i, j, throwResults[i][j]);
                }
            }
            return scoreTable;
        }

        private static ScoreTable scoreTableComplete1 = ToScoreTable(new ScoreTable(new ClassicRules()), new ThrowResult[][]
        {
            new ThrowResult[]
            {
                ThrowResult.TWO,
                ThrowResult.ONE,
            },
            new ThrowResult[]
            {
                ThrowResult.ZERO,
                ThrowResult.EIGHT,
            },
            new ThrowResult[]
            {
                ThrowResult.FOUR,
                ThrowResult.TWO,
            },
            new ThrowResult[]
            {
                ThrowResult.NONE,
                ThrowResult.STRIKE,
            },
            new ThrowResult[]
            {
                ThrowResult.TWO,
                ThrowResult.SEVEN,
            },
            new ThrowResult[]
            {
                ThrowResult.NINE,
                ThrowResult.SPARE,
            },
            new ThrowResult[]
            {
                ThrowResult.FOUR,
                ThrowResult.THREE,
            },
            new ThrowResult[]
            {
                ThrowResult.SEVEN,
                ThrowResult.ONE,
            },
            new ThrowResult[]
            {
                ThrowResult.EIGHT,
                ThrowResult.ZERO,
            },
            new ThrowResult[]
            {
                ThrowResult.STRIKE,
                ThrowResult.STRIKE,
                ThrowResult.TWO,
            }
        });

        private ScoreTable scoreTableComplete2 = ToScoreTable(new ScoreTable(new ClassicRules()), new ThrowResult[][]
        {
            new ThrowResult[]
            {
                ThrowResult.THREE,
                ThrowResult.FIVE,
            },
            new ThrowResult[]
            {
                ThrowResult.TWO,
                ThrowResult.ZERO,
            },
            new ThrowResult[]
            {
                ThrowResult.FIVE,
                ThrowResult.SPARE,
            },
            new ThrowResult[]
            {
                ThrowResult.ZERO,
                ThrowResult.FOUR,
            },
            new ThrowResult[]
            {
                ThrowResult.ONE,
                ThrowResult.EIGHT,
            },
            new ThrowResult[]
            {
                ThrowResult.SEVEN,
                ThrowResult.ZERO,
            },
            new ThrowResult[]
            {
                ThrowResult.NONE,
                ThrowResult.STRIKE,
            },
            new ThrowResult[]
            {
                ThrowResult.THREE,
                ThrowResult.THREE,
            },
            new ThrowResult[]
            {
                ThrowResult.SIX,
                ThrowResult.ZERO,
            },
            new ThrowResult[]
            {
                ThrowResult.SEVEN,
                ThrowResult.SPARE,
                ThrowResult.FIVE,
            }
        });

        private ScoreTable scoreTableComplete3 = ToScoreTable(new ScoreTable(new ClassicRules()), new ThrowResult[][]
        {
            new ThrowResult[]
            {
                ThrowResult.SIX,
                ThrowResult.THREE,
            },
            new ThrowResult[]
            {
                ThrowResult.NINE,
                ThrowResult.SPARE,
            },
            new ThrowResult[]
            {
                ThrowResult.NONE,
                ThrowResult.STRIKE,
            },
            new ThrowResult[]
            {
                ThrowResult.ZERO,
                ThrowResult.FOUR,
            },
            new ThrowResult[]
            {
                ThrowResult.ONE,
                ThrowResult.SEVEN,
            },
            new ThrowResult[]
            {
                ThrowResult.ONE,
                ThrowResult.ONE,
            },
            new ThrowResult[]
            {
                ThrowResult.ZERO,
                ThrowResult.EIGHT,
            },
            new ThrowResult[]
            {
                ThrowResult.SIX,
                ThrowResult.TWO,
            },
            new ThrowResult[]
            {
                ThrowResult.FIVE,
                ThrowResult.FOUR,
            },
            new ThrowResult[]
            {
                ThrowResult.THREE,
                ThrowResult.SIX,
                ThrowResult.NONE,
            }
        });

        private ScoreTable scoreTableComplete4 = ToScoreTable(new ScoreTable(new ClassicRules()), new ThrowResult[][]
        {
            new ThrowResult[]
            {
                ThrowResult.NONE,
                ThrowResult.STRIKE,
            },
            new ThrowResult[]
            {
                ThrowResult.NONE,
                ThrowResult.STRIKE,
            },
            new ThrowResult[]
            {
                ThrowResult.NONE,
                ThrowResult.STRIKE,
            },
            new ThrowResult[]
            {
                ThrowResult.NONE,
                ThrowResult.STRIKE,
            },
            new ThrowResult[]
            {
                ThrowResult.NONE,
                ThrowResult.STRIKE,
            },
            new ThrowResult[]
            {
                ThrowResult.NONE,
                ThrowResult.STRIKE,
            },
            new ThrowResult[]
            {
                ThrowResult.NONE,
                ThrowResult.STRIKE,
            },
            new ThrowResult[]
            {
                ThrowResult.NONE,
                ThrowResult.STRIKE,
            },
            new ThrowResult[]
            {
                ThrowResult.NONE,
                ThrowResult.STRIKE,
            },
            new ThrowResult[]
            {
                ThrowResult.STRIKE,
                ThrowResult.STRIKE,
                ThrowResult.STRIKE,
            }
        });

        private ScoreTable scoreTableComplete5 = ToScoreTable(new ScoreTable(new ClassicRules()), new ThrowResult[][]
        {
            new ThrowResult[]
            {
                ThrowResult.FIVE,
                ThrowResult.FOUR,
            },
            new ThrowResult[]
            {
                ThrowResult.NONE,
                ThrowResult.STRIKE,
            },
            new ThrowResult[]
            {
                ThrowResult.THREE,
                ThrowResult.ZERO,
            },
            new ThrowResult[]
            {
                ThrowResult.EIGHT,
                ThrowResult.ONE,
            },
            new ThrowResult[]
            {
                ThrowResult.SEVEN,
                ThrowResult.TWO,
            },
            new ThrowResult[]
            {
                ThrowResult.ZERO,
                ThrowResult.SIX,
            },
            new ThrowResult[]
            {
                ThrowResult.FIVE,
                ThrowResult.THREE,
            },
            new ThrowResult[]
            {
                ThrowResult.TWO,
                ThrowResult.FOUR,
            },
            new ThrowResult[]
            {
                ThrowResult.SIX,
                ThrowResult.ZERO,
            },
            new ThrowResult[]
            {
                ThrowResult.FOUR,
                ThrowResult.SPARE,
                ThrowResult.FOUR,
            }
        });

        private ScoreTable scoreTableComplete6 = ToScoreTable(new ScoreTable(new ClassicRules()), new ThrowResult[][]
        {
            new ThrowResult[]
            {
                ThrowResult.NINE,
                ThrowResult.ZERO,
            },
            new ThrowResult[]
            {
                ThrowResult.ZERO,
                ThrowResult.NINE,
            },
            new ThrowResult[]
            {
                ThrowResult.THREE,
                ThrowResult.FOUR,
            },
            new ThrowResult[]
            {
                ThrowResult.SEVEN,
                ThrowResult.ONE,
            },
            new ThrowResult[]
            {
                ThrowResult.TWO,
                ThrowResult.FIVE,
            },
            new ThrowResult[]
            {
                ThrowResult.EIGHT,
                ThrowResult.SPARE,
            },
            new ThrowResult[]
            {
                ThrowResult.ZERO,
                ThrowResult.ZERO,
            },
            new ThrowResult[]
            {
                ThrowResult.NONE,
                ThrowResult.STRIKE,
            },
            new ThrowResult[]
            {
                ThrowResult.SIX,
                ThrowResult.TWO,
            },
            new ThrowResult[]
            {
                ThrowResult.STRIKE,
                ThrowResult.TWO,
                ThrowResult.FIVE,
            }
        });

        private ScoreTable scoreTableComplete7 = ToScoreTable(new ScoreTable(new ClassicRules()), new ThrowResult[][]
        {
            new ThrowResult[]
            {
                ThrowResult.EIGHT,
                ThrowResult.ZERO,
            },
            new ThrowResult[]
            {
                ThrowResult.SIX,
                ThrowResult.TWO,
            },
            new ThrowResult[]
            {
                ThrowResult.FOUR,
                ThrowResult.FOUR,
            },
            new ThrowResult[]
            {
                ThrowResult.THREE,
                ThrowResult.THREE,
            },
            new ThrowResult[]
            {
                ThrowResult.NONE,
                ThrowResult.STRIKE,
            },
            new ThrowResult[]
            {
                ThrowResult.FIVE,
                ThrowResult.THREE,
            },
            new ThrowResult[]
            {
                ThrowResult.ONE,
                ThrowResult.FIVE,
            },
            new ThrowResult[]
            {
                ThrowResult.ZERO,
                ThrowResult.SPARE,
            },
            new ThrowResult[]
            {
                ThrowResult.FIVE,
                ThrowResult.THREE,
            },
            new ThrowResult[]
            {
                ThrowResult.STRIKE,
                ThrowResult.STRIKE,
                ThrowResult.FIVE,
            }
        });

        private ScoreTable scoreTableNotComplete1 = ToScoreTable(new ScoreTable(new ClassicRules()), new ThrowResult[][]
        {
            new ThrowResult[]
            {
                ThrowResult.FOUR,
                ThrowResult.SPARE,
            },
            new ThrowResult[]
            {
                ThrowResult.NONE,
                ThrowResult.STRIKE,
            },
            new ThrowResult[]
            {
                ThrowResult.FOUR,
                ThrowResult.TWO,
            },
            new ThrowResult[]
            {
                ThrowResult.NONE,
                ThrowResult.STRIKE,
            },
            new ThrowResult[]
            {
                ThrowResult.NINE,
                ThrowResult.ZERO,
            },
            new ThrowResult[]
            {
                ThrowResult.ZERO,
                ThrowResult.SPARE,
            },
            new ThrowResult[]
            {
                ThrowResult.FIVE,
                ThrowResult.FOUR,
            },
            new ThrowResult[]
            {
                ThrowResult.SEVEN,
                ThrowResult.TWO,
            },
            new ThrowResult[]
            {
                ThrowResult.NONE,
                ThrowResult.NONE,
            },
            new ThrowResult[]
            {
                ThrowResult.NONE,
                ThrowResult.NONE,
                ThrowResult.NONE,
            }
        });

        private ScoreTable scoreTableNotComplete2 = ToScoreTable(new ScoreTable(new ClassicRules()), new ThrowResult[][]
        {
            new ThrowResult[]
            {
                ThrowResult.THREE,
                ThrowResult.SIX,
            },
            new ThrowResult[]
            {
                ThrowResult.SIX,
                ThrowResult.THREE,
            },
            new ThrowResult[]
            {
                ThrowResult.ZERO,
                ThrowResult.ONE,
            },
            new ThrowResult[]
            {
                ThrowResult.NINE,
                ThrowResult.ZERO,
            },
            new ThrowResult[]
            {
                ThrowResult.ZERO,
                ThrowResult.SPARE,
            },
            new ThrowResult[]
            {
                ThrowResult.NONE,
                ThrowResult.STRIKE,
            },
            new ThrowResult[]
            {
                ThrowResult.EIGHT,
                ThrowResult.ZERO,
            },
            new ThrowResult[]
            {
                ThrowResult.TWO,
                ThrowResult.NONE,
            },
            new ThrowResult[]
            {
                ThrowResult.NONE,
                ThrowResult.NONE,
            },
            new ThrowResult[]
            {
                ThrowResult.NONE,
                ThrowResult.NONE,
                ThrowResult.NONE,
            }
        });

        private ScoreTable scoreTableNotComplete3 = ToScoreTable(new ScoreTable(new ClassicRules()), new ThrowResult[][]
        {
            new ThrowResult[]
            {
                ThrowResult.NONE,
                ThrowResult.STRIKE,
            },
            new ThrowResult[]
            {
                ThrowResult.NONE,
                ThrowResult.STRIKE,
            },
            new ThrowResult[]
            {
                ThrowResult.NINE,
                ThrowResult.ZERO,
            },
            new ThrowResult[]
            {
                ThrowResult.EIGHT,
                ThrowResult.ONE,
            },
            new ThrowResult[]
            {
                ThrowResult.NONE,
                ThrowResult.STRIKE,
            },
            new ThrowResult[]
            {
                ThrowResult.TWO,
                ThrowResult.TWO,
            },
            new ThrowResult[]
            {
                ThrowResult.ZERO,
                ThrowResult.SPARE,
            },
            new ThrowResult[]
            {
                ThrowResult.NONE,
                ThrowResult.NONE,
            },
            new ThrowResult[]
            {
                ThrowResult.NONE,
                ThrowResult.NONE,
            },
            new ThrowResult[]
            {
                ThrowResult.NONE,
                ThrowResult.NONE,
                ThrowResult.NONE,
            }
        });
    }
}
