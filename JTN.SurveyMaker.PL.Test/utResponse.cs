using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JTN.SurveyMaker.PL.Test
{
    [TestClass]
    public class utResponse
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
            int expected = 6;
            int actual = dc.tblResponses.Count();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void InsertTest()
        {
            tblResponse newRow = new tblResponse();

            newRow.Id = new Guid("8C68718D-5B9F-45EE-95B7-5D0EDA1CBBF2");
            newRow.QuestionId = new Guid("8C68718D-5B9F-45EE-95B7-5D0EDA1CBBF1");
            newRow.AnswerId = new Guid("CF01862F-2038-410C-AB22-F35C58784B5F");
            newRow.ResponseDate = new DateTime(2022, 10, 10); 

            dc.tblResponses.Add(newRow);
            int result = dc.SaveChanges();

            Assert.IsTrue(result == 1);
        }

        [TestMethod]
        public void UpdateTest()
        {
            InsertTest();

            tblResponse existingrow = dc.tblResponses.FirstOrDefault(c => c.Id == new Guid("8C68718D-5B9F-45EE-95B7-5D0EDA1CBBF2"));

            if (existingrow != null)
            {
                existingrow.ResponseDate = new DateTime(2022, 01, 10);
                dc.SaveChanges();
            }

            tblResponse row = dc.tblResponses.FirstOrDefault(c => c.Id == new Guid("8C68718D-5B9F-45EE-95B7-5D0EDA1CBBF2"));

            Assert.AreEqual(existingrow.Question, row.Question);
        }

        [TestMethod]
        public void DeleteTest()
        {
            InsertTest();

            tblResponse row = dc.tblResponses.FirstOrDefault(c => c.Id == new Guid("8C68718D-5B9F-45EE-95B7-5D0EDA1CBBF2"));

            if (row != null)
            {
                dc.tblResponses.Remove(row);
                dc.SaveChanges();
            }

            tblResponse deletedrow = dc.tblResponses.FirstOrDefault(c => c.Id == new Guid("8C68718D-5B9F-45EE-95B7-5D0EDA1CBBF2"));

            Assert.IsNull(deletedrow);
        }
    }
}

