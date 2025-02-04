using AutoMapper;
using AIResumePicker.Models;
using AIResumePicker.DTOs;

public class MappingConfig : Profile
{
    public MappingConfig()
    {
        CreateMap<User, UserDTO>().ReverseMap();
        CreateMap<Employee, EmployeeDTO>().ReverseMap();
        CreateMap<Job, JobDTO>().ReverseMap();
        CreateMap<JobCriteria, JobCriteriaDTO>().ReverseMap();
        CreateMap<Resume, ResumeDTO>().ReverseMap();
    }
}
