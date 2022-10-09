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
        }
    }
}
