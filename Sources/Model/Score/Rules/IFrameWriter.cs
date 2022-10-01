using Model.Score;
using Model.Score.Frame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.score.Rules
{
    public interface IFrameWriter
    {
        public void WriteValue(AFrame frame, int index, ThrowResult throwResult);
    }
}
