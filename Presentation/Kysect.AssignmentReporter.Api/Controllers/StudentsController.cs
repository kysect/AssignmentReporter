using System;
using System.ComponentModel.DataAnnotations;
using Kysect.AssignmentReporter.WebService.Server.Service;
using Kysect.AssignmentReporter.WebService.Shared.CreationalDto;
using Microsoft.AspNetCore.Mvc;

namespace Kysect.AssignmentReporter.WebService.Server.Controllers
{
    [ApiController]
    public class StudentsController : Controller
    {
        private EntitiesService _service;

        public StudentsController(EntitiesService service)
        {
            _service = service;
        }

        [HttpPost("Students/Add")]
        public IActionResult CreateStudent([Required] [FromBody] StudentCreationalDto student)
        {
            try
            {
                return Ok(_service.AddStudent(student));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("Student/{studentId}/Delete")]
        public IActionResult DeleteStudent([Required] [FromRoute] Guid studentId)
        {
            try
            {
                _service.DeleteStudent(studentId);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("Students/Get")]
        public IActionResult DeleteStudent()
        {
            try
            {
                return Ok(_service.GetStudents());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("Student/{studentId}/Move/{newGroupName}")]
        public IActionResult Move([Required] [FromRoute] Guid studentId, [Required] [FromRoute] string newGroupName)
        {
            try
            {
                return Ok(_service.MoveStudent(studentId, newGroupName));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}