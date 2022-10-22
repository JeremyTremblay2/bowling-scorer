using Model.Games;
using Model.Players;
using Model.Score.Rules;
using Model.Score;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTests.Games;

namespace UnitTests.Model.Players
{
    public class StatisticsDataTest
    {
        // Winner: Clint, Best score: 104
        public static Game gameFinished = new Game(new ClassicRules(), new Dictionary<Player, ScoreTable>
        {
            { new Player(21, "Clint", "clintImage"), GameDataTest.scoreTableComplete },
            { new Player(14, "Haley", "haleyImage"), GameDataTest.anotherScoreTableComplete },
        }, true, 23);

        // Winner: Haley, Best score: 104
        public static Game gameFinished2 = new Game(new ClassicRules(), new Dictionary<Player, ScoreTable>
        {
            { new Player(21, "Clint", "clintImage"), GameDataTest.anotherScoreTableComplete },
            { new Player(14, "Haley", "haleyImage"), GameDataTest.scoreTableComplete },
        }, true, 36);

        public static Game gameNotFinished = new Game(new ClassicRules(), new Dictionary<Player, ScoreTable>
        {
            { new Player(14, "Haley", "haleyImage"), GameDataTest.scoreTableComplete },
        }, false, 12);

        public static Player playerEmpty = new Player(18, "Abigail", "abiImage");

        public static Player player83 = new Player(14, "Gus", "gusImage");

        public static Player player104 = new Player(21, "Clint", "clintImage");

        static StatisticsDataTest() {
            player104.AddGame(gameFinished);
            player83.AddGame(gameFinished);
        }

        public static IEnumerable<object[]> Test_StatisticsDataAddGame()
        {
            Player player83 = new Player(14, "Gus", "gusImage");
            Player player104 = new Player(21, "Clint", "clintImage");
            player104.AddGame(gameFinished);
            player83.AddGame(gameFinished);
            yield return new object[] {
                true,
                1,
                1,
                0,
                104,
                20,
                5.0,
                104,
                new Player(21, "Clint", "clintImage"),
                new Game[]
                {
                    gameFinished
                }
            };

            yield return new object[] {
                true,
                1,
                0,
                1,
                83,
                20,
                4.4,
                83,
                new Player(14, "Haley", "haleyImage"),
                new Game[]
                {
                    gameFinished
                }
            };

            yield return new object[] {
                true,
                2,
                1,
                1,
                104,
                40,
                4.7,
                93.5,
                player83,
                new Game[]
                {
                    gameFinished2
                }
            };

            yield return new object[] {
                false,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                new Player(14, "Haley", "haleyImage"),
                new Game[]
                {
                    gameNotFinished
                }
            };

            yield return new object[] {
                false,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                new Player(14, "Haley", "haleyImage"),
                new Game[]
                {
                    null
                }
            };

            // Non existing player
            yield return new object[] {
                false,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                new Player(15, "David", "davidImage"),
                new Game[]
                {
                    gameNotFinished
                }
            };

            yield return new object[] {
                false,
                1,
                1,
                0,
                104,
                20,
                5.0,
                104,
                player104,
                new Game[]
                {
                    gameFinished,
                }
            };
        }

        public static IEnumerable<object[]> Test_StatisticsRemoveAddGame()
        {
            Player player83 = new Player(14, "Gus", "gusImage");
            Player player104 = new Player(21, "Clint", "clintImage");
            player104.AddGame(gameFinished);
            player83.AddGame(gameFinished);
            player83.AddGame(gameFinished2);
            yield return new object[] {
                true,
                1,
                0,
                1,
                83,
                20,
                4.4,
                83,
                player83,
                new Game[]
                {
                    gameFinished2
                }
            };

            yield return new object[] {
                true,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                player104,
                new Game[]
                {
                    gameFinished
                }
            };

            yield return new object[] {
                false,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                player104,
                new Game[]
                {
                    null
                }
            };

            yield return new object[] {
                false,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                new Player(2, "Toto", "totoImage"),
                new Game[]
                {
                    null
                }
            };

            yield return new object[] {
                false,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                player104,
                new Game[]
                {
                    gameNotFinished
                }
            };
        }


        public static IEnumerable<object[]> Test_StatisticsDataEquality()
        {
            Player playerEmpty = new Player(18, "Abigail", "abiImage");
            Player playerEmpty2 = new Player(18, "Abigail", "abiImage");
            Player player83 = new Player(14, "Gus", "gusImage");
            Player player832 = new Player(14, "Gus", "gusImage");
            Player player104 = new Player(21, "Clint", "clintImage");
            player104.AddGame(gameFinished);
            player83.AddGame(gameFinished);
            player832.AddGame(gameFinished);

            yield return new object[] {
                true,
                playerEmpty.Statistics,
                playerEmpty.Statistics
            };

            yield return new object[] {
                true,
                player83.Statistics,
                player83.Statistics
            };

            yield return new object[] {
                true,
                playerEmpty.Statistics,
                playerEmpty2.Statistics
            };

            yield return new object[] {
                true,
                player83.Statistics,
                player832.Statistics
            };

            yield return new object[] {
                false,
                player104.Statistics,
                player832.Statistics
            };

            yield return new object[] {
                false,
                player832.Statistics,
                playerEmpty.Statistics
            };

            yield return new object[] {
                false,
                player83.Statistics,
                playerEmpty.Statistics
            };

            yield return new object[] {
                false,
                player832.Statistics,
                player104.Statistics
            };

            yield return new object[] {
                false,
                player104.Statistics,
                playerEmpty.Statistics
            };

            yield return new object[] {
                false,
                null,
                playerEmpty.Statistics
            };

            yield return new object[] {
                false,
                player104.Statistics,
                null
            };

            yield return new object[] {
                true,
                null,
                null
            };
        }




    }
}
