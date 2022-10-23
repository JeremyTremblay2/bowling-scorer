using Model.Games;
using Model.Players;
using System.Text;
using Microsoft.Extensions.Logging;
using NullLogger = Microsoft.Extensions.Logging.Abstractions.NullLogger;
using ILogger = Microsoft.Extensions.Logging.ILogger;
using System.Collections.ObjectModel;

namespace Business
{
    /// <summary>
    /// The BowlingManager is the main access point to the Bowling app.
    /// It contains a DataManager capable of performing addition, deletion and modification on the collections.
    /// It is simply a facade, which relays each action to another manager.
    /// </summary>
    public class BowlingManager
    {
        private readonly IDataManager dataManager;

        private readonly ILogger logger;

        private readonly GameManager gameManager;

        private readonly PlayerManager playerManager;

        /// <summary>
        /// Computed property simply returning the game collection containing in the game manager.
        /// </summary>
        public ReadOnlyCollection<Game> Games
        {
            get => gameManager.Games;
        }

        /// <summary>
        /// Computed property simply returning the player collection containing in the player manager.
        /// </summary>
        public ReadOnlyCollection<Player> Players
        {
            get => playerManager.Players;
        }

        /// <summary>
        /// Computed property simply returning the selected player collection containing in the player manager.
        /// </summary>
        public ReadOnlyCollection<Player> SelectedPlayers
        {
            get => playerManager.SelectedPlayers;
        }

        /// <summary>
        /// Computed property simply returning the current game in the game manager.
        /// </summary>
        public Game CurrentGame
        {
            get => gameManager.CurrentGame;
        }

        /// <summary>
        /// Create a new instance of a BowlingManager.
        /// </summary>
        /// <param name="dataManager">The persistance system use to save the players and games.</param>
        /// <param name="logger">A faculative logeer used to trace the program.</param>
        /// <exception cref="ArgumentNullException">If the DataManager given is null.</exception>
        public BowlingManager(IDataManager dataManager, ILogger<BowlingManager>? logger = null)
        {
            this.dataManager = dataManager ?? throw new ArgumentNullException(nameof(dataManager), "The DataManager given to the manager cannot be null.");
            this.logger = logger == null ? NullLogger.Instance : logger;
            gameManager = new GameManager();
            playerManager = new PlayerManager();
            logger.LogDebug("New instance of the bowling manager created.");
        }

        /// <summary>
        /// Add a game to the Data Manager.
        /// </summary>
        /// <param name="game">The game to add.</param>
        /// <returns>A boolean indicating if the game was added.</returns>
        public async Task<bool> AddGame(Game game)
        {
            bool result = await dataManager.AddGame(game);
            if (!result)
            {
                logger.LogWarning("Failed attempt to add game from data manager. Game attempted to add: {game}", game);
            }
            else
            {
                logger.LogInformation("Game added to the data manager. Game added: {game}", game);
            }
            return result;
        }

        /// <summary>
        /// Reove a game from the Data Manager.
        /// </summary>
        /// <param name="game">The game to remove</param>
        /// <returns>A boolean indicating if the game was removed.</returns>
        public async Task<bool> RemoveGame(Game game)
        {
            bool result = await dataManager.RemoveGame(game);
            if (!result)
            {
                logger.LogWarning("Failed attempt to remove game from data manager. Game attempted to remove: {game}", game);
            }
            else
            {
                logger.LogInformation("Game removed to the data manager. Game removed: {game}", game);
            }
            return result;
        }

        /// <summary>
        /// Get a game from its ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>The game found.</returns>
        public async Task<Game> GetGameFromID(int id)
        {
            Game result = await dataManager.GetGameFromID(id);
            logger.LogInformation("Game retrieved from the data manager from its the ID {id}. Game retrieved: {game}", id, result);
            return result;
        }

        /// <summary>
        /// Get the number of game specified, from the starting index given ordered by the ID.
        /// </summary>
        /// <param name="index">The index to start the recuperation of the game.</param>
        /// <param name="count">The number of games to get.</param>
        /// <returns>A collection of the games retrieved.</returns>
        public async Task<IEnumerable<Game>> GetGames(int index, int count)
        {
            IEnumerable<Game> result = await dataManager.GetGames(index, count);
            logger.LogInformation("Trying to get until {count} game from the data manager from the index {index}. "
                + "Found {result.Count} games in total.", count, index, result.Count());
            foreach (Game game in result)
            {
                gameManager.AddGame(game);
            }
            return result;
        }

        /// <summary>
        /// Get a collection of games in which the specified player has participated, ordered by the creation date.
        /// </summary>
        /// <param name="player">The player who participate to the games.</param>
        /// <param name="index">The starting index of the receverd process.</param>
        /// <param name="count">The number of game to retrive after the index.</param>
        /// <returns>A collection of games in which the specified player has participated.</returns>
        public async Task<IEnumerable<Game>> GetGamesFromPlayer(Player player, int index, int count)
        {
            IEnumerable<Game> result = await dataManager.GetGamesFromPlayer(player, index, count);
            logger.LogInformation("Trying to get until {count} game from the data manager from the index {index}. "
                + "Found {result.Count} games in total related to the player {player}.", count, index, result.Count(), player);
            return result;
        }

        /// <summary>
        /// Add a player to the DataManager.
        /// </summary>
        /// <param name="player">The player to add.</param>
        /// <returns>A boolean indicating if the player was added.</returns>
        public async Task<bool> AddPlayer(Player player)
        {
            bool result = await dataManager.AddPlayer(player);
            if (!result)
            {
                logger.LogWarning("Failed attempt to add a player to the data manager. Player attempted to add: {player}", player);
            }
            else
            {
                logger.LogInformation("Player added to the data manager succesfully. Player added: {player}", player);
            }
            return result;
        }

        /// <summary>
        /// Add a collection of player to the DataManager.
        /// </summary>
        /// <param name="players">The players to add.</param>
        /// <returns>A collection of the player added to the manager.</returns>
        public async Task<IEnumerable<Player>> AddPlayers(Player[] players)
        {
            IEnumerable<Player> addedPlayers = await dataManager.AddPlayers(players);
            if (addedPlayers.Count() != players.Length)
            {
                var notAddedPlayers = addedPlayers.Intersect(players);
                StringBuilder builder = new();
                foreach (Player player in notAddedPlayers)
                {
                    builder.AppendLine(player.ToString());
                }
                logger.LogWarning("Trying to add {count} players to the data manager. "
                + "{x} players was added but {missingPlayers} are missing. Here are the missing players: ", 
                players.Length, addedPlayers.Count(), builder.ToString());
            }
            else
            {
                logger.LogInformation("{x} players was added to the data manager. ", players.Length);
            }
            return addedPlayers;
        }

        /// <summary>
        /// Change the name and the image of the specified player.
        /// </summary>
        /// <param name="player">The player to edit.</param>
        /// <param name="name">The name player's name.</param>
        /// <param name="image">The new player's image.</param>
        /// <returns>A boolean indicating if the player was succesfully updated.</returns>
        public async Task<bool> EditPlayer(Player player, string name, string image)
        {
            bool result = await dataManager.EditPlayer(player, name, image);
            if (!result)
            {
                logger.LogWarning("Failed attempt to edit a player from the data manager. "
                    + "Player attempted to edit: {player}, new name: {name}, new image: {image}", player, name, image);
            }
            else
            {
                logger.LogInformation("Player modified succesfully. New player: {player}", player);
            }
            return result;
        }

        /// <summary>
        /// Remove a player from the manager.
        /// </summary>
        /// <param name="player">The player to remove.</param>
        /// <returns>A boolean indicating if the player was removed.</returns>
        public async Task<bool> RemovePlayer(Player player)
        {
            bool result = await dataManager.RemovePlayer(player);
            if (!result)
            {
                logger.LogWarning("Failed attempt to remove a player from the data manager. "
                    + "Player attempted to be removed: {player}", player);
            }
            else
            {
                logger.LogInformation("Player {player} deleted succesfully from the DataManager.", player);
            }
            return result;
        }

        /// <summary>
        /// Get the first players specified from the stated index ordered by the ID.
        /// </summary>
        /// <param name="index">The index to get the first players.</param>
        /// <param name="count">The number of players to get.</param>
        /// <returns>The collection of players retrieve.</returns>
        public async Task<IEnumerable<Player>> GetPlayers(int index, int count)
        {
            IEnumerable<Player> result = await dataManager.GetPlayers(index, count);
            logger.LogInformation("Trying to get until {count} players from the data manager from the index {index}. "
                + "Found {result.Count} players in total.", count, index, result.Count());
            foreach (Player player in result)
            {
                playerManager.AddPlayer(player);
            }
            return result;
        }

        /// <summary>
        /// Get a plyer from its ID.
        /// </summary>
        /// <param name="id">The id of the player to get.</param>
        /// <returns>The player retrieve from its ID.</returns>
        public async Task<Player> GetPlayerFromID(int id)
        {
            Player result = await dataManager.GetPlayerFromID(id);
            if (result == null)
            {
                logger.LogWarning("Failed attempt to get a player from the its ID from the data manager. "
                    + "ID of the player attempted te retrieve: {id}", id);
            }
            else
            {
                logger.LogInformation("Player {player} retrieved succesfully from its ID {id}", result, id);
            }
            return result;
        }

        /// <summary>
        /// Get the first players specified from the stated index with a name containing 
        /// the substring given, ordered by the name.
        /// </summary>
        /// <param name="index">The index to get the first players.</param>
        /// <param name="count">The number of players to get.</param>
        /// <param name="substring">The substring contained in the name of the players.</param>
        /// <returns>The collection of players retrieve.</returns>
        public async Task<IEnumerable<Player>> GetPlayerFromName(string substring, int index, int count)
        {
            IEnumerable<Player> result = await dataManager.GetPlayerFromName(substring, index, count);
            logger.LogInformation("Trying to get until {count} players from the data manager from the index {index} with name containing {substring}. "
                + "Found {result.Count} players in total.", count, index, substring, result.Count());
            return result;
        }


        /// <summary>
        /// Add a player to the selected players.
        /// </summary>
        /// <param name="player">The player to add.</param>
        /// <returns>A boolean indicating if the player was added.</returns>
        public async Task<bool> AddSelectedPlayer(Player player)
        {
            bool result = await Task.Run(() => playerManager.AddSelectedPlayer(player));
            if (!result)
            {
                logger.LogWarning("Failed attempt to add a selected player to the player manager. Player attempted to add: {player}", player);
            }
            else
            {
                logger.LogInformation("Selected player added to the player manager succesfully. Player added: {player}", player);
            }
            return result;
        }

        /// <summary>
        /// Add a collection of player to the selected players.
        /// </summary>
        /// <param name="players">The players to add.</param>
        /// <returns>A collection of the player added to the selected players collection.</returns>
        public async Task<IEnumerable<Player>> AddSelectedPlayers(Player[] players)
        {
            IEnumerable<Player> addedPlayers = await Task.Run(() => playerManager.AddSelectedPlayers(players));
            if (addedPlayers.Count() != players.Length)
            {
                var notAddedPlayers = addedPlayers.Intersect(players);
                StringBuilder builder = new();
                foreach (Player player in notAddedPlayers)
                {
                    builder.AppendLine(player.ToString());
                }
                logger.LogWarning("Trying to add {count} selected players to the player manager. "
                + "{x} players was added but {missingPlayers} are missing. Here are the missing players: ",
                players.Length, addedPlayers.Count(), builder.ToString());
            }
            else
            {
                logger.LogInformation("{x} selected players was added to the player manager.", players.Length);
            }
            return addedPlayers;
        }

        /// <summary>
        /// Remove a player from the selection.
        /// </summary>
        /// <param name="player">The player to remove.</param>
        /// <returns>A boolean indicating if the player was removed.</returns>
        public async Task<bool> RemoveSelectedPlayer(Player player)
        {
            bool result = await Task.Run(() => playerManager.RemoveSelectedPlayer(player));
            if (!result)
            {
                logger.LogWarning("Failed attempt to remove a selected player from the player manager. "
                    + "Player attempted to be removed: {player}", player);
            }
            else
            {
                logger.LogInformation("Selected player {player} deleted succesfully from the player manager.", player);
            }
            return result;
        }

        /// <summary>
        /// Clear the collection of selected players.
        /// </summary>
        public void ClearSelectedPlayers()
        {
            playerManager.ClearSelectedPlayers();
            logger.LogInformation("All the selected players from the players manager were cleared.");
        }
    }
}
