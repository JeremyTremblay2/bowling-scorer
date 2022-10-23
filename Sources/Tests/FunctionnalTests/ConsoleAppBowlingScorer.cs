using static System.Console;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business;
using Entity2Model;
using System.Diagnostics.Metrics;
using Model.Players;

namespace FunctionnalTests
{
    public static class ConsoleAppBowlingScorer
    {
        private static BowlingManager manager = new BowlingManager(new DbDataManager());

        public static void StartConsoleApp()
        {
            WriteLine("Welcome to Bowling Scorer ! ");
            LoopMenu();
            WriteLine("Goodbye !");
        }

        private static void LoopMenu()
        {
            bool inMenu = true;
            while (inMenu)
            {
                DisplayMenu();
                int response = ReadInteger(), count, index;
                IEnumerable<Player> players;
                switch (response)
                {
                    case 1:
                        ManagePlayerMenu();
                        continue;
                    case 2:
                        WriteLine("The feature will be available soon.");
                        break;
                    case 3:
                        WriteLine("Enter the player's name:");
                        string name = ReadLine();
                        WriteLine("Enter the player's image:");
                        string image = ReadLine();
                        bool result = manager.AddPlayer(new Player(name, image)).Result;
                        if (result) WriteLine("Player added !"); else WriteLine("Error during the addition of the player");
                        break;
                    case 4:
                        WriteLine("The feature will be available soon.");
                        break;
                    case 5:
                        inMenu = false;
                        continue;
                    default:
                        WriteLine("Invalid choice.");
                        break;
                }
                WriteLine("Press enter to continue...");
                ReadLine();
            }
        }

        private static void ManagePlayerMenu()
        {
            bool inMenu = true;
            while (inMenu)
            {
                DisplayMenuPlayers();
                int response = ReadInteger(), count, index;
                IEnumerable<Player> players;
                switch (response)
                {
                    case 1:
                        (index, count) = GetIndexAndCount();
                        if (index == -1 || count == -1) break;
                        players = manager.GetPlayers(index, count).Result;
                        DisplayObjects(players);
                        break;
                    case 2:
                        WriteLine("Enter Players ID (> 0): ");
                        int id = ReadInteger();
                        if (id <= 0)
                        {
                            WriteLine("Wrong ID, aborting.");
                            break;
                        }
                        Player p = manager.GetPlayerFromID(id).Result;
                        WriteLine(p);
                        break;
                    case 3:
                        (index, count) = GetIndexAndCount();
                        if (index == -1 || count == -1) break;
                        WriteLine("Enter substring from player's name: ");
                        string substring = ReadLine();
                        players = manager.GetPlayerFromName(substring, index, count).Result;
                        DisplayObjects(players);
                        break;
                    case 4:
                        inMenu = false;
                        continue;
                    default:
                        break;
                }
                WriteLine("Press enter to continue...");
                ReadLine();
            }
        }

        private static void DisplayMenu()
        {
            Clear();
            WriteLine("Please select an option: ");
            WriteLine("1 - Show players");
            WriteLine("2 - Show games");
            WriteLine("3 - Add player");
            WriteLine("4 - Add game");
            WriteLine("5 - Exit");
            WriteLine("---------------------------------------------");
        }

        private static void DisplayMenuPlayers()
        {
            Clear();
            WriteLine("Please select an option: ");
            WriteLine("1 - Show players from range");
            WriteLine("2 - Show player from ID.");
            WriteLine("3 - Show player from substring");
            WriteLine("4 - Back");
            WriteLine("---------------------------------------------");
        }

        private static int ReadInteger()
        {
            if (!int.TryParse(ReadLine(), out int number))
            {
                WriteLine("Please enter an integer.");
                return -1;
            }
            return number;
        }

        private static (int index, int count) GetIndexAndCount()
        {
            WriteLine("Enter Count: ");
            int count = ReadInteger();
            if (count <= 0)
            {
                WriteLine("Wrong count, aborting.");
                return (-1, -1);
            }
            WriteLine("Enter index: ");
            int index = ReadInteger();
            if (index < 0)
            {
                WriteLine("Wrong index, aborting.");
                return (-1, -1);
            }
            return (index, count);
        }

        private static void DisplayObjects(IEnumerable<object> objects)
        {
            foreach (object obj in objects)
            {
                WriteLine(obj.ToString());
            }
        }
    }
}
