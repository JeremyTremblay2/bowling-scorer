using Entities;
using FrameModel.Frame.ThrowResults;
using FrameModel.Frame;
using Model.Score.Rules;
using Model.Score;
using static System.Console;
using Entity2Model;
using Microsoft.EntityFrameworkCore;

namespace FunctionnalTests
{
    public static class FrameEntityTests
    {
        public static void AddFrameEntities()
        {
            ScoreTable scoreTable = new ScoreTable(new ClassicRules());
            scoreTable.WriteValue(scoreTable.Frames[0], 0, ThrowResult.SIX);
            scoreTable.WriteValue(scoreTable.Frames[0], 1, ThrowResult.TWO);
            scoreTable.WriteValue(scoreTable.Frames[1], 0, ThrowResult.THREE);
            scoreTable.WriteValue(scoreTable.Frames[1], 1, ThrowResult.FOUR);
            scoreTable.WriteValue(scoreTable.Frames[2], 0, ThrowResult.NONE);
            scoreTable.WriteValue(scoreTable.Frames[2], 1, ThrowResult.STRIKE);
            scoreTable.WriteValue(scoreTable.Frames[3], 0, ThrowResult.NONE);
            scoreTable.WriteValue(scoreTable.Frames[3], 1, ThrowResult.STRIKE);
            scoreTable.WriteValue(scoreTable.Frames[4], 0, ThrowResult.NONE);
            scoreTable.WriteValue(scoreTable.Frames[4], 1, ThrowResult.STRIKE);
            scoreTable.WriteValue(scoreTable.Frames[5], 0, ThrowResult.NONE);
            scoreTable.WriteValue(scoreTable.Frames[5], 1, ThrowResult.STRIKE);
            scoreTable.WriteValue(scoreTable.Frames[6], 0, ThrowResult.NONE);
            scoreTable.WriteValue(scoreTable.Frames[6], 1, ThrowResult.STRIKE);
            scoreTable.WriteValue(scoreTable.Frames[7], 0, ThrowResult.NONE);
            scoreTable.WriteValue(scoreTable.Frames[7], 1, ThrowResult.STRIKE);
            scoreTable.WriteValue(scoreTable.Frames[8], 0, ThrowResult.NONE);
            scoreTable.WriteValue(scoreTable.Frames[8], 1, ThrowResult.STRIKE);
            scoreTable.WriteValue(scoreTable.Frames[9], 0, ThrowResult.STRIKE);
            scoreTable.WriteValue(scoreTable.Frames[9], 1, ThrowResult.STRIKE);
            scoreTable.WriteValue(scoreTable.Frames[9], 2, ThrowResult.STRIKE);
            scoreTable.UpdateAll();
            using (BowlingDbContext db = new())
            {
                WriteLine("Opening the connection to the database.");

                if (db.Frames.Any())
                {
                    WriteLine("There is some frames in the db, clean it");
                    foreach (var frame in db.Frames)
                    {
                        WriteLine("Delete one frame : " + frame);
                        db.Frames.Remove(frame);
                    }
                    db.SaveChanges();
                }

                WriteLine("Start to write the ScoreTable's scores");
                foreach (AFrame frameToSave in scoreTable.Frames)
                {
                    WriteLine("Add : " + frameToSave);
                    db.Frames.Add(frameToSave.ToEntity());
                }
                db.SaveChanges();

                WriteLine("All frames in the db :");
                foreach (var frameToShow in db.Frames.Include(f => f.ThrowResultEntitys))
                {
                    WriteLine(frameToShow.ToModel());
                }
            }
        }
    }
}
