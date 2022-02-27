using System.ComponentModel.DataAnnotations;
using Kysect.AssignmentReporter.Application;
using Kysect.AssignmentReporter.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Kysect.AssignmentReporter.Api.Controllers;

[ApiController]
public class SubjectController : Controller
{
    private readonly EntitiesService _service;

    public SubjectController(EntitiesService service)
    {
        _service = service;
    }

    [HttpPost("Subjects/Add")]
    public IActionResult Create([Required] [FromBody] SubjectCreationalDto subjectDto)
    {
        try
        {
            return Ok(_service.AddSubject(subjectDto));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete("Subject/{subjectName}/Delete")]
    public IActionResult Delete([Required] [FromRoute] string subjectName)
    {
        try
        {
            _service.DeleteSubject(subjectName);
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("Subjects/Get")]
    public IActionResult GetAll()
    {
        try
        {
            return Ok(_service.GetSubjects());
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}