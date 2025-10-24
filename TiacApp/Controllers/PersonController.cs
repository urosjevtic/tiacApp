using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TiacApp.Application.DTOs;
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
            var result = await _personService.GetPersons();

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<object>> AddPerson([FromBody] PersoneInputDTO newPerson)
        {
            var result = await _personService.AddPerson(newPerson);
            return Ok(result);
        }
    }
}
 