using Model.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.Players
{
    public class PlayerManagerDataTest
    {
        public static IEnumerable<object[]> Data_AddPlayerToManager()
        {
            // Add a player non existing in the manager.
            yield return new object[] {
                true,
                new Player[]
                {
                    new Player(1, "Baptiste", "baptisteImage"),
                    new Player(2, "Elliot", "elliotImage"),
                    new Player(4, "Harvey", "harveyImage"),
                    new Player(7, "Lewis", "lewisImage"),
                    new Player(9, "Haley", "haleyImage"),
                },
                new PlayerManager(
                    new Player(1, "Baptiste", "baptisteImage"),
                    new Player(2, "Elliot", "elliotImage"),
                    new Player(7, "Lewis", "lewisImage"),
                    new Player(9, "Haley", "haleyImage")
                ),
                new Player(4, "Harvey", "harveyImage"),
            };

            // Add a player non existing in the manager.
            yield return new object[] {
                true,
                new Player[]
                {
                    new Player(9, "Franck", "franckImage"),
                    new Player(10, "Leo", "leoImage"),
                    new Player(11, "Marnie", "marnieImage"),
                    new Player(15, "Willy", "willyImage"),
                    new Player(50, "Elvis", "elvisImage"),
                },
                new PlayerManager(
                    new Player(10, "Leo", "leoImage"),
                    new Player(11, "Marnie", "marnieImage"),
                    new Player(15, "Willy", "willyImage"),
                    new Player(50, "Elvis", "elvisImage")
                ),
                new Player(9, "Franck", "franckImage"),
            };

            // Add a player existing in the manager.
            yield return new object[] {
                false,
                new Player[]
                {
                    new Player(45, "Emily", "emilyImage"),
                    new Player(25, "Leah", "leahImage"),
                    new Player(22, "Gus", "gusImage"),
                    new Player(71, "Clint", "clintImage"),
                    new Player(99, "Sebastian", "sebastianImage"),
                },
                new PlayerManager(
                    new Player(45, "Emily", "emilyImage"),
                    new Player(25, "Leah", "leahImage"),
                    new Player(22, "Gus", "gusImage"),
                    new Player(71, "Clint", "clintImage"),
                    new Player(99, "Sebastian", "sebastianImage")
                ),
                new Player(22, "Gus", "gusImage"),
            };

            // Add a null value.
            yield return new object[] {
                false,
                new Player[]
                {
                    new Player(12, "Marlon", "marlonImage"),
                    new Player(45, "Vincent", "vincentImage"),
                    new Player(85, "Jas", "jasImage"),
                    new Player(75, "Pierre", "pierreImage"),
                    new Player(43, "Maru", "maruImage"),
                },
                new PlayerManager(
                    new Player(12, "Marlon", "marlonImage"),
                    new Player(45, "Vincent", "vincentImage"),
                    new Player(85, "Jas", "jasImage"),
                    new Player(75, "Pierre", "pierreImage"),
                    new Player(43, "Maru", "maruImage")
                ),
                null,
            };

            // Add a non existing player with same ID as an other player already existing.
            yield return new object[] {
                false,
                new Player[]
                {
                    new Player(12, "Marlon", "marlonImage"),
                    new Player(45, "Vincent", "vincentImage"),
                    new Player(85, "Jas", "jasImage"),
                    new Player(75, "Pierre", "pierreImage"),
                    new Player(43, "Maru", "maruImage"),
                },
                new PlayerManager(
                    new Player(12, "Marlon", "marlonImage"),
                    new Player(45, "Vincent", "vincentImage"),
                    new Player(85, "Jas", "jasImage"),
                    new Player(75, "Pierre", "pierreImage"),
                    new Player(43, "Maru", "maruImage")
                ),
                new Player(85, "SpiderMan", "spiderManImage")
            };
        }

        public static IEnumerable<object[]> Data_AddMultiplePlayersToManager()
        {
            // Add one player, not present in the manager.
            yield return new object[] {
                1,
                new Player[]
                {
                    new Player(1, "Baptiste", "baptisteImage"),
                    new Player(2, "Elliot", "elliotImage"),
                    new Player(4, "Harvey", "harveyImage"),
                    new Player(7, "Lewis", "lewisImage"),
                    new Player(9, "Haley", "haleyImage"),
                    new Player(8, "Franck", "franckImage"),
                    new Player(10, "Leo", "leoImage"),
                    new Player(11, "Marnie", "marnieImage"),
                    new Player(15, "Willy", "willyImage"),
                    new Player(50, "Elvis", "elvisImage"),
                },
                new Player[]
                {
                    new Player(7, "Lewis", "lewisImage"),
                },
                new PlayerManager(
                    new Player(1, "Baptiste", "baptisteImage"),
                    new Player(2, "Elliot", "elliotImage"),
                    new Player(4, "Harvey", "harveyImage"),
                    new Player(9, "Haley", "haleyImage"),
                    new Player(8, "Franck", "franckImage"),
                    new Player(10, "Leo", "leoImage"),
                    new Player(11, "Marnie", "marnieImage"),
                    new Player(50, "Elvis", "elvisImage"),
                    new Player(15, "Willy", "willyImage")
                ),
                new Player[]
                {
                    new Player(7, "Lewis", "lewisImage"),
                }
            };

            //Add null values and two players not present in the manager.
            yield return new object[] {
                2,
                new Player[]
                {
                    new Player(1, "Baptiste", "baptisteImage"),
                    new Player(2, "Elliot", "elliotImage"),
                    new Player(4, "Harvey", "harveyImage"),
                    new Player(7, "Lewis", "lewisImage"),
                    new Player(9, "Haley", "haleyImage"),
                    new Player(8, "Franck", "franckImage"),
                    new Player(10, "Leo", "leoImage"),
                    new Player(11, "Marnie", "marnieImage"),
                    new Player(15, "Willy", "willyImage"),
                    new Player(50, "Elvis", "elvisImage"),
                },
                new Player[]
                {
                    new Player(15, "Willy", "willyImage"),
                    new Player(7, "Lewis", "lewisImage"),
                },
                new PlayerManager(
                    new Player(1, "Baptiste", "baptisteImage"),
                    new Player(2, "Elliot", "elliotImage"),
                    new Player(4, "Harvey", "harveyImage"),
                    new Player(9, "Haley", "haleyImage"),
                    new Player(8, "Franck", "franckImage"),
                    new Player(10, "Leo", "leoImage"),
                    new Player(11, "Marnie", "marnieImage"),
                    new Player(50, "Elvis", "elvisImage")
                ),
                new Player[]
                {
                    new Player(15, "Willy", "willyImage"),
                    null,
                    new Player(7, "Lewis", "lewisImage"),
                    null,
                    null
                }
            };

            // Add players in multiple copies, and three new players.
            yield return new object[] {
                3,
                new Player[]
                {
                    new Player(1, "Baptiste", "baptisteImage"),
                    new Player(2, "Elliot", "elliotImage"),
                    new Player(4, "Harvey", "harveyImage"),
                    new Player(7, "Lewis", "lewisImage"),
                    new Player(9, "Haley", "haleyImage"),
                    new Player(8, "Franck", "franckImage"),
                    new Player(10, "Leo", "leoImage"),
                    new Player(11, "Marnie", "marnieImage"),
                    new Player(15, "Willy", "willyImage"),
                    new Player(50, "Elvis", "elvisImage"),
                },
                new Player[]
                {
                    new Player(15, "Willy", "willyImage"),
                    new Player(7, "Lewis", "lewisImage"),
                    new Player(9, "Haley", "haleyImage"),
                },
                new PlayerManager(
                    new Player(1, "Baptiste", "baptisteImage"),
                    new Player(2, "Elliot", "elliotImage"),
                    new Player(4, "Harvey", "harveyImage"),
                    new Player(8, "Franck", "franckImage"),
                    new Player(10, "Leo", "leoImage"),
                    new Player(11, "Marnie", "marnieImage"),
                    new Player(50, "Elvis", "elvisImage")
                ),
                new Player[]
                {
                    new Player(15, "Willy", "willyImage"),
                    new Player(15, "Willy", "willyImage"),
                    new Player(7, "Lewis", "lewisImage"),
                    new Player(2, "Elliot", "elliotImage"),
                    new Player(2, "Elliot", "elliotImage"),
                    new Player(9, "Haley", "haleyImage"),
                }
            };

            // Add multiple null values, multiple player copies and differents players but with same ID.
            yield return new object[] {
                4,
                new Player[]
                {
                    new Player(12, "Marlon", "marlonImage"),
                    new Player(45, "Vincent", "vincentImage"),
                    new Player(85, "Jas", "jasImage"),
                    new Player(75, "Pierre", "pierreImage"),
                    new Player(43, "Maru", "maruImage"),
                    new Player(45, "Emily", "emilyImage"),
                    new Player(25, "Leah", "leahImage"),
                    new Player(22, "Gus", "gusImage"),
                    new Player(71, "Clint", "clintImage"),
                    new Player(99, "Sebastian", "sebastianImage"),
                },
                new Player[]
                {
                    new Player(25, "Leah", "leahImage"),
                    new Player(43, "Maru", "maruImage"),
                    new Player(75, "Pierre", "pierreImage"),
                    new Player(12, "Marlon", "marlonImage"),
                },
                new PlayerManager(
                    new Player(45, "Vincent", "vincentImage"),
                    new Player(85, "Jas", "jasImage"),
                    new Player(45, "Emily", "emilyImage"),
                    new Player(22, "Gus", "gusImage"),
                    new Player(71, "Clint", "clintImage"),
                    new Player(99, "Sebastian", "sebastianImage")
                ),
                new Player[]
                {
                    new Player(25, "Leah", "leahImage"),
                    new Player(43, "Maru", "maruImage"),
                    null,
                    new Player(43, "Maru", "maruImage"), // Same ID as previous.
                    new Player(75, "Pierre", "pierreImage"),
                    new Player(22, "Other person", "randomImage"), // Same ID as Gus
                    new Player(12, "Marlon", "marlonImage"),
                    null
                }
            };
        }

        public static IEnumerable<object[]> Data_EqualsManagers()
        {
            // Create two managers identicals and empty.
            yield return new object[] {
                true,
                new PlayerManager(

                ),
                new PlayerManager(

                ),
            };

            // Create two managers identicals and add some data to them.
            yield return new object[] {
                true,
                new PlayerManager(
                    new Player(12, "Marlon", "marlonImage"),
                    new Player(45, "Vincent", "vincentImage"),
                    new Player(85, "Jas", "jasImage"),
                    new Player(75, "Pierre", "pierreImage"),
                    new Player(43, "Maru", "maruImage"),
                    new Player(45, "Emily", "emilyImage"),
                    new Player(25, "Leah", "leahImage"),
                    new Player(22, "Gus", "gusImage"),
                    new Player(71, "Clint", "clintImage"),
                    new Player(99, "Sebastian", "sebastianImage")
                ),
                new PlayerManager(
                    new Player(12, "Marlon", "marlonImage"),
                    new Player(45, "Vincent", "vincentImage"),
                    new Player(85, "Jas", "jasImage"),
                    new Player(75, "Pierre", "pierreImage"),
                    new Player(43, "Maru", "maruImage"),
                    new Player(45, "Emily", "emilyImage"),
                    new Player(25, "Leah", "leahImage"),
                    new Player(22, "Gus", "gusImage"),
                    new Player(71, "Clint", "clintImage"),
                    new Player(99, "Sebastian", "sebastianImage")
                ),
            };

            // Create two managers identicals and add some data to them, but data is shuffle.
            yield return new object[] {
                true,
                new PlayerManager(
                    new Player(25, "Leah", "leahImage"),
                    new Player(12, "Marlon", "marlonImage"),
                    new Player(99, "Sebastian", "sebastianImage"),
                    new Player(45, "Vincent", "vincentImage"),
                    new Player(85, "Jas", "jasImage"),
                    new Player(22, "Gus", "gusImage"),
                    new Player(71, "Clint", "clintImage"),
                    new Player(75, "Pierre", "pierreImage"),
                    new Player(43, "Maru", "maruImage"),
                    new Player(45, "Emily", "emilyImage")
                ),
                new PlayerManager(
                    new Player(12, "Marlon", "marlonImage"),
                    new Player(45, "Vincent", "vincentImage"),
                    new Player(85, "Jas", "jasImage"),
                    new Player(75, "Pierre", "pierreImage"),
                    new Player(43, "Maru", "maruImage"),
                    new Player(45, "Emily", "emilyImage"),
                    new Player(25, "Leah", "leahImage"),
                    new Player(22, "Gus", "gusImage"),
                    new Player(71, "Clint", "clintImage"),
                    new Player(99, "Sebastian", "sebastianImage")
                ),
            };

            // Create two managers differents, one has data and not the other.
            yield return new object[] {
                false,
                new PlayerManager(
                    new Player(12, "Marlon", "marlonImage")
                ),
                new PlayerManager(

                ),
            };

            // Create two managers differents, one has data and not the other.
            yield return new object[] {
                false,
                new PlayerManager(

                ),
                new PlayerManager(
                    new Player(22, "Gus", "gusImage"),
                    new Player(71, "Clint", "clintImage"),
                    new Player(99, "Sebastian", "sebastianImage")
                ),
            };

            // Create two managers differents, same amount of data but one has different data from the other.
            yield return new object[] {
                false,
                new PlayerManager(
                    new Player(22, "Gus", "gusImage"),
                    new Player(71, "Clint", "clintImage"),
                    new Player(99, "Sebastian", "sebastianImage")
                ),
                new PlayerManager(
                    new Player(43, "Maru", "maruImage"),
                    new Player(45, "Emily", "emilyImage"),
                    new Player(25, "Leah", "leahImage")
                ),
            };

            // Create two managers differents, same amount of data but one player is different.
            yield return new object[] {
                false,
                new PlayerManager(
                    new Player(43, "Maru", "maruImage"),
                    new Player(45, "Emily", "emilyImage"),
                    new Player(22, "Leah", "leahImage") //the id is not the same
                ),
                new PlayerManager(
                    new Player(43, "Maru", "maruImage"),
                    new Player(45, "Emily", "emilyImage"),
                    new Player(25, "Leah", "leahImage")
                ),
            };
        }

        public static IEnumerable<object[]> Data_RemovePlayers()
        {
            // Should remove the player.
            yield return new object[] {
                true,
                new PlayerManager(
                    new Player(22, "Gus", "gusImage"),
                    new Player(71, "Clint", "clintImage"),
                    new Player(99, "Sebastian", "sebastianImage")
                ),
                new Player(99, "Sebastian", "sebastianImage")
            };

            // Should remove the player.
            yield return new object[] {
                true,
                new PlayerManager(
                    new Player(22, "Gus", "gusImage"),
                    new Player(71, "Clint", "clintImage"),
                    new Player(99, "Sebastian", "sebastianImage")
                ),
                new Player(71, "Clint", "clintImage"),
            };

            // Should not remove the null value provided.
            yield return new object[] {
                false,
                new PlayerManager(
                    new Player(22, "Gus", "gusImage"),
                    new Player(71, "Clint", "clintImage"),
                    new Player(99, "Sebastian", "sebastianImage")
                ),
                null
            };

            // Should not remove the inexisting player provided.
            yield return new object[] {
                false,
                new PlayerManager(
                    new Player(22, "Gus", "gusImage"),
                    new Player(71, "Clint", "clintImage"),
                    new Player(99, "Sebastian", "sebastianImage")
                ),
                new Player(45, "Emily", "emilyImage"),
            };

            // Should remove the player with the same ID.
            yield return new object[] {
                true,
                new PlayerManager(
                    new Player(22, "Gus", "gusImage"),
                    new Player(71, "Clint", "clintImage"),
                    new Player(99, "Sebastian", "sebastianImage")
                ),
                new Player(71, "Emily", "emilyImage"), // She has the same ID as Clint.
            };
        }

        public static IEnumerable<object[]> Data_FindPlayersFromID()
        {
            // Should retrieve the player.
            yield return new object[] {
                new Player(99, "Sebastian", "sebastianImage"),
                new PlayerManager(
                    new Player(22, "Gus", "gusImage"),
                    new Player(71, "Clint", "clintImage"),
                    new Player(99, "Sebastian", "sebastianImage")
                ),
                99,
            };

            // Should retrieve the player.
            yield return new object[] {
                new Player(71, "Clint", "clintImage"),
                new PlayerManager(
                    new Player(22, "Gus", "gusImage"),
                    new Player(71, "Clint", "clintImage"),
                    new Player(99, "Sebastian", "sebastianImage")
                ),
                71,
            };

            // Should not retrieve a non-existing player.
            yield return new object[] {
                null,
                new PlayerManager(
                    new Player(22, "Gus", "gusImage"),
                    new Player(71, "Clint", "clintImage"),
                    new Player(99, "Sebastian", "sebastianImage")
                ),
                23, //does not exists.
            };
        }
    }
}
