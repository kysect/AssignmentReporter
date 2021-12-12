using System;
using System.ComponentModel.DataAnnotations;
using Kysect.AssignmentReporter.WebService.Server.Service;
using Kysect.AssignmentReporter.WebService.Shared;
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
        public IActionResult Add([Required] [FromBody] MinimalSubjectGroupDto groupDto)
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

        [HttpDelete("SubjectGroups/Delete")]
        public IActionResult Delete([Required] [FromBody] MinimalSubjectGroupDto groupDto)
        {
            try
            {
                _service.DeleteSubjectGroup(groupDto);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("SubjectGroups/Get")]
        public IActionResult GetSubjectGroup([Required] [FromBody] MinimalSubjectGroupDto groupDto)
        {
            try
            {
                return Ok(_service.GetSubjectGroup(groupDto));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("SubjectGroups/AddStudent")]
        public IActionResult AddStudent([Required] [FromBody] StudentDto studentDto, [Required] [FromBody] MinimalSubjectGroupDto subjectGroupDto)
        {
            try
            {
                return Ok(_service.AddStudentToSubjectGroup(studentDto, subjectGroupDto));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("SubjectGroups/DeleteStudent")]
        public IActionResult DeleteStudent([Required] [FromBody] StudentDto studentDto, [Required] [FromBody] MinimalSubjectGroupDto subjectGroupDto)
        {
            try
            {
                _service.DeleteStudentFromSubjectGroup(studentDto, subjectGroupDto);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}