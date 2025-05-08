using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OutcommingPost.Domain;
using OutcommingPost.Domain.Interfaces;

namespace OutcommingPost.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InitiatorsController : ControllerBase
    {
        private readonly IInitiatorRepository _initiatorRepository;

        public InitiatorsController(IInitiatorRepository initiatorRepository)
        {
            _initiatorRepository = initiatorRepository;
        }

        // GET: api/Initiators
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Initiator>>> GetInitiators()
        {
            var initiators = await _initiatorRepository.GetAllAsync();
            return Ok(initiators);
        }

        // GET: api/Initiators/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Initiator>> GetInitiator(int id)
        {
            var initiator = await _initiatorRepository.GetByIdAsync(id);

            if (initiator == null)
            {
                return NotFound();
            }

            return initiator;
        }

        // POST: api/Initiators
        [HttpPost]
        public async Task<ActionResult<Initiator>> PostInitiator(Initiator initiator)
        {
            await _initiatorRepository.AddAsync(initiator);
            await _initiatorRepository.SaveChangesAsync();

            return CreatedAtAction("GetInitiator", new { id = initiator.Id }, initiator);
        }

        // PUT: api/Initiators/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInitiator(int id, Initiator initiator)
        {
            if (id != initiator.Id)
            {
                return BadRequest();
            }

            await _initiatorRepository.UpdateAsync(initiator);
            
            try
            {
                await _initiatorRepository.SaveChangesAsync();
            }
            catch
            {
                throw;
            }

            return NoContent();
        }

        // DELETE: api/Initiators/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInitiator(int id)
        {
            var initiator = await _initiatorRepository.GetByIdAsync(id);
            if (initiator == null)
            {
                return NotFound();
            }

            await _initiatorRepository.DeleteAsync(initiator);
            await _initiatorRepository.SaveChangesAsync();

            return NoContent();
        }

        // GET: api/Initiators/active
        [HttpGet("active")]
        public async Task<ActionResult<IEnumerable<Initiator>>> GetActiveInitiators()
        {
            var initiators = await _initiatorRepository.GetActiveInitiatorsAsync();
            return Ok(initiators);
        }
    }
}