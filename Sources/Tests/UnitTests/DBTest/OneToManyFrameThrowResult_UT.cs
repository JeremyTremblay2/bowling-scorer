using Entities;
using Entities.Frame;
using Entity2Model;
using FrameModel.Frame;
using FrameModel.Frame.ThrowResults;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Model.Score.Rules;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace UnitTests.DBTest
{
    public class OneToManyFrameThrowResult_UT
    {
        [Fact]
        public void AddFrameThrowresult()
        {
            //connection must be opened to use In-memory database
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            var options = new DbContextOptionsBuilder<BowlingDbContext>()
                .UseSqlite(connection)
                .Options;

            //prepares the database with one instance of the context
            using (var context = new BowlingDbContext(options))
            {
                //context.Database.OpenConnection();
                context.Database.EnsureCreated();

                ARules rules = new ClassicRules();

                AFrame classic1 = new ClassicFrame(1, ThrowResult.FIVE, ThrowResult.TWO);
                AFrame classic2 = new ClassicFrame(1, ThrowResult.TWO, ThrowResult.FOUR);
                AFrame classic3 = new ClassicFrame(1, ThrowResult.SEVEN, ThrowResult.ONE);

                List<AFrame> aFrames = new() { classic1, classic2, classic3 };
                rules.UpdateFromFrame(0, aFrames);
                rules.UpdateFromFrame(1, aFrames);
                rules.UpdateFromFrame(2, aFrames);
                rules.CalculateScore(aFrames);

                context.Frames.Add(classic1.ToEntity());
                context.Frames.Add(classic2.ToEntity());
                context.Frames.Add(classic3.ToEntity());
                context.SaveChanges();
            }

            //uses another instance of the context to do the tests
            using (var context = new BowlingDbContext(options))
            {
                context.Database.EnsureCreated();

                Assert.Equal(3, context.Frames.Count());

                //DO the request to grab throw result (there is a FK, we must on say that we want it with the collection)
                var frames = context.Frames.Include(f => f.ThrowResultEntitys);
                List<FrameEntity> frameEntities = frames.ToList();

                // Frame 0 
                Assert.Equal(1, frameEntities[0].FrameId);
                Assert.Equal(1, frameEntities[0].FrameNumberLabel);
                Assert.Equal(7, frameEntities[0].ScoreValue);
                Assert.Equal(7, frameEntities[0].CumulativeScore);

                // Frame 0 - ThrowResults
                Assert.Equal(2, frameEntities[0].ThrowResultEntitys.Count);
                List<ThrowResultEntity> trEnts0 = frameEntities[0].ThrowResultEntitys.ToList();
                Assert.Equal(ThrowResult.FIVE, trEnts0[0].Value.ToThrowResult());
                Assert.Equal(ThrowResult.TWO, trEnts0[1].Value.ToThrowResult());

                // Frame 1 
                Assert.Equal(2, frameEntities[1].FrameId);
                Assert.Equal(1, frameEntities[1].FrameNumberLabel);
                Assert.Equal(6, frameEntities[1].ScoreValue);
                Assert.Equal(13, frameEntities[1].CumulativeScore);

                // Frame 1 - ThrowResults
                Assert.Equal(2, frameEntities[1].ThrowResultEntitys.Count);
                List<ThrowResultEntity> trEnts1 = frameEntities[1].ThrowResultEntitys.ToList();
                Assert.Equal(ThrowResult.TWO, trEnts1[0].Value.ToThrowResult());
                Assert.Equal(ThrowResult.FOUR, trEnts1[1].Value.ToThrowResult());

                // Frame 2 
                Assert.Equal(3, frameEntities[2].FrameId);
                Assert.Equal(1, frameEntities[2].FrameNumberLabel);
                Assert.Equal(8, frameEntities[2].ScoreValue);
                Assert.Equal(21, frameEntities[2].CumulativeScore);

                // Frame 2 - ThrowResults
                Assert.Equal(2, frameEntities[2].ThrowResultEntitys.Count);
                List<ThrowResultEntity> trEnts2 = frameEntities[2].ThrowResultEntitys.ToList();
                Assert.Equal(ThrowResult.SEVEN, trEnts2[0].Value.ToThrowResult());
                Assert.Equal(ThrowResult.ONE, trEnts2[1].Value.ToThrowResult());
            }
        }

        
    }
}
