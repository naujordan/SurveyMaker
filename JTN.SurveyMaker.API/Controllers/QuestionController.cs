﻿using JTN.SurveyMaker.BL;
using JTN.SurveyMaker.BL.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace JTN.SurveyMaker.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        // GET: api/<QuestionController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Question>>> Get()
        {
            try
            {
                return Ok(await QuestionManager.Load());
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }

        // GET api/<QuestionController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Question>> Get(Guid id)
        {
            try
            {
                return Ok(await QuestionManager.LoadById(id));
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // POST api/<QuestionController>
        [HttpPost("{rollback?}")]
        public async Task<ActionResult> Post([FromBody] Question question, bool rollback = false)
        {
            try
            {
                await QuestionManager.Insert(question, rollback);
                return Ok(question.Id);
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // PUT api/<QuestionController>/5
        [HttpPut("{id}/{rollback?}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] Question question, bool rollback = false)
        {
            try
            {
                return Ok(await QuestionManager.Update(question, rollback));
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("{activationCode}")]
        public async Task<ActionResult<Question>> Get(string activationCode)
        {
            try
            {
                return Ok(await QuestionManager.Load(activationCode));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

    }
}
