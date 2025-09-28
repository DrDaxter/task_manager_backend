using AutoMapper;
using taskManagerBE.Dto;
using taskManagerBE.Models;

namespace taskManagerBE.Helpers;

public class ProjectProfile: Profile
{
    public ProjectProfile()
    {
        CreateMap<Project, ProjectDto>().ReverseMap();
        CreateMap<Project, CreateProjectDto>().ReverseMap();
        CreateMap<Project, UpdateProjectDto>().ReverseMap();
    }
}