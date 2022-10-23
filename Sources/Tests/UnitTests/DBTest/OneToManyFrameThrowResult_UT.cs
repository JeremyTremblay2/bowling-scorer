using Entities;
using Entities.Frame;
using Entity2Model;
using FrameModel.Frame;
using FrameModel.Frame.ThrowResults;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Model.Score.Rules;
using Model.Score.Rules.Calculator;
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

        [Fact]
        public void ModifyFrame()
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
                context.Database.EnsureCreated();

                ARules rules = new ClassicRules();
                AFrame classic1 = new ClassicFrame(1, ThrowResult.FIVE, ThrowResult.TWO);

                context.Frames.Add(classic1.ToEntity());
                context.SaveChanges();
            }

            using (var context = new BowlingDbContext(options))
            {
                context.Database.EnsureCreated();

                Assert.Equal(1, context.Frames.Count());
                FrameEntity frameEntity = context.Frames.First();
                frameEntity.FrameNumberLabel = 2;
                context.SaveChanges();
            }
            using (var context = new BowlingDbContext(options))
            {
                FrameEntity frameEntity = context.Frames.First();
                Assert.Equal(2, frameEntity.FrameNumberLabel);
            }
        }

        [Fact]
        public void DeleteFrame()
        {
            //connection must be opened to use In-memory database
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            var options = new DbContextOptionsBuilder<BowlingDbContext>()
                .UseSqlite(connection)
                .Options;

            using (var context = new BowlingDbContext(options))
            {
                context.Database.EnsureCreated();

                ARules rules = new ClassicRules();
                AFrame classic1 = new ClassicFrame(1, ThrowResult.FIVE, ThrowResult.TWO);

                context.Frames.Add(classic1.ToEntity());
                context.SaveChanges();
            }

            using (var context = new BowlingDbContext(options))
            {
                context.Database.EnsureCreated();

                context.Frames.Remove(context.Frames.First());
                context.SaveChanges();
            }

            using (var context = new BowlingDbContext(options))
            {
                context.Database.EnsureCreated();

                Assert.Equal(0, context.Frames.Count());
            }
        }

        [Fact]
        public void FrameExtensionShouldTransformFrameIntoFrameEntity()
        {
            AFrame classic1 = new ClassicFrame(1, ThrowResult.FIVE, ThrowResult.TWO);
            FrameEntity entity = classic1.ToEntity();
            Assert.Equal(1, entity.FrameNumberLabel);
            Assert.Equal(0, entity.FrameId);
            Assert.Equal(0, entity.CumulativeScore);
            Assert.Equal(0, entity.ScoreValue);
            Assert.Equal(ThrowResult.FIVE, entity.ThrowResultEntitys.First().Value.ToThrowResult());
            Assert.Equal(ThrowResult.TWO, entity.ThrowResultEntitys.ElementAt(1).Value.ToThrowResult());
        }

        [Fact]
        public void FrameExtensionShouldTransformFrameEntityIntoFrame()
        {
            FrameEntity entity = null;
            entity = new FrameEntity
            {
                FrameId = 2,
                FrameNumberLabel = 3,
                CumulativeScore = 10,
                ScoreValue = 5,
                ThrowResultEntitys = new List<ThrowResultEntity>()
                {
                    new ThrowResultEntity
                    {
                        ThrowResultId = 0,
                        FrameEntity = entity,
                        Value = '5'
                    },
                    new ThrowResultEntity
                    {
                        ThrowResultId = 1,
                        FrameEntity = entity,
                        Value = '/'
                    }
                }
            };
            AFrame frame = entity.ToModel();
            Assert.Equal(2, frame.ID);
            Assert.Equal(3, frame.FrameNumberLabel);
            Assert.Equal(10, frame.CumulativeScore);
            Assert.Equal(5, frame.ScoreValue);
            Assert.Equal(ThrowResult.FIVE, frame.ThrowResults[0]);
            Assert.Equal(ThrowResult.SPARE, frame.ThrowResults[1]);
        }

        [Fact]
        public void ThrowResultsFromFrameEntityShouldHaveLogicalValues()
        {
            FrameEntity entity = null;
            entity = new FrameEntity
            {
                FrameId = 2,
                FrameNumberLabel = 3,
                CumulativeScore = 10,
                ScoreValue = 5,
            };
            var values = new List<ThrowResultEntity>()
            {
                new ThrowResultEntity
                {
                    ThrowResultId = 0,
                    FrameEntity = entity,
                    Value = '5'
                },
                new ThrowResultEntity
                {
                    ThrowResultId = 1,
                    FrameEntity = entity,
                    Value = '/'
                }
            };
            entity.ThrowResultEntitys = values;
            Assert.Equal(0, entity.ThrowResultEntitys.ElementAt(0).ThrowResultId);
            Assert.Equal(1, entity.ThrowResultEntitys.ElementAt(1).ThrowResultId);
            Assert.Equal('/', entity.ThrowResultEntitys.ElementAt(1).Value);
            Assert.Equal('5', entity.ThrowResultEntitys.ElementAt(0).Value);
            Assert.Equal(entity, entity.ThrowResultEntitys.ElementAt(0).FrameEntity);
            Assert.Equal(entity, entity.ThrowResultEntitys.ElementAt(1).FrameEntity);
        }
    }
}
