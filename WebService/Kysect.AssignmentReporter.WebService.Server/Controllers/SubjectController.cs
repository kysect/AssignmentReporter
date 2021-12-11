using System;
using System.ComponentModel.DataAnnotations;
using Kysect.AssignmentReporter.WebService.Server.Service;
using Microsoft.AspNetCore.Mvc;

namespace Kysect.AssignmentReporter.WebService.Server.Controllers;

[ApiController]
public class SubjectController : Controller
{
    private readonly EntitiesService _service;

    public SubjectController(EntitiesService service)
    {
        _service = service;
    }

    [HttpPost("Subjects/Create{subjectName}s")]
    public IActionResult Create([Required] [FromRoute] string subjectName)
    {
        try
        {
            _service.CreateSubject(subjectName);
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete("Subjects/Delete/{subjectName}")]
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

    [HttpGet("Subjects/GetAll")]
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