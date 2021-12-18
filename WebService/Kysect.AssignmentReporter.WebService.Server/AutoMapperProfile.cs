using System.Collections.Generic;
using AutoMapper;
using Kysect.AssignmentReporter.WebService.DAL.Entities;
using Kysect.AssignmentReporter.WebService.Shared;

namespace Kysect.AssignmentReporter.WebService.Server;

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