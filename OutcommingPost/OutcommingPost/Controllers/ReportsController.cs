using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OutcommingPost.Domain;
using OutcommingPost.Domain.Interfaces;

namespace OutcommingPost.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportsController : ControllerBase
    {
        private readonly IReportRepository _reportRepository;

        public ReportsController(IReportRepository reportRepository)
        {
            _reportRepository = reportRepository;
        }

        // GET: api/Reports
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Report>>> GetReports()
        {
            var reports = await _reportRepository.GetAllAsync();
            return Ok(reports);
        }

        // GET: api/Reports/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Report>> GetReport(int id)
        {
            var report = await _reportRepository.GetReportWithDocumentsAsync(id);

            if (report == null)
            {
                return NotFound();
            }

            return report;
        }

        // POST: api/Reports/generate
        [HttpPost("generate")]
        public async Task<ActionResult<Report>> GenerateReport([FromQuery] DateTime start, [FromQuery] DateTime end)
        {
            var report = await _reportRepository.GenerateReportAsync(start, end);
            return CreatedAtAction("GetReport", new { id = report.Id }, report);
        }

        // DELETE: api/Reports/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReport(int id)
        {
            var report = await _reportRepository.GetByIdAsync(id);
            if (report == null)
            {
                return NotFound();
            }

            await _reportRepository.DeleteAsync(report);
            await _reportRepository.SaveChangesAsync();

            return NoContent();
        }
    }
}