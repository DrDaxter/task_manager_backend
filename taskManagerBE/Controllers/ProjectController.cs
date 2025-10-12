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
            var projectsDto = new List<ProjectDto>();
            foreach (var project in projects)
            {
                projectsDto.Add(_mapper.Map<ProjectDto>(project));
            }
            return Ok(projectsDto);
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
        
        var projectDto = _mapper.Map<ProjectDto>(project);
        
        return Ok(projectDto);
    }

    [HttpPost(Name = "CreateProject")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult CreateProject([FromBody] CreateProjectDto project)
    {
        if(!ModelState.IsValid)
            return BadRequest(ModelState);
        
        var newProject = _mapper.Map<Project>(project);
        if (!_projectRepository.AddProject(newProject))
        {
            ModelState.AddModelError("", "Error creating project");
            return BadRequest(ModelState);
        }
        
        return CreatedAtRoute("CreateProject", new { projectId = newProject.Id }, newProject);
    }

    [HttpPatch("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult UpdateProject(int id, [FromBody] UpdateProjectDto project)
    {
        if(!ModelState.IsValid)
            return BadRequest(ModelState);
        
        var projectToUpdate = _mapper.Map<Project>(project);
        projectToUpdate.Id = id;

        if (!_projectRepository.UpdateProject(projectToUpdate))
        {
            ModelState.AddModelError("", "Error updating project");
            return BadRequest(ModelState);
        }
        
        return NoContent();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public IActionResult DeleteProject(int id)
    {
        try
        {
            var projectToDelete = _projectRepository.GetProjectById(id);
            
            if(projectToDelete == null)
                return NotFound();
            
            if (!_projectRepository.DeleteProject(projectToDelete))
            {
                ModelState.AddModelError("", "Error deleting project");
                return BadRequest(ModelState);
            }
            return NoContent();
        }
        catch (Exception e)
        {
          return StatusCode(500, e.Message);
        }
    }
}