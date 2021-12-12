using System;
using System.ComponentModel.DataAnnotations;
using Kysect.AssignmentReporter.WebService.Server.Service;
using Kysect.AssignmentReporter.WebService.Shared;
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

    [HttpPost("Subjects/Create")]
    public IActionResult Create([Required] [FromBody] SubjectDto subjectDto)
    {
        try
        {
            return Ok(_service.CreateSubject(subjectDto));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete("Subjects/Delete")]
    public IActionResult Delete([Required] [FromBody] SubjectDto subjectDto)
    {
        try
        {
            _service.DeleteSubject(subjectDto);
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