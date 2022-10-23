using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Mapper<TEntity, TModel> where TEntity : class where TModel : class, Enum
    {
        private readonly HashSet<Tuple<TEntity, TModel>> maps;

        public Mapper()
        {
            maps = new HashSet<Tuple<TEntity, TModel>>();
        }

        public void Map(TEntity entity, TModel model) => maps.Add(new Tuple<TEntity, TModel>(entity, model));

        public TModel? GetModel(TEntity entity)
        {
            var value = maps.FirstOrDefault(t => t.Item1 != null && t.Item1.Equals(entity));
            if (value == null) return default;
            return value.Item2;
        }

        public TEntity? GetModel(TModel model)
        {
            var value = maps.FirstOrDefault(t => t.Item2 != null && t.Item2.Equals(model));
            if (value == null) return default;
            return value.Item1;
        }
    }
}
