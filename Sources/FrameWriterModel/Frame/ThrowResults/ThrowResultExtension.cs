using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrameWriterModel.Frame.ThrowResults
{
    /// <summary>
    /// Extension used to convert ThrowResult occurences into other types
    /// </summary>
    public static class ThrowResultExtension
    {
        public static int ToInt(this ThrowResult throwResult)
        {
            return (throwResult switch
            {
                ThrowResult.NONE => 0,
                ThrowResult.ZERO => 0,
                ThrowResult.ONE => 1,
                ThrowResult.TWO => 2,
                ThrowResult.TREE => 3,
                ThrowResult.FOUR => 4,
                ThrowResult.FIVE => 5,
                ThrowResult.SIX => 6,
                ThrowResult.SEVEN => 7,
                ThrowResult.EIGHT => 8,
                ThrowResult.NINE => 9,
                ThrowResult.SPARE => 10,
                ThrowResult.STRIKE => 10,
                _ => throw new ArgumentException("Cannot convert this ThrowResult to int : " + throwResult)
            });
        }
    }
}
