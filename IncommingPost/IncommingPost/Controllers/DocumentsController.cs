using IncommingPost.Domain;
using IncommingPost.Infrastructure;
using IncommingPost.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IncommingPost.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DocumentsController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public DocumentsController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<DocumentDto>>> GetAllDocuments()
    {
        var documents = await _context.Documents
            .Include(d => d.ReceivedFrom)
            .Include(d => d.ClassifiedAs)
            .ToListAsync();

        return Ok(documents.Select(doc => new DocumentDto
        {
            Id = doc.Id,
            ReceivedDate = doc.ReceivedDate,
            ReceivedTime = doc.ReceivedTime,
            RegistrationNumber = doc.RegistrationNumber,
            Summary = doc.Summary,
            HasAttachments = doc.HasAttachments,
            ProcessingStatus = doc.ProcessingStatus,
            SenderId = doc.SenderId,
            SenderName = doc.ReceivedFrom?.Name,
            DocumentTypeId = doc.DocumentTypeId,
            DocumentTypeName = doc.ClassifiedAs?.TypeName,
            ExecutorIds = doc.AssignedTo.Select(e => e.Id).ToList()
        }));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<DocumentDto>> GetDocument(int id)
    {
        var document = await _context.Documents
            .Include(d => d.ReceivedFrom)
            .Include(d => d.ClassifiedAs)
            .Include(d => d.AssignedTo)
            .FirstOrDefaultAsync(d => d.Id == id);
        
        if (document == null)
        {
            return NotFound();
        }

        return new DocumentDto
        {
            Id = document.Id,
            ReceivedDate = document.ReceivedDate,
            ReceivedTime = document.ReceivedTime,
            RegistrationNumber = document.RegistrationNumber,
            Summary = document.Summary,
            HasAttachments = document.HasAttachments,
            ProcessingStatus = document.ProcessingStatus,
            SenderId = document.SenderId,
            SenderName = document.ReceivedFrom?.Name,
            DocumentTypeId = document.DocumentTypeId,
            DocumentTypeName = document.ClassifiedAs?.TypeName,
            ExecutorIds = document.AssignedTo.Select(e => e.Id).ToList()
        };
    }

    [HttpPost]
    public async Task<ActionResult<DocumentDto>> CreateDocument(CreateDocumentDto dto)
    {
        var document = new Document
        {
            ReceivedDate = dto.ReceivedDate,
            ReceivedTime = dto.ReceivedTime,
            RegistrationNumber = dto.RegistrationNumber,
            Summary = dto.Summary,
            HasAttachments = dto.HasAttachments,
            SenderId = dto.SenderId,
            DocumentTypeId = dto.DocumentTypeId,
            ProcessingStatus = "New"
        };

        _context.Documents.Add(document);
        await _context.SaveChangesAsync();
        
        // Запись в журнал регистрации
        var logEntry = new RegistrationLog
        {
            DocumentId = document.Id,
            Action = "Document registered",
            LogDate = DateTime.Now
        };
        _context.RegistrationLogs.Add(logEntry);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetDocument), new { id = document.Id }, 
            await GetDocument(document.Id));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateDocument(int id, UpdateDocumentDto dto)
    {
        var document = await _context.Documents.FindAsync(id);
        
        if (document == null)
        {
            return NotFound();
        }

        document.Summary = dto.Summary;
        document.ProcessingStatus = dto.ProcessingStatus;
        document.HasAttachments = dto.HasAttachments;

        _context.Entry(document).State = EntityState.Modified;
        
        // Запись в журнал
        var logEntry = new RegistrationLog
        {
            DocumentId = document.Id,
            Action = "Document updated",
            LogDate = DateTime.Now
        };
        _context.RegistrationLogs.Add(logEntry);
        
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteDocument(int id)
    {
        var document = await _context.Documents.FindAsync(id);
        
        if (document == null)
        {
            return NotFound();
        }

        _context.Documents.Remove(document);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpPost("{documentId}/executors/{executorId}")]
    public async Task<IActionResult> AssignExecutor(int documentId, int executorId)
    {
        var document = await _context.Documents
            .Include(d => d.AssignedTo)
            .FirstOrDefaultAsync(d => d.Id == documentId);
            
        var executor = await _context.Executors.FindAsync(executorId);
        
        if (document == null || executor == null)
        {
            return NotFound();
        }

        document.AssignedTo.Add(executor);
        
        // Запись в журнал
        var logEntry = new RegistrationLog
        {
            DocumentId = document.Id,
            Action = $"Assigned to executor: {executor.FullName}",
            LogDate = DateTime.Now
        };
        _context.RegistrationLogs.Add(logEntry);
        
        await _context.SaveChangesAsync();
        
        return NoContent();
    }

    [HttpGet("by-sender/{senderId}")]
    public async Task<ActionResult<IEnumerable<DocumentDto>>> GetBySender(int senderId)
    {
        var documents = await _context.Documents
            .Include(d => d.ReceivedFrom)
            .Include(d => d.ClassifiedAs)
            .Where(d => d.SenderId == senderId)
            .ToListAsync();
        
        return Ok(documents.Select(doc => new DocumentDto
        {
            Id = doc.Id,
            ReceivedDate = doc.ReceivedDate,
            ReceivedTime = doc.ReceivedTime,
            RegistrationNumber = doc.RegistrationNumber,
            Summary = doc.Summary,
            HasAttachments = doc.HasAttachments,
            ProcessingStatus = doc.ProcessingStatus,
            SenderId = doc.SenderId,
            SenderName = doc.ReceivedFrom?.Name,
            DocumentTypeId = doc.DocumentTypeId,
            DocumentTypeName = doc.ClassifiedAs?.TypeName
        }));
    }

    [HttpGet("by-status/{status}")]
    public async Task<ActionResult<IEnumerable<DocumentDto>>> GetByStatus(string status)
    {
        var documents = await _context.Documents
            .Include(d => d.ReceivedFrom)
            .Include(d => d.ClassifiedAs)
            .Where(d => d.ProcessingStatus == status)
            .ToListAsync();
        
        return Ok(documents.Select(doc => new DocumentDto
        {
            Id = doc.Id,
            ReceivedDate = doc.ReceivedDate,
            ReceivedTime = doc.ReceivedTime,
            RegistrationNumber = doc.RegistrationNumber,
            Summary = doc.Summary,
            HasAttachments = doc.HasAttachments,
            ProcessingStatus = doc.ProcessingStatus,
            SenderId = doc.SenderId,
            SenderName = doc.ReceivedFrom?.Name,
            DocumentTypeId = doc.DocumentTypeId,
            DocumentTypeName = doc.ClassifiedAs?.TypeName
        }));
    }
}