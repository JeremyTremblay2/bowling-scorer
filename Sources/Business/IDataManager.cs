using Model.Games;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    /// <summary>
    /// Interface used by DataManagers, allow the developer to choose the persistance mode by using
    /// polymorphism (strategy)
    /// </summary>
    public interface IDataManager
    {
        /// <summary>
        /// Add a game that will be saved by the persistance layer
        /// </summary>
        /// <param name="game"></param>
        public void AddGame(Game game);

        /// <summary>
        /// Return all the existing loaded games
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Game> GetGames();
    }
}
