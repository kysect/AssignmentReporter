using System;
using System.ComponentModel.DataAnnotations;
using Kysect.AssignmentReporter.WebService.Server.Service;
using Kysect.AssignmentReporter.WebService.Shared;
using Microsoft.AspNetCore.Mvc;

namespace Kysect.AssignmentReporter.WebService.Server.Controllers
{
    [ApiController]
    public class GroupsController : Controller
    {
        private EntitiesService _service;

        public GroupsController(EntitiesService service)
        {
            _service = service;
        }

        [HttpPost("Groups/Add")]
        public IActionResult CreateGroup([Required] [FromBody] GroupDto group)
        {
            try
            {
                return Ok(_service.AddGroup(group));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("Groups/GetAll")]
        public IActionResult GetGroupNames()
        {
            try
            {
                return Ok(_service.GetGroups());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("Groups/Get")]
        public IActionResult GetGroups([Required] [FromBody] MinimalGroupDto group)
        {
            try
            {
                return Ok(_service.GetGroup(group));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("Groups/Delete")]
        public IActionResult DeleteGroup([Required] [FromBody] GroupDto group)
        {
            try
            {
                _service.DeleteGroup(group);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("Groups/GetStudents")]
        public IActionResult DeleteStudent([Required] [FromBody] MinimalGroupDto group)
        {
            try
            {
                return Ok(_service.GetGroupStudents(group));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}