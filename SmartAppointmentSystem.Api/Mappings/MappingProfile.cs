using AutoMapper;
using SmartAppointmentSystem.Api.Models;
using SmartAppointmentSystem.Business.DTOs;
using SmartAppointmentSystem.Data.Entities;

namespace SmartAppointmentSystem.Api.Mappings;
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<DoctorUserRequestModel, DoctorUserRequestDTO>()
            .ForAllMembers(opt => opt.Condition((src, dest, srcMember) =>
                srcMember != null && !string.IsNullOrWhiteSpace(srcMember?.ToString())
            ));
        CreateMap<DoctorUserRequestDTO, Doctor>()
            .ForAllMembers(opt => opt.Condition((src, dest, srcMember) =>
                srcMember != null && !string.IsNullOrWhiteSpace(srcMember?.ToString())
            ));
    }
}