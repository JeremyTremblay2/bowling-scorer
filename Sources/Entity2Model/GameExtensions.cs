using Entities;
using Entities.Entities.Game;
using Entity2Model.Mapper;
using Model.Games;
using Model.Players;
using Model.Score.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity2Model
{
    public static class GameExtensions
    {
        public static GameEntity ToEntity(this Game game)
        {
            GameEntity? gameEntity = BowlingMapper.Games.Get(game);
            if (gameEntity is not null) return gameEntity;

            gameEntity = new()
            {
                Id = game.ID,
            };
            BowlingMapper.Games.Map(gameEntity, game);
            return gameEntity;
        }

        public static Game ToModel(this GameEntity gameEntity)
        {
            Game? game = BowlingMapper.Games.Get(gameEntity);
            if (game is not null) return game;

            List<Player> players = new();
            foreach (var gp in gameEntity.GamePlayer)
            {
                players.Add(gp.PlayerEntity.ToModel());
            }

            game = new(new ClassicRules(), gameEntity.Id, players);
            return game;
        }
    }
}
