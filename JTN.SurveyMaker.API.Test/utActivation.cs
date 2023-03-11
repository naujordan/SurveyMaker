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
    public class utActivation : utBase
    {
        [TestMethod]
        public async Task InsertTestAsync()
        {
            Activation activation = new Activation { QuestionId = new Guid("0F390814-9E21-4D29-89AF-661ABF7DA225"), StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(7), ActivationCode = "999999" };
            await base.InsertTestAsync<Activation>(activation);
        }

        [TestMethod]
        public async Task DeleteTestAsync()
        {
            await base.DeleteTestAsync<Activation>(new KeyValuePair<string, string>("ActivationCode", "AAAAAA"));
        }

        [TestMethod]
        public async Task UpdateTestAsync()
        {
            Activation activation = new Activation { ActivationCode = "AAAAA2" };
            await base.UpdateTestAsync<Activation>(new KeyValuePair<string, string>("ActivationCode", "AAAAAA"), activation);
        }
    }
}
