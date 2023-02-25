using JTN.SurveyMaker.BL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace JTN.SurveyMaker.BL.Test
{
    public class utQuestion
    {
        [Test]
        public async Task InsertTest()
        {
            int results = await QuestionManager.Insert(new Question { Id = Guid.NewGuid(), Text = "This is a test question" }, true);
            Assert.IsTrue(results > 0);

        }

        [Test]
        public async Task LoadTest()
        {
            var task = await QuestionManager.Load();
            IEnumerable<Question> questions = task;
            Assert.AreEqual(5, questions.ToList().Count);
        }

        [Test]
        public async Task UpdateTest()
        {

            IEnumerable<Question> questions = await QuestionManager.Load();
            Question question = questions.FirstOrDefault(c => c.Text == "Who has won more NBA championships?");
            question.Text = "Updated Question";
            int results = await QuestionManager.Update(question, true);
            Assert.IsTrue(results > 0);

        }

        [Test]
        public async Task DeleteTest()
        {
            Task.Run(async () =>
            {
                var task = await QuestionManager.Load();
                IEnumerable<Question> questions = task;
                Question question = questions.FirstOrDefault(c => c.Text == "Who has won more NBA championships?");
                int results = await QuestionManager.Delete(question.Id, true);
                Assert.IsTrue(results > 0);

            });


        }

        [Test]
        public async Task LoadByIdTest()
        {
            Guid test = new Guid("8C68718D-5B9F-45EE-95B7-5D0EDA1CBBF1");
            var task = await QuestionManager.LoadById(test);
            Assert.AreEqual(test, task.Id);
           
        }

    }
}
