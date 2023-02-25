using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JTN.SurveyMaker.PL.Test
{
    [TestClass]
    public class utQuestionAnswer
    {
        protected SurveyMakerEntities dc;
        protected IDbContextTransaction transaction;

        [TestInitialize]
        public void TestInitialize()
        {
            dc = new SurveyMakerEntities();
            transaction = dc.Database.BeginTransaction();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            transaction.Rollback();
            transaction.Dispose();
            dc = null;
        }

        [TestMethod]
        public void LoadTest()
        {
            int expected = 17;
            int actual = dc.tblQuestionAnswers.Count();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void InsertTest()
        {
            tblQuestionAnswer newRow = new tblQuestionAnswer();

            newRow.Id = Guid.NewGuid();
            newRow.QuestionId = dc.tblQuestions.FirstOrDefault().Id;
            newRow.AnswerId = dc.tblAnswers.FirstOrDefault().Id;
            newRow.isCorrect = false;

            dc.tblQuestionAnswers.Add(newRow);
            int result = dc.SaveChanges();

            Assert.IsTrue(result > 0);
        }

        [TestMethod]
        public void UpdateTest()
        {

            tblQuestionAnswer existingrow = dc.tblQuestionAnswers.FirstOrDefault(c => c.AnswerId == Guid.Parse("858CA0DD-E16A-4C79-A38C-7BEAE7C5930F"));

            if (existingrow != null)
            {
                existingrow.isCorrect = true;
                dc.SaveChanges();
            }

            Assert.IsTrue(existingrow.isCorrect = true);
        }

        [TestMethod]
        public void DeleteTest()
        {
            InsertTest();

            tblQuestionAnswer row = dc.tblQuestionAnswers.FirstOrDefault(c => c.AnswerId == Guid.Parse("858CA0DD-E16A-4C79-A38C-7BEAE7C5930F"));

            if (row != null)
            {
                dc.tblQuestionAnswers.Remove(row);
                dc.SaveChanges();
            }

            tblQuestionAnswer deletedrow = dc.tblQuestionAnswers.FirstOrDefault(c => c.AnswerId == Guid.Parse("858CA0DD-E16A-4C79-A38C-7BEAE7C5930F"));

            Assert.IsNull(deletedrow);
        }

        [TestMethod]
        public void LazyLoadingTest()
        {
            Assert.IsNotNull(dc.tblQuestionAnswers.FirstOrDefault().Answer);
        }
    }
}
