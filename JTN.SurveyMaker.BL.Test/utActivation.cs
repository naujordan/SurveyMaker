using JTN.SurveyMaker.BL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JTN.SurveyMaker.BL.Test
{
    public class utActivation
    {
        [Test]
        public async Task InsertTest()
        {
            int results = await ActivationManager.Insert(new Activation { Id = Guid.NewGuid(), QuestionId = new Guid("0829D95B-A089-4612-92BD-E914684D274A"), StartDate = new DateTime(2023, 03, 01), EndDate = new DateTime(2023, 03, 02), ActivationCode = "000999" }, true);
            Assert.IsTrue(results > 0);

        }

        [Test]
        public async Task UpdateTest()
        {

            IEnumerable<Question> questions = await QuestionManager.Load();
            Question question = questions.FirstOrDefault(c => c.Id == new Guid("0F390814-9E21-4D29-89AF-661ABF7DA225"));
            Activation activation = question.Activations.FirstOrDefault(c => c.QuestionId == new Guid("0F390814-9E21-4D29-89AF-661ABF7DA225"));
            activation.ActivationCode = "Test00";
            int results = await ActivationManager.Update(activation, true);
            Assert.IsTrue(results > 0);

        }

        [Test]
        public async Task DeleteTest()
        {
            Task.Run(async () =>
            {
                var task = await QuestionManager.Load();
                IEnumerable<Question> questions = task;
                Question question = questions.FirstOrDefault(c => c.Id == new Guid("0F390814-9E21-4D29-89AF-661ABF7DA225"));
                Activation activation = question.Activations.FirstOrDefault(c => c.QuestionId == new Guid("0F390814-9E21-4D29-89AF-661ABF7DA225"));
                int results = await ActivationManager.Delete(activation.Id, true);
                Assert.IsTrue(results > 0);

            });


        }
    }
}
