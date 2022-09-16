using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.score
{
    public class Frame
    {
        private ThrowResult[] throwResults;
        public System.Collections.ObjectModel.ReadOnlyCollection<ThrowResult> ThrowResults
        {
            get;
            private set;
        }
        private int scoreValue;
        public int ScoreValue
        {
            get { return scoreValue; }
            set {
                if (value < 0) scoreValue = 0;
                else scoreValue = value;
            }
        }
        public Frame(int numberOfThrow)
        {
            if (numberOfThrow <= 0)
            {
                throw new ArgumentException("The number of throw must be > 0");
            }
            throwResults = new ThrowResult[numberOfThrow];
            ThrowResults = new System.Collections.ObjectModel.ReadOnlyCollection<ThrowResult>(throwResults);
        }

        public void AddThrow(int index, ThrowResult throwResult) 
        {
            throwResults[index] = throwResult;
        }

    }
}
