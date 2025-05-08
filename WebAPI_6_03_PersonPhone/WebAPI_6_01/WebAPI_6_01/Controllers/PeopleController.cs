using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI_6_01.Domain;
using WebAPI_6_01.Infrastructure;
using WebAPI01.API.Dto;

namespace WebAPI01.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        private readonly Context _context;
        private readonly PersonRepository _personRepository;
        public PeopleController(Context context)
        {
            _context = context;
            _personRepository = new PersonRepository(_context);
        }

        // GET: api/People
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PersonDto>>> GetPersons()
        {
            var persons = await _personRepository.GetAllAsync();
            return PersonDtoMapper.ToDto(persons);
        }

        // GET: api/People/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PersonDto>> GetPerson(Guid id)
        {
            var person = await _personRepository.GetByIdAsync(id);
            if (person == null)
            {
                return NotFound();
            }
            var personDto = PersonDtoMapper.ToDto(person);
            return personDto;
        }

        // PUT: api/People/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPerson(Guid id, PersonDto personDto)
        {
            if (id != personDto.Id)
            {
                return BadRequest();
            }

            var person = PersonDtoMapper.ToEntity(personDto);

            await _personRepository.UpdateAsync(person);

            return NoContent();
        }

        // POST: api/People
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PersonDto>> PostPerson(PersonDto personDto)
        {
            var person = PersonDtoMapper.ToEntity(personDto);
            var person2 = await _personRepository.AddAsync(person);
            var personDto2 = PersonDtoMapper.ToDto(person2);
            return CreatedAtAction("GetPerson", new { id = personDto2.Id }, personDto2);
        }

        // DELETE: api/People/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePerson(Guid id)
        {
            var person = await _personRepository.GetByIdAsync(id);
            if (person == null)
            {
                return NotFound();
            }

            await _personRepository.DeleteAsync(id);

            return NoContent();
        }

        private bool PersonExists(Guid id)
        {
            return _context.Persons.Any(e => e.Id == id);
        }
    }
}
