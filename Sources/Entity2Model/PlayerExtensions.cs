﻿using Entities;
using Entity2Model.Mapper;
using Model.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Entity2Model
{
    /// <summary>
    /// Contains extension methods to transform an entity into a model version of it, and vice-versa.
    /// </summary>
    public static class PlayerExtensions
    {
        /// <summary>
        /// Transform a player entity into a player.
        /// </summary>
        /// <param name="entity">The entity to transform into a player.</param>
        /// <returns>A player similar to the entity.</returns>
        public static Player ToModel(this PlayerEntity entity)
        {
            Player? player = BowlingMapper.Players.Get(entity);
            if (player is not null) return player;
            player = new Player(entity.ID, entity.Name, entity.Image);
            BowlingMapper.Players.Map(entity, player);
            return player;
        }

        /// <summary>
        /// Transform a player into an entity.
        /// </summary>
        /// <param name="player">The player to transform into an entity.</param>
        /// <returns>An entity similar to the player model.</returns>
        public static PlayerEntity ToEntity(this Player player)
        {
            PlayerEntity? entity = BowlingMapper.Players.Get(player);
            if (entity is not null) return entity;
            entity = new PlayerEntity
            {
                ID = player.ID,
                Name = player.Name,
                Image = player.Image
            };
            BowlingMapper.Players.Map(entity, player);
            return entity;
        }
    }
}
