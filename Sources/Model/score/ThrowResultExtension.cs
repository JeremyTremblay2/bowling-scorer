using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.score
{
    public static class ThrowResultExtension
    {
        public static int ToInt(this ThrowResult throwResult)
        {
            switch (throwResult)
            {
                case ThrowResult.ONE: { return 1; }
                case ThrowResult.TWO: { return 2; }
                case ThrowResult.TREE : { return 3; }
                case ThrowResult.FOUR: { return 4; }
                case ThrowResult.FIVE: { return 5; }
                case ThrowResult.SIX: { return 6; }
                case ThrowResult.SEVEN: { return 7; }
                case ThrowResult.EIGHT: { return 8; }
                case ThrowResult.NINE: { return 9; }
                case ThrowResult.STRIKE: { return 10; }
                case ThrowResult.SPAIR: { return 10; }
                default: { throw new ArgumentException("Cannot convert this ThrowResult to int"); }
            }
        }
    }
}
