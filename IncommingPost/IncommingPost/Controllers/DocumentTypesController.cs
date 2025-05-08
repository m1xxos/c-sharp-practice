using IncommingPost.Domain;
using IncommingPost.Domain.Interfaces;
using IncommingPost.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IncommingPost.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DocumentTypesController : ControllerBase
{
    private readonly IDocumentTypeRepository _documentTypeRepository;

    public DocumentTypesController(IDocumentTypeRepository documentTypeRepository)
    {
        _documentTypeRepository = documentTypeRepository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<DocumentTypeDto>>> GetAllDocumentTypes()
    {
        var types = await _documentTypeRepository.GetAllAsync();
        
        var typeDtos = types.Select(t => new DocumentTypeDto
        {
            Id = t.Id,
            TypeName = t.TypeName,
            Description = t.Description
        }).ToList();

        return Ok(typeDtos);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<DocumentTypeDto>> GetDocumentType(int id)
    {
        var type = await _documentTypeRepository.GetTypeWithDocumentsAsync(id);
        
        if (type == null)
        {
            return NotFound();
        }

        var typeDto = new DocumentTypeDto
        {
            Id = type.Id,
            TypeName = type.TypeName,
            Description = type.Description
        };

        return typeDto;
    }

    [HttpPost]
    public async Task<ActionResult<DocumentTypeDto>> CreateDocumentType(CreateDocumentTypeDto createTypeDto)
    {
        var documentType = new DocumentType
        {
            TypeName = createTypeDto.TypeName,
            Description = createTypeDto.Description
        };

        await _documentTypeRepository.AddAsync(documentType);
        await _documentTypeRepository.SaveChangesAsync();

        return CreatedAtAction(nameof(GetDocumentType), new { id = documentType.Id }, new DocumentTypeDto
        {
            Id = documentType.Id,
            TypeName = documentType.TypeName,
            Description = documentType.Description
        });
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateDocumentType(int id, UpdateDocumentTypeDto updateTypeDto)
    {
        var documentType = await _documentTypeRepository.GetByIdAsync(id);
        
        if (documentType == null)
        {
            return NotFound();
        }

        documentType.TypeName = updateTypeDto.TypeName;
        documentType.Description = updateTypeDto.Description;

        await _documentTypeRepository.UpdateAsync(documentType);
        await _documentTypeRepository.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteDocumentType(int id)
    {
        var documentType = await _documentTypeRepository.GetByIdAsync(id);
        
        if (documentType == null)
        {
            return NotFound();
        }

        await _documentTypeRepository.DeleteAsync(documentType);
        await _documentTypeRepository.SaveChangesAsync();

        return NoContent();
    }
}