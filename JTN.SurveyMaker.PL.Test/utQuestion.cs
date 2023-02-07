using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JTN.SurveyMaker.PL.Test
{
    [TestClass]
    public class utQuestion
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
            int expected = 5;
            int actual = dc.tblQuestions.Count();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void InsertTest()
        {
            tblQuestion newRow = new tblQuestion();

            newRow.Id = Guid.NewGuid();
            newRow.Question = "Is this a test";

            dc.tblQuestions.Add(newRow);
            int result = dc.SaveChanges();

            Assert.IsTrue(result == 1);
        }

        [TestMethod]
        public void UpdateTest()
        {
            InsertTest();

            tblQuestion existingrow = dc.tblQuestions.FirstOrDefault(c => c.Question == "Is this a test");

            if (existingrow != null)
            {
                existingrow.Question = "UpdatedQuestion";
                dc.SaveChanges();
            }

            tblQuestion row = dc.tblQuestions.FirstOrDefault(c => c.Question == "UpdatedQuestion");

            Assert.AreEqual(existingrow.Question, row.Question);
        }

        [TestMethod]
        public void DeleteTest()
        {
            InsertTest();

            tblQuestion row = dc.tblQuestions.FirstOrDefault(c => c.Question == "Is this a test");

            if (row != null)
            {
                dc.tblQuestions.Remove(row);
                dc.SaveChanges();
            }

            tblQuestion deletedrow = dc.tblQuestions.FirstOrDefault(c => c.Question == "Is this a test");

            Assert.IsNull(deletedrow);
        }
    }
}
