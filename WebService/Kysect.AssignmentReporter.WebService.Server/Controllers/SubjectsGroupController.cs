using System;
using System.ComponentModel.DataAnnotations;
using Kysect.AssignmentReporter.WebService.Server.Service;
using Kysect.AssignmentReporter.WebService.Shared.CreationalDto;
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

        [HttpGet("SubjectGroups/Get")]
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
        public IActionResult Add([Required] [FromBody] SubjectGroupCreationalDto groupDto)
        {
            try
            {
                return Ok(_service.AddSubjectGroup(groupDto));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("SubjectGroup/{groupId}/Delete")]
        public IActionResult Delete([Required] [FromRoute] Guid groupId)
        {
            try
            {
                _service.DeleteSubjectGroup(groupId);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("SubjectGroup/{groupId}/Get")]
        public IActionResult GetSubjectGroup([Required] [FromRoute] Guid groupId)
        {
            try
            {
                return Ok(_service.GetSubjectGroup(groupId));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("SubjectGroup/{groupId}/AddStudent/{studentId}")]
        public IActionResult AddStudent([Required] [FromRoute] Guid studentId, [Required] [FromRoute] Guid groupId)
        {
            try
            {
                return Ok(_service.AddStudentToSubjectGroup(studentId, groupId));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("SubjectGroup/{groupId}/DeleteStudent/{studentId}")]
        public IActionResult DeleteStudent([Required] [FromRoute] Guid studentId, [Required] [FromRoute] Guid groupId)
        {
            try
            {
                _service.DeleteStudentFromSubjectGroup(studentId, groupId);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}