using Model.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity2Model.Mapper
{
    /// <summary>
    /// Class implementing the IMapper and representing a two-parameter mapper for the conversion between 
    /// entities and model object in the case of an ORM project.
    /// </summary>
    /// <typeparam name="TEntity">The entities.</typeparam>
    /// <typeparam name="TModel">The model objects associated to these entities.</typeparam>
    public class Mapper<TEntity, TModel> : IMapper<TEntity, TModel> where TEntity : class where TModel : class
    {
        /// <summary>
        /// Contains the correspondance between entities and model.
        /// </summary>
        private readonly HashSet<Tuple<TEntity, TModel>> maps = new HashSet<Tuple<TEntity, TModel>>();

        /// <summary>
        /// Map an entity and a model and add them to the mapper.
        /// </summary>
        /// <param name="entity">The entity to map.</param>
        /// <param name="model">The model to map.</param>
        public void Map(TEntity elem1, TModel elem2) => maps.Add(new Tuple<TEntity, TModel>(elem1, elem2));

        /// <summary>
        /// Get the model object corresponding to the given entity.
        /// </summary>
        /// <param name="elem">The entity used to get the model.</param>
        /// <returns>The model corresponding to the entity or null if is was not found.</returns>
        public TModel? Get(TEntity elem)
        {
            var value = maps.FirstOrDefault(t => t.Item1 != null && t.Item1.Equals(elem));
            if (value == null) return null;
            return value.Item2;
        }

        /// <summary>
        /// Get the entity object corresponding to the given model.
        /// </summary>
        /// <param name="elem">The model used to get the entity.</param>
        /// <returns>The entity corresponding to the model or null if is was not found.</returns>
        public TEntity? Get(TModel elem)
        {
            var value = maps.FirstOrDefault(t => t.Item2 != null && t.Item2.Equals(elem));
            if (value == null) return null;
            return value.Item1;
        }

        /// <summary>
        /// Clear data from the mapper and the correspondances.
        /// </summary>
        public void Clear() => maps.Clear();
    }
}
