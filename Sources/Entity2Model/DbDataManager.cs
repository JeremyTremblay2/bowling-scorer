using Business;
using Entities;
using Entity2Model.Mapper;
using Model.Games;
using Model.Players;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Entity2Model
{
    /// <summary>
    /// A DbDataManager represent a manager responsible of adding / removing and getting information from and to a database.
    /// </summary>
    public class DbDataManager : IDataManager
    {
        /// <summary>
        /// Add a game to the bowling database.
        /// </summary>
        /// <param name="game">The game to add.</param>
        /// <returns>A boolean indicating if the game was added.</returns>
        public Task<bool> AddGame(Game game)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Add a player to the bowling database.
        /// </summary>
        /// <param name="player">The player to add.</param>
        /// <returns>A boolean indicating if the player was added.</returns>
        public async Task<bool> AddPlayer(Player player)
        {
            BowlingMapper.Reset();
            bool result = false;
            using (var context = new BowlingDbContext())
            {
                await context.Players.AddAsync(player.ToEntity());
                result = await context.SaveChangesAsync() == 1;
            }
            return result;
        }

        /// <summary>
        /// Add a collection of players to the bowling database.
        /// </summary>
        /// <param name="players">The players to add.</param>
        /// <returns>A collection of the player added to the bowling database.</returns>
        public async Task<IEnumerable<Player>> AddPlayers(Player[] players)
        {
            BowlingMapper.Reset();
            List<Player> result = new List<Player>();
            foreach (Player p in players)
            {
                if(await AddPlayer(p)) result.Add(p);
            }
            return result;
        }

        /// <summary>
        /// Change the name and the image of the specified player from the bowling database.
        /// </summary>
        /// <param name="player">The player to edit.</param>
        /// <param name="name">The name player's name.</param>
        /// <param name="image">The new player's image.</param>
        /// <returns>A boolean indicating if the player was succesfully updated.</returns>
        public async Task<bool> EditPlayer(Player player, string name, string image)
        {
            // THIS METHOD IS USELESS AND WILL BE REMOVED IN THE FUTURE.
            BowlingMapper.Reset();
            bool result = false;
            using (var context = new BowlingDbContext())
            {
                PlayerEntity p = await Task.Run(() => context.Players.Where(p => p.ID == player.ID).First());
                if (p == null) return false;
                p.Name = name;
                p.Image = image;
                result = await context.SaveChangesAsync() == 1;
            }
            return result;
        }

        /// <summary>
        /// Get a game from its ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>The game found.</returns>
        public Task<Game> GetGameFromID(int id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get the number of game specified, from the starting index given ordered by the ID, from the bowling database.
        /// </summary>
        /// <param name="index">The index to start the recuperation of the game.</param>
        /// <param name="count">The number of games to get.</param>
        /// <returns>A collection of the games retrieved.</returns>
        public Task<IEnumerable<Game>> GetGames(int index, int count)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get a collection of games in which the specified player has participated, ordered by the creation date.
        /// </summary>
        /// <param name="player">The player who participate to the games.</param>
        /// <param name="index">The starting index of the receverd process.</param>
        /// <param name="count">The number of game to retrive after the index.</param>
        /// <returns>A collection of games in which the specified player has participated.</returns>
        public Task<IEnumerable<Game>> GetGamesFromPlayer(Player player, int index, int count)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get a plyer from its ID.
        /// </summary>
        /// <param name="id">The id of the player to get.</param>
        /// <returns>The player retrieve from its ID.</returns>
        public async Task<Player> GetPlayerFromID(int id)
        {
            BowlingMapper.Reset();
            PlayerEntity result = null;
            using (var context = new BowlingDbContext())
            {
                result = await Task.Run(() => context.Players.Where(p => p.ID == id).First());
            }
            return result?.ToModel();
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
            BowlingMapper.Reset();
            List<Player> players = new List<Player>();
            using (var context = new BowlingDbContext())
            {
                await Task.Run(() =>
                    players.AddRange(
                        context.Players.Where(p => p.Name.Contains(substring))
                        .Skip(index * count)
                        .Take(count)
                        .Select(pl => pl.ToModel())));
            }
            return players;
        }

        /// <summary>
        /// Get the first players specified from the stated index ordered by the ID from the bowling database.
        /// </summary>
        /// <param name="index">The index to get the first players.</param>
        /// <param name="count">The number of players to get.</param>
        /// <returns>The collection of players retrieve.</returns>
        public async Task<IEnumerable<Player>> GetPlayers(int index, int count)
        {
            BowlingMapper.Reset();
            List<Player> players = new List<Player>();
            using (var context = new BowlingDbContext())
            {
                await Task.Run(() =>
                    players.AddRange(
                        context.Players
                        .Skip(index * count)
                        .Take(count)
                        .Select(pl => pl.ToModel())));
            }
            return players;
        }

        /// <summary>
        /// Reove a game from the bowling database.
        /// </summary>
        /// <param name="game">The game to remove</param>
        /// <returns>A boolean indicating if the game was removed.</returns>
        public Task<bool> RemoveGame(Game game)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Remove a player from the bowling database.
        /// </summary>
        /// <param name="player">The player to remove.</param>
        /// <returns>A boolean indicating if the player was removed.</returns>
        public async Task<bool> RemovePlayer(Player player)
        {
            BowlingMapper.Reset();
            bool result = false;
            using (var context = new BowlingDbContext())
            {
                context.Players.Remove(player.ToEntity());
                result = await context.SaveChangesAsync() == 1;
            }
            return result;
        }
    }
}
