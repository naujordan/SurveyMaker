using JTN.SurveyMaker.BL;
using JTN.SurveyMaker.BL.Models;
using Microsoft.AspNetCore.Mvc;
using System;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace JTN.SurveyMaker.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActivationController : ControllerBase
    {
        // GET: api/<ActivationController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Activation>>> Get()
        {
            try
            {
                List<Question> questions = new List<Question>();
                questions = await QuestionManager.Load();
                List<Activation> activations = new List<Activation>();

                foreach (Question question in questions) 
                { 
                    foreach(Activation activation in question.Activations) 
                    {
                        activations.Add(activation);
                    }
                }

                return Ok(activations);
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }

        // POST api/<ActivationController>
        [HttpPost("{rollback?}")]
        public async Task<ActionResult> Post([FromBody] Activation activation, bool rollback = false)
        {
            try
            {
                await ActivationManager.Insert(activation, rollback);
                return Ok(activation.Id);
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // PUT api/<ColorController>/5
        [HttpPut("{id}/{rollback?}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] Activation activation, bool rollback = false)
        {
            try
            {
                return Ok(await ActivationManager.Update(activation, rollback));
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
