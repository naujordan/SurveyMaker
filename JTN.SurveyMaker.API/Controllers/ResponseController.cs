using JTN.SurveyMaker.BL;
using JTN.SurveyMaker.BL.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace JTN.SurveyMaker.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResponseController : ControllerBase
    {

        // POST api/<ResponseController>
        [HttpPost("{rollback?}")]
        public async Task<ActionResult> Post([FromBody] Response response, bool rollback = false)
        {
            try
            {
                await ResponseManager.Insert(response, rollback);
                return Ok(response.Id);
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
