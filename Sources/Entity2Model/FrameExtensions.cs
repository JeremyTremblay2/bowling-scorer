using Entities.Frame;
using FrameModel.Frame;
using FrameModel.Frame.ThrowResults;
using System;
using System.Collections.Generic;
using System.Data.Common;
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
            var trEnt = aFrameEntity.ThrowResultEntitys.ToList();
            ClassicFrame classic = new(aFrameEntity.FrameId, aFrameEntity.FrameNumberLabel, trEnt[0].Value.ToThrowResult(), trEnt[1].Value.ToThrowResult())
            {
                CumulativeScore = aFrameEntity.CumulativeScore,
                ScoreValue = aFrameEntity.ScoreValue
            };
            return classic;
        }

        public static FrameEntity ToEntity(this AFrame aFrame)
        {
            FrameEntity frameEntity = new();
            frameEntity.FrameId = aFrame.ID;
            frameEntity.FrameNumberLabel = aFrame.FrameNumberLabel;
            frameEntity.ScoreValue = aFrame.ScoreValue;
            frameEntity.CumulativeScore = aFrame.CumulativeScore;
            foreach (ThrowResult tr in aFrame.ThrowResults)
            {
                ThrowResultEntity throwResultEntity = new()
                {
                    FrameEntity = frameEntity,
                    Value = tr.ToChar()
                };
                frameEntity.ThrowResultEntitys.Add(throwResultEntity);
            }
            return frameEntity;
        }
    }
}
