using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OutcommingPost.Domain;
using OutcommingPost.Domain.Interfaces;

namespace OutcommingPost.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReceiversController : ControllerBase
    {
        private readonly IReceiverRepository _receiverRepository;

        public ReceiversController(IReceiverRepository receiverRepository)
        {
            _receiverRepository = receiverRepository;
        }

        // GET: api/Receivers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Receiver>>> GetReceivers()
        {
            var receivers = await _receiverRepository.GetAllAsync();
            return Ok(receivers);
        }

        // GET: api/Receivers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Receiver>> GetReceiver(int id)
        {
            var receiver = await _receiverRepository.GetByIdAsync(id);

            if (receiver == null)
            {
                return NotFound();
            }

            return receiver;
        }

        // POST: api/Receivers
        [HttpPost]
        public async Task<ActionResult<Receiver>> PostReceiver(Receiver receiver)
        {
            await _receiverRepository.AddAsync(receiver);
            await _receiverRepository.SaveChangesAsync();

            return CreatedAtAction("GetReceiver", new { id = receiver.Id }, receiver);
        }

        // PUT: api/Receivers/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReceiver(int id, Receiver receiver)
        {
            if (id != receiver.Id)
            {
                return BadRequest();
            }

            await _receiverRepository.UpdateAsync(receiver);
            
            try
            {
                await _receiverRepository.SaveChangesAsync();
            }
            catch
            {
                throw;
            }

            return NoContent();
        }

        // DELETE: api/Receivers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReceiver(int id)
        {
            var receiver = await _receiverRepository.GetByIdAsync(id);
            if (receiver == null)
            {
                return NotFound();
            }

            await _receiverRepository.DeleteAsync(receiver);
            await _receiverRepository.SaveChangesAsync();

            return NoContent();
        }

        // GET: api/Receivers/active
        [HttpGet("active")]
        public async Task<ActionResult<IEnumerable<Receiver>>> GetActiveReceivers()
        {
            var receivers = await _receiverRepository.GetActiveReceiversAsync();
            return Ok(receivers);
        }
    }
}