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
            BL.Models.Response response = new BL.Models.Response { ResponseDate = DateTime.Now };
            await base.InsertTestAsync<BL.Models.Response>(response);
        }
    }
}
