using JTN.SurveyMaker.BL.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JTN.SurveyMaker.API.Test
{
    [TestClass]
    public class utQuestion : utBase
    {
        [TestMethod]
        public async Task LoadTestAsync()
        {
            await base.LoadTestAsync<Question>();
        }

        [TestMethod]
        public async Task LoadByIdTestAsync()
        {
            await base.LoadByIdTestAsync<Question>(new KeyValuePair<string, string>("Text", "Who has won more NBA championships?"));
        }

        [TestMethod]
        public async Task InsertTestAsync()
        {
            Question question = new Question {Id = Guid.NewGuid(), Text = "Is this for the API test?", Activations = new List<Activation>(), Answers = new List<Answer>() };
            await base.InsertTestAsync<Question>(question);
        }

        [TestMethod]
        public async Task UpdateTestAsync()
        {
            Question question = new Question { Text = "Was this for the API test?" };
            await base.UpdateTestAsync<Question>(new KeyValuePair<string, string>("Text", "Who has won more NBA championships?"), question);
        }
    }
}
