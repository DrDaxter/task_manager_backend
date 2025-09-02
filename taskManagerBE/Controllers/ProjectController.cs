using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using taskManagerBE.Dto;
using taskManagerBE.Interfaces;
using taskManagerBE.Models;

namespace taskManagerBE.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProjectController: ControllerBase
{
    private readonly IProjectRepository _projectRepository;
    private readonly IMapper _mapper;
    
    public ProjectController(IProjectRepository projectRepository, IMapper mapper)
    {
        _projectRepository = projectRepository;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult GetAllProjects()
    {
        try
        {
            var projects = _projectRepository.GetAllProjects();
            return Ok(projects);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpGet("{projectId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult GetProjectById(int projectId)
    {
        if(!ModelState.IsValid)
            return BadRequest(ModelState);
        
        var project = _projectRepository.GetProjectById(projectId);
        
        if(project == null)
            return NotFound();
        
        return Ok(project);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult CreateProject([FromBody] ProjectDto project)
    {
        if(!ModelState.IsValid)
            return BadRequest(ModelState);
        
        var newProject = _mapper.Map<Project>(project);
        if (!_projectRepository.AddProject(newProject))
        {
            ModelState.AddModelError("", "Error creating project");
            return BadRequest(ModelState);
        }
        
        return Ok("Project created");
    }
}