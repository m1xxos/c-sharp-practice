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
    public class DocumentsController : ControllerBase
    {
        private readonly IDocumentRepository _documentRepository;

        public DocumentsController(IDocumentRepository documentRepository)
        {
            _documentRepository = documentRepository;
        }

        // GET: api/Documents
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Document>>> GetDocuments()
        {
            var documents = await _documentRepository.GetAllAsync();
            return Ok(documents);
        }

        // GET: api/Documents/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Document>> GetDocument(int id)
        {
            var document = await _documentRepository.GetByIdAsync(id);

            if (document == null)
            {
                return NotFound();
            }

            return document;
        }

        // POST: api/Documents
        [HttpPost]
        public async Task<ActionResult<Document>> PostDocument(Document document)
        {
            document.CreationDate = DateTime.Now;
            document.IsValid = false;
            
            await _documentRepository.AddAsync(document);
            await _documentRepository.SaveChangesAsync();

            return CreatedAtAction("GetDocument", new { id = document.Id }, document);
        }

        // PUT: api/Documents/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDocument(int id, Document document)
        {
            if (id != document.Id)
            {
                return BadRequest();
            }

            await _documentRepository.UpdateAsync(document);
            
            try
            {
                await _documentRepository.SaveChangesAsync();
            }
            catch
            {
                throw;
            }

            return NoContent();
        }

        // DELETE: api/Documents/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDocument(int id)
        {
            var document = await _documentRepository.GetByIdAsync(id);
            if (document == null)
            {
                return NotFound();
            }

            await _documentRepository.DeleteAsync(document);
            await _documentRepository.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/Documents/5/assign-number
        [HttpPost("{id}/assign-number")]
        public async Task<ActionResult<Document>> AssignDocumentNumber(int id)
        {
            var document = await _documentRepository.AssignRegistrationNumberAsync(id);
            
            if (document == null)
            {
                return NotFound();
            }

            return Ok(document);
        }

        // GET: api/Documents/bydate?start=2023-01-01&end=2023-01-31
        [HttpGet("bydate")]
        public async Task<ActionResult<IEnumerable<Document>>> GetDocumentsByDateRange([FromQuery] DateTime start, [FromQuery] DateTime end)
        {
            var documents = await _documentRepository.GetDocumentsByDateRangeAsync(start, end);
            return Ok(documents);
        }
    }
}