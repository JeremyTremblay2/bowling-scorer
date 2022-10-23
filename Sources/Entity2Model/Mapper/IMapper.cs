using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity2Model.Mapper
{
    /// <summary>
    /// Defines a contract that the implementing class must provide methods that allow data mapping. 
    /// Represents a generic two-parameter mapper.
    /// </summary>
    /// <typeparam name="T">The first parameter to map.</typeparam>
    /// <typeparam name="P">The second parameter to map.</typeparam>
    public interface IMapper<T, P>
    {
        /// <summary>
        /// Map the two elements given in parameter.
        /// </summary>
        /// <param name="elem1">The first telement to map.</param>
        /// <param name="elem2">The second element to map.</param>
        void Map(T elem1, P elem2);

        /// <summary>
        /// Get The first element from the second if it already exists in the mapper.
        /// </summary>
        /// <param name="elem">The first element.</param>
        /// <returns>The second element corresponding to the first element.</returns>
        T? Get(P elem);

        /// <summary>
        /// Get The second element from the first if it already exists in the mapper. 
        /// </summary>
        /// <param name="elem">The second element.</param>
        /// <returns>The first element corresponding to the second element.</returns>
        P? Get(T elem);

        /// <summary>
        /// Clear the mapper.
        /// </summary>
        void Clear();
    }
}
