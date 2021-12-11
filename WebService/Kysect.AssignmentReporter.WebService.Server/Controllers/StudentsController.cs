using System;
using System.ComponentModel.DataAnnotations;
using Kysect.AssignmentReporter.WebService.Server.Service;
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

        [HttpPut("Students/Add")]
        public IActionResult CreateStudent([Required] [FromQuery] string fullName, [Required] [FromQuery] string groupName)
        {
            try
            {
                _service.AddStudent(fullName, groupName);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("Students/Delete/{studentId}")]
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

        [HttpGet("Students/GetGroupStudents/{groupName}")]
        public IActionResult DeleteStudent([Required] [FromRoute] string groupName)
        {
            try
            {
                return Ok(_service.GetGroupStudents(groupName));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("Students/GetAll")]
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

        [HttpGet("Students/Get/{studentId}")]
        public IActionResult Get([Required] [FromRoute] Guid studentId)
        {
            try
            {
                return Ok(_service.GetStudent(studentId));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("Students/Move")]
        public IActionResult Move([Required] [FromQuery] Guid studentId, [Required] [FromQuery] string groupName)
        {
            try
            {
                _service.MoveStudent(studentId, groupName);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("Students/AddStudentToSubjectGroup")]
        public IActionResult AddStudentToSubjectGroup([Required] [FromQuery] Guid studentId, [Required] [FromQuery] Guid groupId)
        {
            try
            {
                _service.AddStudentToSubjectGroup(studentId, groupId);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("Students/RemoveStudentFromSubjectGroup")]
        public IActionResult RemoveStudentFromSubjectGroup([Required] [FromQuery] Guid studentId, [Required] [FromQuery] Guid groupId)
        {
            try
            {
                _service.RemoveStudentFromSubjectGroup(studentId, groupId);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}