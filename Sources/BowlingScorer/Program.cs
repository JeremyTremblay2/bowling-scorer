using Model.Games;
using Stub;

namespace BowlingScorer
{
    public class Program
    {
        static void Main(string[] args)
        {
            Stub.Stub stub = new();
            IList<Game>? games = stub.GetGames() as IList<Game>;
            
            foreach (Game game in games){
                Console.WriteLine(game.ToString());
            }
        }
    }
}
