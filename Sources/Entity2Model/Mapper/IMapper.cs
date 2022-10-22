using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity2Model.Mapper
{
    public interface IMapper<T, P>
    {
        void Map(T elem1, P elem2);

        T? Get(P entity);

        P? Get(T entity);
    }
}
