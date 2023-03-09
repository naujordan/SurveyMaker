using JTN.SurveyMaker.BL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JTN.SurveyMaker.BL.Test
{
    public class utResponse
    {
        [Test]
        public async Task InsertTest()
        {
            int results = await ResponseManager.Insert(new Response { Id = Guid.NewGuid(), QuestionId = new Guid("0829D95B-A089-4612-92BD-E914684D274A"), AnswerId = new Guid("297B147D-163D-451E-8CE1-90F5892C46EA")}, true);
            Assert.IsTrue(results == 1);

        }

        [Test]
        public async Task LoadByQuestionIdTest()
        {
            Guid question = new Guid("1F7EDCDA-1888-4C43-AFBE-62496D9F043E");
            var task = await ResponseManager.Load(question);
            IEnumerable<Response> responses = task;
            Assert.That(responses.ToList().Count, Is.EqualTo(1));
        }
    }
}
