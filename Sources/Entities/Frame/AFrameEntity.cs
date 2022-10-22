using FrameWriterModel.Frame.ThrowResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Frame
{
    public class AFrameEntity
    {
        public int Id { get; set; }
        public int FrameNumberLabel { get; set; }
        public int ScoreValue { get; set; }
        public int CumulativeScore { get; set; }


    }
}
