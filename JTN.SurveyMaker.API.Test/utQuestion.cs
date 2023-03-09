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
            await base.LoadByIdTestAsync<Question>(new KeyValuePair<string, string>("Question", "Who has won more NBA championships?"));
        }
    }
}
