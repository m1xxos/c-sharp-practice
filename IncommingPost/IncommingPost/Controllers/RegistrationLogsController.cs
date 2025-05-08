using IncommingPost.Domain;
using IncommingPost.Domain.Interfaces;
using IncommingPost.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IncommingPost.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RegistrationLogsController : ControllerBase
{
    private readonly IRegistrationLogRepository _logRepository;

    public RegistrationLogsController(IRegistrationLogRepository logRepository)
    {
        _logRepository = logRepository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<RegistrationLogDto>>> GetAllLogs()
    {
        var logs = await _logRepository.GetAllAsync();
        
        var logDtos = logs.Select(l => new RegistrationLogDto
        {
            Id = l.Id,
            LogDate = l.LogDate,
            Action = l.Action,
            DocumentId = l.DocumentId
        }).ToList();

        return Ok(logDtos);
    }

    [HttpGet("by-document/{documentId}")]
    public async Task<ActionResult<IEnumerable<RegistrationLogDto>>> GetLogsByDocument(int documentId)
    {
        var logs = await _logRepository.GetLogsByDocumentIdAsync(documentId);
        
        var logDtos = logs.Select(l => new RegistrationLogDto
        {
            Id = l.Id,
            LogDate = l.LogDate,
            Action = l.Action,
            DocumentId = l.DocumentId
        }).ToList();

        return Ok(logDtos);
    }

    [HttpGet("by-date-range")]
    public async Task<ActionResult<IEnumerable<RegistrationLogDto>>> GetLogsByDateRange([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
    {
        var logs = await _logRepository.GetLogsByDateRangeAsync(startDate, endDate);
        
        var logDtos = logs.Select(l => new RegistrationLogDto
        {
            Id = l.Id,
            LogDate = l.LogDate,
            Action = l.Action,
            DocumentId = l.DocumentId
        }).ToList();

        return Ok(logDtos);
    }

    [HttpPost]
    public async Task<ActionResult<RegistrationLogDto>> CreateLogEntry(CreateLogEntryDto createLogEntryDto)
    {
        await _logRepository.AddLogEntryAsync(createLogEntryDto.DocumentId, createLogEntryDto.Action);
        
        return Ok();
    }
}