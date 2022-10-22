using Entities.Frame;
using FrameWriterModel.Frame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Entity2Model
{
    public static class FrameExtensions
    {
        /// <summary>
        /// Convert FrameEntity to AFrame
        /// </summary>
        /// <param name="aFrameEntity"></param>
        /// <returns></returns>
        public static AFrame ToModel(this FrameEntity aFrameEntity)
        {
            ClassicFrame classic = new ClassicFrame(aFrameEntity.Id, aFrameEntity.FrameNumberLabel);
            classic.CumulativeScore = aFrameEntity.CumulativeScore;
            classic.ScoreValue = aFrameEntity.ScoreValue;
            return classic;
        }

        public static FrameEntity ToEntity(this AFrame aFrame)
        {
            FrameEntity frameEntity = new();
            frameEntity.Id = aFrame.ID;
            frameEntity.FrameNumberLabel = aFrame.FrameNumberLabel;
            frameEntity.ScoreValue = aFrame.ScoreValue;
            frameEntity.CumulativeScore = aFrame.CumulativeScore;
            return frameEntity;
        }
    }
}
