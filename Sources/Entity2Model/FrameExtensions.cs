using Entities.Frame;
using Entity2Model.Mapper;
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
        /// <param name="frameEntity"></param>
        /// <returns></returns>
        public static AFrame ToModel(this FrameEntity frameEntity)
        {
            AFrame? frame = BowlingMapper.Frames.Get(frameEntity);
            if (frame is not null) return frame;

            var trEnt = frameEntity.ThrowResultEntitys.ToList();
            if (trEnt.Count == 2)
            {
                ClassicFrame classic = new(frameEntity.FrameId, frameEntity.FrameNumberLabel, trEnt[0].Value.ToThrowResult(), trEnt[1].Value.ToThrowResult())
                {
                    CumulativeScore = frameEntity.CumulativeScore,
                    ScoreValue = frameEntity.ScoreValue
                };
                frame = classic;
            }
            else if (trEnt.Count == 3)
            {
                ClassicLastFrame classicLast = new(frameEntity.FrameId, frameEntity.FrameNumberLabel,
                    trEnt[0].Value.ToThrowResult(), trEnt[1].Value.ToThrowResult(), trEnt[2].Value.ToThrowResult())
                {
                    CumulativeScore = frameEntity.CumulativeScore,
                    ScoreValue = frameEntity.ScoreValue
                };
                frame = classicLast;
            }
            BowlingMapper.Frames.Map(frameEntity, frame);
            return frame;
        }

        public static FrameEntity ToEntity(this AFrame aFrame)
        {
            FrameEntity? frameEntity = BowlingMapper.Frames.Get(aFrame);
            if (frameEntity is not null) return frameEntity;

            frameEntity = new();
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
            BowlingMapper.Frames.Map(frameEntity, aFrame);
            return frameEntity;
        }
    }
}