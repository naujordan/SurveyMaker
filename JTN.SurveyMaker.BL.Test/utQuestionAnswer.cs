using JTN.SurveyMaker.BL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JTN.SurveyMaker.BL.Test
{
    public class utQuestionAnswer
    {
        [Test]
        public async Task InsertTest()
        {
            Question question = new Question {Id = Guid.NewGuid(), Text="test" };
            Answer answer= new Answer {Id = Guid.NewGuid(), Text="answer test" };
            QuestionManager.Insert(question);
            AnswerManager.Insert(answer);


            int results = await QuestionAnswerManager.Insert(question.Id, answer.Id, false, true);
            Assert.AreEqual(1, results);

            QuestionManager.Delete(question.Id);
            AnswerManager.Delete(answer.Id);
        }

        [Test]
        public async Task DeleteTest()
        {
            Task.Run(async () =>
            {
                Guid question = new Guid("8C68718D-5B9F-45EE-95B7-5D0EDA1CBBF1");
                Guid answer = new Guid("CF01862F-2038-410C-AB22-F35C58784B5F");

                int results = await QuestionAnswerManager.Delete(question, answer, true);
                Assert.IsTrue(results > 0);

            });
        }
    }
}
