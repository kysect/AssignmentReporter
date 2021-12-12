using System;
using System.ComponentModel.DataAnnotations;
using Kysect.AssignmentReporter.WebService.Server.Service;
using Kysect.AssignmentReporter.WebService.Shared;
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
        public IActionResult CreateStudent([Required] [FromBody] StudentDto student)
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

        [HttpDelete("Students/Delete")]
        public IActionResult DeleteStudent([Required] [FromBody] StudentDto student)
        {
            try
            {
                _service.DeleteStudent(student);
                return Ok();
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

        [HttpPut("Students/Move")]
        public IActionResult Move([Required] [FromBody] StudentDto student, [Required] [FromBody] GroupDto group)
        {
            try
            {
                return Ok(_service.MoveStudent(student, group));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}