using IncommingPost.Domain;
using IncommingPost.Infrastructure;
using IncommingPost.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IncommingPost.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SendersController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public SendersController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<SenderDto>>> GetAllSenders()
    {
        var senders = await _context.Senders.ToListAsync();
        
        var senderDtos = senders.Select(s => new SenderDto
        {
            Id = s.Id,
            Name = s.Name,
            ContactDetails = s.ContactDetails,
            Identifiers = s.Identifiers,
            Address = s.Address
        }).ToList();

        return Ok(senderDtos);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<SenderDto>> GetSender(int id)
    {
        var sender = await _context.Senders
            .Include(s => s.Documents)
            .FirstOrDefaultAsync(s => s.Id == id);
        
        if (sender == null)
        {
            return NotFound();
        }

        var senderDto = new SenderDto
        {
            Id = sender.Id,
            Name = sender.Name,
            ContactDetails = sender.ContactDetails,
            Identifiers = sender.Identifiers,
            Address = sender.Address,
            DocumentCount = sender.Documents.Count
        };

        return senderDto;
    }

    [HttpGet("search/{nameFragment}")]
    public async Task<ActionResult<IEnumerable<SenderDto>>> SearchSendersByName(string nameFragment)
    {
        var senders = await _context.Senders
            .Where(s => s.Name.Contains(nameFragment))
            .ToListAsync();
        
        var senderDtos = senders.Select(s => new SenderDto
        {
            Id = s.Id,
            Name = s.Name,
            ContactDetails = s.ContactDetails,
            Identifiers = s.Identifiers,
            Address = s.Address
        }).ToList();

        return Ok(senderDtos);
    }

    [HttpPost]
    public async Task<ActionResult<SenderDto>> CreateSender(CreateSenderDto dto)
    {
        var sender = new Sender
        {
            Name = dto.Name,
            ContactDetails = dto.ContactDetails,
            Identifiers = dto.Identifiers,
            Address = dto.Address
        };

        _context.Senders.Add(sender);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetSender), new { id = sender.Id }, new SenderDto
        {
            Id = sender.Id,
            Name = sender.Name,
            ContactDetails = sender.ContactDetails,
            Identifiers = sender.Identifiers,
            Address = sender.Address
        });
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateSender(int id, UpdateSenderDto dto)
    {
        var sender = await _context.Senders.FindAsync(id);
        
        if (sender == null)
        {
            return NotFound();
        }

        sender.Name = dto.Name;
        sender.ContactDetails = dto.ContactDetails;
        sender.Address = dto.Address;

        _context.Entry(sender).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteSender(int id)
    {
        var sender = await _context.Senders.FindAsync(id);
        
        if (sender == null)
        {
            return NotFound();
        }

        // Проверяем, есть ли документы от этого отправителя
        var hasDocuments = await _context.Documents
            .AnyAsync(d => d.SenderId == id);
            
        if (hasDocuments)
        {
            return BadRequest("Cannot delete sender with associated documents");
        }

        _context.Senders.Remove(sender);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}