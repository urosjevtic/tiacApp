using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TiacApp.Application.DTOs;
using TiacApp.Application.Exceptions;
using TiacApp.Application.Service;
using TiacApp.Application.Service.Interface;
using TiacApp.Models;

namespace TiacApp.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IPersoneService _personService;

        public PersonController(IPersoneService personService)
        {
            _personService = personService;
        }

        [HttpGet]
        public async Task<ActionResult<object>> GetPersons()
        {
            try
            {
                var result = await _personService.GetPeople();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { message = "An error occurred while processing your request" });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<object>> GetPersonById(int id)
        {
            try
            {
                var result = await _personService.GetPersonById(id);
                if (result == null)
                {
                    return NotFound(new { message = $"Person with ID {id} not found" });
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { message = "An error occurred while processing your request" });
            }
        }

        [HttpPost]
        public async Task<ActionResult<object>> AddPerson([FromBody] PersoneInputDTO newPerson)
        {
            try
            {
                var result = await _personService.AddPerson(newPerson);
                return Ok(result);
            }
            catch (ValidationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { message = "An error occurred while saving to the database" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { message = "An unexpected error occurred while processing your request" });
            }

        }
    }
}
 