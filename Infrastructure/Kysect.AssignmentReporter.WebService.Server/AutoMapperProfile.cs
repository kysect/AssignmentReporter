using AutoMapper;
using Kysect.AssignmentReporter.Domain;
using Kysect.AssignmentReporter.Dto;

namespace Kysect.AssignmentReporter.Web.Service;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Group, MinimalGroupDto>();
        CreateMap<Subject, SubjectDto>();
        CreateMap<SubjectGroup, MinimalSubjectGroupDto>();
        CreateMap<SubjectGroup, SubjectGroupDto>()
            .ForMember(
                dest => dest.Students,
                opt => opt.MapFrom(src => src.Students));
        CreateMap<Teacher, TeacherDto>()
            .ForMember(
                dest => dest.SubjectGroups,
                opt => opt.MapFrom(src => src.SubjectGroups));
        CreateMap<Teacher, MinimalTeacherDto>();
        CreateMap<Report, ReportDto>();
        CreateMap<Student, StudentDto>();
        CreateMap<Group, GroupDto>()
            .ForMember(
                dest => dest.Students,
                opt => opt.MapFrom(src => src.Students));
    }
}