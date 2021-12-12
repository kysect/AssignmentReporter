using System;
using System.ComponentModel.DataAnnotations;
using Kysect.AssignmentReporter.WebService.Server.Service;
using Kysect.AssignmentReporter.WebService.Shared;
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

        [HttpPost("Teachers/Add")]
        public IActionResult Add([Required] [FromBody] MinimalTeacherDto teacher)
        {
            try
            {
                return Ok(_service.AddTeacher(teacher));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("Teachers/Delete")]
        public IActionResult Delete([Required] [FromBody] MinimalTeacherDto teacher)
        {
            try
            {
                _service.DeleteTeacher(teacher);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("Teachers/Get")]
        public IActionResult Get([Required] [FromBody] MinimalTeacherDto teacher)
        {
            try
            {
                return Ok(_service.GetTeacher(teacher));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}