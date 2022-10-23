using FrameModel.Frame.ThrowResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Frame
{
    public class FrameEntity
    {
        public int FrameId { get; set; }

        public int FrameNumberLabel { get; set; }

        public int ScoreValue { get; set; }

        public int CumulativeScore { get; set; }

        public ICollection<ThrowResultEntity> ThrowResultEntitys { get; set; } = new List<ThrowResultEntity>();
    }
}
