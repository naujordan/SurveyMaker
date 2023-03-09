using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JTN.SurveyMaker.PL.Test
{
    [TestClass]
    public class utActivation
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
            int expected = 3;
            int actual = dc.tblActivations.Count();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void InsertTest()
        {
            tblActivation newRow = new tblActivation();

            newRow.Id = new Guid("8C68718D-5B9F-45EE-95B7-5D0EDA1CBBF0");
            newRow.QuestionId = new Guid("8C68718D-5B9F-45EE-95B7-5D0EDA1CBBF1");
            newRow.StartDate = new DateTime(2022, 10, 10);
            newRow.EndDate = new DateTime(2022, 11, 10);
            newRow.ActivationCode = "ABC123";

            dc.tblActivations.Add(newRow);
            int result = dc.SaveChanges();

            Assert.IsTrue(result == 1);
        }

        [TestMethod]
        public void UpdateTest()
        {
            InsertTest();

            tblActivation existingrow = dc.tblActivations.FirstOrDefault(c => c.Id == new Guid("8C68718D-5B9F-45EE-95B7-5D0EDA1CBBF0"));

            if (existingrow != null)
            {
                existingrow.EndDate = new DateTime(2023, 11, 10);
                dc.SaveChanges();
            }

            tblActivation row = dc.tblActivations.FirstOrDefault(c => c.Id == new Guid("8C68718D-5B9F-45EE-95B7-5D0EDA1CBBF0"));

            Assert.AreEqual(existingrow.Question, row.Question);
        }

        [TestMethod]
        public void DeleteTest()
        {
            InsertTest();

            tblActivation row = dc.tblActivations.FirstOrDefault(c => c.Id == new Guid("8C68718D-5B9F-45EE-95B7-5D0EDA1CBBF0"));

            if (row != null)
            {
                dc.tblActivations.Remove(row);
                dc.SaveChanges();
            }

            tblActivation deletedrow = dc.tblActivations.FirstOrDefault(c => c.Id == new Guid("8C68718D-5B9F-45EE-95B7-5D0EDA1CBBF0"));

            Assert.IsNull(deletedrow);
        }
    }
}

