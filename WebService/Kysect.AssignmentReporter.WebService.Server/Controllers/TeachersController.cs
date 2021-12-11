using System;
using Kysect.AssignmentReporter.WebService.Server.Service;
using Microsoft.AspNetCore.Mvc;

namespace Kysect.AssignmentReporter.WebService.Server.Controllers
{
    [ApiController]
    public class TeachersController : Controller
    {
        private readonly EntitiesService _service;

        public TeachersController(EntitiesService service)
        {
            _service = service;
        }

        [HttpGet("Teachers/GetAll")]
        public IActionResult GetAll()
        {
            try
            {
                return Ok(_service.GetTeachers());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("Teachers/Add{fullName}")]
        public IActionResult Add([FromRoute] string fullName)
        {
            try
            {
                _service.AddTeacher(fullName);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("Teachers/Delete/{id}")]
        public IActionResult Delete([FromRoute] Guid id)
        {
            try
            {
                _service.DeleteTeacher(id);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}