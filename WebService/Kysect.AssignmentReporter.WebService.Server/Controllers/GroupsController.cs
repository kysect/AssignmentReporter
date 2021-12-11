using System;
using System.ComponentModel.DataAnnotations;
using Kysect.AssignmentReporter.WebService.Server.Service;
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
        public IActionResult CreateGroup([Required] [FromQuery] string groupName)
        {
            try
            {
                _service.AddGroup(groupName);
                return Ok();
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
                return Ok(_service.GetGroupNames());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("Groups/Get/{name}")]
        public IActionResult GetGroups([Required] [FromRoute] string name)
        {
            try
            {
                return Ok(_service.GetGroup(name));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("Groups/Delete/{name}")]
        public IActionResult DeleteGroup([Required] [FromRoute] string name)
        {
            try
            {
                _service.DeleteGroup(name);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}