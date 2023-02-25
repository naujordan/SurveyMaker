using JTN.SurveyMaker.BL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JTN.SurveyMaker.BL.Test
{
    public class utAnswer
    {
        [Test]
        public async Task LoadTest()
        {
            var task = await AnswerManager.Load();
            IEnumerable<Answer> answers = task;
            Assert.AreEqual(14, answers.ToList().Count);
        }

        [Test]
        public async Task UpdateTest()
        {

            IEnumerable<Answer> answers = await AnswerManager.Load();
            Answer answer = answers.FirstOrDefault(c => c.Text == "10");
            answer.Text = "Updated Answer";
            int results = await AnswerManager.Update(answer, true);
            Assert.IsTrue(results > 0);

        }

        [Test]
        public async Task DeleteTest()
        {
            Task.Run(async () =>
            {
                var task = await AnswerManager.Load();
                IEnumerable<Answer> answers = task;
                Answer answer = answers.FirstOrDefault(c => c.Text == "10");
                int results = await AnswerManager.Delete(answer.Id, true);
                Assert.IsTrue(results > 0);

            });
        }

        [Test]
        public async Task InsertTest()
        {
            bool results = await AnswerManager.Insert(new Answer { Id = Guid.NewGuid(), Text = "This is a test answer" }, true);
            Assert.IsTrue(results);

        }

        [Test]
        public async Task LoadByQuestionIdTest()
        {
            Guid question = new Guid("1F7EDCDA-1888-4C43-AFBE-62496D9F043E");
            var task = await AnswerManager.Load(question);
            IEnumerable<Answer> answers = task;
            Assert.AreEqual(2, answers.ToList().Count);
        }

        [Test]
        public async Task LoadByIdTest()
        {
            Guid test = new Guid("CF01862F-2038-410C-AB22-F35C58784B5F");
            var task = await AnswerManager.LoadById(test);
            Assert.AreEqual(test, task.Id);

        }
    }
}
