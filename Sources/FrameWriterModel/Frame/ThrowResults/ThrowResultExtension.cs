﻿using System;
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
            return throwResult switch
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
                _ => throw new ArgumentException($"Cannot convert this ThrowResult to int : {throwResult}")
            };
        }

        public static ThrowResult ToThrowResult(this int throwResult, bool isStrike)
        {
            return throwResult switch
            {
                0 => ThrowResult.ZERO,
                1 => ThrowResult.ONE,
                2 => ThrowResult.TWO,
                3 => ThrowResult.TREE,
                4 => ThrowResult.FOUR,
                5 => ThrowResult.FIVE,
                6 => ThrowResult.SIX,
                7 => ThrowResult.SEVEN,
                8 => ThrowResult.EIGHT,
                9 => ThrowResult.NINE,
                10 => isStrike == true ? ThrowResult.STRIKE : ThrowResult.SPARE,
                _ => throw new ArgumentException($"Cannot convert this int to ThrowResult : {throwResult}")
            };
        }

        public static char ToChar(this ThrowResult throwResult)
        {
            return throwResult switch
            {
                ThrowResult.NONE => '-',
                ThrowResult.ZERO => '0',
                ThrowResult.ONE => '1',
                ThrowResult.TWO => '2',
                ThrowResult.TREE => '3',
                ThrowResult.FOUR => '4',
                ThrowResult.FIVE => '5',
                ThrowResult.SIX => '6',
                ThrowResult.SEVEN => '7',
                ThrowResult.EIGHT => '8',
                ThrowResult.NINE => '9',
                ThrowResult.SPARE => '/',
                ThrowResult.STRIKE => 'X',
                _ => throw new ArgumentException($"Cannot convert this ThrowResult to char : {throwResult}")
            };
        }
    }
}
