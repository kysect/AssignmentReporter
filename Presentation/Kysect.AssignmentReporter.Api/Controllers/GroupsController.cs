using System.ComponentModel.DataAnnotations;
using Kysect.AssignmentReporter.Application;
using Kysect.AssignmentReporter.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Kysect.AssignmentReporter.Api.Controllers;

[ApiController]
public class GroupsController : Controller
{
    private EntitiesService _service;

    public GroupsController(EntitiesService service)
    {
        _service = service;
    }

    [HttpPost("Groups/Add")]
    public IActionResult CreateGroup([Required] [FromBody] GroupCreationalDto group)
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

    [HttpGet("Groups/Get")]
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

    [HttpGet("Group/{groupName}/Get")]
    public IActionResult GetGroups([Required] [FromRoute] string groupName)
    {
        try
        {
            return Ok(_service.GetGroup(groupName));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete("Group/{groupName}/Delete")]
    public IActionResult DeleteGroup([Required] [FromRoute] string groupName)
    {
        try
        {
            _service.DeleteGroup(groupName);
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}