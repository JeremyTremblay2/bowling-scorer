using Model.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Sdk;

namespace UnitTests.Players
{
    public class PlayerDataTest
    {
        private static readonly int[] ids = Enumerable.Range(1, 6).ToArray();

        public static IEnumerable<object[]> Test_PlayerDataEquality()
        {
            yield return new object[] {
                new Player[] {
                    new(ids[0], "Jean", "linkToImage"),
                    new(ids[0], "Francis", "otherLink"),
                    new(ids[4], "Steeve", "randomImage"),
                    new(ids[1], "Patrick", "catImage"),
                    new(ids[3], "Camille", "dogImage"),
                    new(ids[1], "Elise", "birdImage"),
                    new(ids[2], "Monica", "waleImage"),
                    new(ids[3], "Tibault", "frogImage"),
                }
            };
        }

        public static IEnumerable<object[]> Test_PlayerDataComparison()
        {
            yield return new object[] {
                new Player[] {
                    new("Adrien", "birdImage"),
                    new("Ambre", "otherDoc"),
                    new("Elliot", "otherLink"),
                    new("Florence", "dogImage"),
                    new("Mathéo", "frogImage"),
                    new("Robin", "catImage"),
                    new("Samuel", "waleImage"),
                    new("William", "randomImage"),
                    new("Yann", "linkToImage"),
                },
                new Player[] {
                    new("Yann", "linkToImage"),
                    new("Elliot", "otherLink"),
                    new("Ambre", "otherDoc"),
                    new("William", "randomImage"),
                    new("Robin", "catImage"),
                    new("Florence", "dogImage"),
                    new("Adrien", "birdImage"),
                    new("Samuel", "waleImage"),
                    new("Mathéo", "frogImage"),
                },

            };
            yield return new object[] {
                new Player[] {
                    new("Aurélien", "otherDoc"),
                    new("Benjamein", "otherLink"),
                    new("Claire", "dogImage"),
                    new("Dylan", "randomImage"),
                    new("Elisa", "frogImage"),
                    new("Guillaume", "linkToImage"),
                    new("Rose", "birdImage"),
                    new("Sofia", "waleImage"),
                    new("Tristan", "catImage"),
                },
                new Player[] {
                    new("Guillaume", "linkToImage"),
                    new("Benjamein", "otherLink"),
                    new("Aurélien", "otherDoc"),
                    new("Dylan", "randomImage"),
                    new("Tristan", "catImage"),
                    new("Claire", "dogImage"),
                    new("Rose", "birdImage"),
                    new("Sofia", "waleImage"),
                    new("Elisa", "frogImage"),
                },
            };
        }
    }
}
