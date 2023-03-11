using Azure;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JTN.SurveyMaker.API.Test
{
    [TestClass]
    public class utResponse : utBase
    {
        [TestMethod]
        public async Task InsertTestAsync()
        {
            BL.Models.Response response = new BL.Models.Response { QuestionId = new Guid("0829D95B-A089-4612-92BD-E914684D274A"), AnswerId = new Guid("297B147D-163D-451E-8CE1-90F5892C46EA") };
            await base.InsertTestAsync<BL.Models.Response>(response);
        }
    }
}
