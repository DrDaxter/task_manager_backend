using AutoMapper;
using taskManagerBE.Dto;
using Task = taskManagerBE.Models.Task;

namespace taskManagerBE.Helpers;

public class TaskProfile: Profile
{
    public TaskProfile()
    {
        CreateMap<Task, TaskDto>().ReverseMap();
        CreateMap<Task, CreateTaskDto>().ReverseMap();
    }
}