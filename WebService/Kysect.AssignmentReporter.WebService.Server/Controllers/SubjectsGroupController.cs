using System;
using System.ComponentModel.DataAnnotations;
using Kysect.AssignmentReporter.WebService.Server.Service;
using Microsoft.AspNetCore.Mvc;

namespace Kysect.AssignmentReporter.WebService.Server.Controllers
{
    [ApiController]
    public class SubjectsGroupController : Controller
    {
        private readonly EntitiesService _service;

        public SubjectsGroupController(EntitiesService service)
        {
            _service = service;
        }

        [HttpGet("SubjectGroups/GetAll")]
        public IActionResult GetAll()
        {
            try
            {
                return Ok(_service.GetSubjectGroups());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("SubjectGroups/Add")]
        public IActionResult Add([Required] [FromQuery] Guid teacherId, [Required] [FromQuery] string subjectName)
        {
            try
            {
                _service.AddSubjectGroup(subjectName, teacherId);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("SubjectGroups/Delete/{id}")]
        public IActionResult Delete([Required] [FromRoute] Guid id)
        {
            try
            {
                _service.DeleteSubjectGroup(id);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}