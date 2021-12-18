using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Kysect.AssignmentReporter.Models;
using Kysect.AssignmentReporter.WebService.Server.Service;
using Kysect.AssignmentReporter.WebService.Shared;
using Kysect.AssignmentReporter.WebService.Shared.CreationalDto;
using Microsoft.AspNetCore.Mvc;

namespace Kysect.AssignmentReporter.WebService.Server.Controllers
{
    [ApiController]
    public class ReportsController : Controller
    {
        private ReportsService _service;
        public ReportsController(ReportsService service)
        {
            _service = service;
        }

        [HttpGet("Reports/GetGitRepositories")]
        public IActionResult GetRepositories([Required] [FromQuery] string gitHubToken)
        {
            try
            {
                IReadOnlyList<GithubRepositoryInfo> result = _service.GetRepositories(gitHubToken);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Reports/CreateReports")]
        public IActionResult CreateReport([Required] [FromBody] RepositoryCreationalInfoDto info)
        {
            try
            {
                _service.CreateReports(info);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("Reports/Get")]
        public IActionResult GetReports()
        {
            try
            {
                IReadOnlyList<ReportDto> result = _service.GetReports();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Report/{id}/Download/")]
        public FileResult DownloadReport([Required] [FromRoute] Guid id)
        {
            FileDto file = _service.DownloadReport(id);
            return File(file.Stream, System.Net.Mime.MediaTypeNames.Application.Octet, file.Name);
        }

        [HttpDelete("Reports/{id}/Delete/")]
        public IActionResult DeleteReport([Required] [FromRoute] Guid id)
        {
            try
            {
                _service.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}