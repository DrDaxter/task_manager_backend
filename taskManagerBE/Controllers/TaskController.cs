using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using taskManagerBE.Dto;
using taskManagerBE.Interfaces;
using Task = taskManagerBE.Models.Task;

namespace taskManagerBE.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TaskController: ControllerBase
{
    private readonly ITaskRepository _taskRepository;
    private readonly IMapper _mapper;

    public TaskController(ITaskRepository taskRepository, IMapper mapper)
    {
        _taskRepository = taskRepository;
        _mapper = mapper;
    }

    [HttpGet("GetTasksByProjectId/{projectId:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetTasksByProjectId(int projectId)
    {
        try
        {
            if (projectId <= 0) return BadRequest();

            var tasks = await _taskRepository.GetTasksByProjectId(projectId);

            if (tasks.Count <= 0) return NotFound();

            var tasksDto = _mapper.Map<IEnumerable<TaskDto>>(tasks);

            return Ok(tasksDto);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpPost(Name = "CreateTask")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult CreateTask([FromBody] CreateTaskDto createTaskDto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        
        if(createTaskDto == null) return BadRequest();
        
        var task = _mapper.Map<Task>(createTaskDto);

        if (!_taskRepository.AddTask(task))
        {
            ModelState.AddModelError("error", "Error while adding new task");
            return BadRequest(ModelState);
        }
        
        return CreatedAtRoute("CreateTask", new { id = task.Id }, task);
    }
}