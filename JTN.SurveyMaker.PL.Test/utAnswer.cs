using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JTN.SurveyMaker.PL.Test
{
    [TestClass]
    public class utAnswer
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
            int expected = 14;
            int actual = dc.tblAnswers.Count();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void InsertTest()
        {
            tblAnswer newRow = new tblAnswer();

            newRow.Id = Guid.NewGuid();
            newRow.Answer = "This is a test";

            dc.tblAnswers.Add(newRow);
            int result = dc.SaveChanges();

            Assert.IsTrue(result == 1);
        }

        [TestMethod]
        public void UpdateTest()
        {
            InsertTest();

            tblAnswer existingrow = dc.tblAnswers.FirstOrDefault(c => c.Answer == "This is a test");

            if (existingrow != null)
            {
                existingrow.Answer = "UpdatedAnswer";
                dc.SaveChanges();
            }

            tblAnswer row = dc.tblAnswers.FirstOrDefault(c => c.Answer == "UpdatedAnswer");

            Assert.AreEqual(existingrow.Answer, row.Answer);
        }

        [TestMethod]
        public void DeleteTest()
        {
            InsertTest();

            tblAnswer row = dc.tblAnswers.FirstOrDefault(c => c.Answer == "This is a test");

            if (row != null)
            {
                dc.tblAnswers.Remove(row);
                dc.SaveChanges();
            }

            tblAnswer deletedrow = dc.tblAnswers.FirstOrDefault(c => c.Answer == "This is a test");

            Assert.IsNull(deletedrow);
        }
    }
}
