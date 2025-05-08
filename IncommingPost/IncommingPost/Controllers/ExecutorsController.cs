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
public class ExecutorsController : ControllerBase
{
    private readonly IExecutorRepository _executorRepository;

    public ExecutorsController(IExecutorRepository executorRepository)
    {
        _executorRepository = executorRepository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ExecutorDto>>> GetAllExecutors()
    {
        var executors = await _executorRepository.GetAllAsync();
        
        var executorDtos = executors.Select(e => new ExecutorDto
        {
            Id = e.Id,
            FullName = e.FullName,
            Position = e.Position,
            Department = e.Department,
            DueDate = e.DueDate,
            ExecutionStatus = e.ExecutionStatus
        }).ToList();

        return Ok(executorDtos);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ExecutorDto>> GetExecutor(int id)
    {
        var executor = await _executorRepository.GetExecutorWithAssignedDocumentsAsync(id);
        
        if (executor == null)
        {
            return NotFound();
        }

        var executorDto = new ExecutorDto
        {
            Id = executor.Id,
            FullName = executor.FullName,
            Position = executor.Position,
            Department = executor.Department,
            DueDate = executor.DueDate,
            ExecutionStatus = executor.ExecutionStatus
        };

        return executorDto;
    }

    [HttpGet("by-department/{department}")]
    public async Task<ActionResult<IEnumerable<ExecutorDto>>> GetExecutorsByDepartment(string department)
    {
        var executors = await _executorRepository.GetExecutorsByDepartmentAsync(department);
        
        var executorDtos = executors.Select(e => new ExecutorDto
        {
            Id = e.Id,
            FullName = e.FullName,
            Position = e.Position,
            Department = e.Department,
            DueDate = e.DueDate,
            ExecutionStatus = e.ExecutionStatus
        }).ToList();

        return Ok(executorDtos);
    }

    [HttpGet("search/{nameFragment}")]
    public async Task<ActionResult<IEnumerable<ExecutorDto>>> SearchExecutorsByName(string nameFragment)
    {
        var executors = await _executorRepository.SearchExecutorsByNameAsync(nameFragment);
        
        var executorDtos = executors.Select(e => new ExecutorDto
        {
            Id = e.Id,
            FullName = e.FullName,
            Position = e.Position,
            Department = e.Department,
            DueDate = e.DueDate,
            ExecutionStatus = e.ExecutionStatus
        }).ToList();

        return Ok(executorDtos);
    }

    [HttpPost]
    public async Task<ActionResult<ExecutorDto>> CreateExecutor(CreateExecutorDto createExecutorDto)
    {
        var executor = new Executor
        {
            FullName = createExecutorDto.FullName,
            Position = createExecutorDto.Position,
            Department = createExecutorDto.Department,
            ExecutionStatus = "Available"
        };

        await _executorRepository.AddAsync(executor);
        await _executorRepository.SaveChangesAsync();

        return CreatedAtAction(nameof(GetExecutor), new { id = executor.Id }, new ExecutorDto
        {
            Id = executor.Id,
            FullName = executor.FullName,
            Position = executor.Position,
            Department = executor.Department,
            DueDate = executor.DueDate,
            ExecutionStatus = executor.ExecutionStatus
        });
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateExecutor(int id, UpdateExecutorDto updateExecutorDto)
    {
        var executor = await _executorRepository.GetByIdAsync(id);
        
        if (executor == null)
        {
            return NotFound();
        }

        executor.Position = updateExecutorDto.Position;
        executor.Department = updateExecutorDto.Department;
        
        if (updateExecutorDto.DueDate.HasValue)
        {
            executor.DueDate = updateExecutorDto.DueDate.Value;
        }
        
        if (!string.IsNullOrEmpty(updateExecutorDto.ExecutionStatus))
        {
            executor.ExecutionStatus = updateExecutorDto.ExecutionStatus;
        }

        await _executorRepository.UpdateAsync(executor);
        await _executorRepository.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteExecutor(int id)
    {
        var executor = await _executorRepository.GetByIdAsync(id);
        
        if (executor == null)
        {
            return NotFound();
        }

        await _executorRepository.DeleteAsync(executor);
        await _executorRepository.SaveChangesAsync();

        return NoContent();
    }
}