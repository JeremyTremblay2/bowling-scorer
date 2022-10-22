using FrameModel.Frame.ThrowResults;
using Model.Score;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    /// <summary>
    /// Extension class to manipulate ScoreTables.
    /// </summary>
    public static class ScoreTableExtension
    {
        /// <summary>
        /// Fill in a score table given in parameters with the throw results specified.
        /// </summary>
        /// <param name="scoreTable">The score table to fill in.</param>
        /// <param name="throwResults">The throw results to write into.</param>
        /// <returns>The score table updated.</returns>
        public static ScoreTable ToScoreTable(this ScoreTable scoreTable, ThrowResult[][] throwResults)
        {
            for (int i = 0; i < throwResults.Length; i++)
            {
                var currentFrame = throwResults[i];
                for (int j = 0; j < currentFrame.Length; j++)
                {
                    scoreTable.WriteValue(i, j, throwResults[i][j]);
                }
            }
            return scoreTable;
        }
    }
}
