using AutoMapper;
using taskManagerBE.Dto;
using taskManagerBE.Models;

namespace taskManagerBE.Helpers;

public class MappingProfile: Profile
{
    public MappingProfile()
    {
        CreateMap<Project, ProjectDto>();
        CreateMap<ProjectDto, Project>();
    }
}