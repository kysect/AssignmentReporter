using System;
using System.ComponentModel.DataAnnotations;
using Kysect.AssignmentReporter.WebService.Server.Service;
using Kysect.AssignmentReporter.WebService.Shared;
using Kysect.AssignmentReporter.WebService.Shared.CreationalDto;
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

        [HttpGet("Teachers/Get")]
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
        public IActionResult Add([Required] [FromBody] TeacherCreationalDto teacher)
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

        [HttpDelete("Teacher/{teacherId}/Delete")]
        public IActionResult Delete([Required] [FromRoute] Guid teacherId)
        {
            try
            {
                _service.DeleteTeacher(teacherId);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("Teacher/{id}/Get")]
        public IActionResult Get([Required] [FromRoute] Guid id)
        {
            try
            {
                return Ok(_service.GetTeacher(id));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}