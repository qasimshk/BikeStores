using AutoMapper;
using bs.identity.domain.Entities;
using bs.identity.domain.Models;

namespace bs.identity.application.Commands.EmployeeRegistration
{
    public class EmployeeRegistrationCommandMapper : Profile
    {
        public EmployeeRegistrationCommandMapper()
        {
            CreateMap<Employee, EmployeeRegistrationCommand>()
                .ForMember(d => d.EmailAddress, opt => opt.MapFrom(s => s.Email))
                .ForMember(d => d.EmailAddress, opt => opt.MapFrom(s => s.UserName))
                .ForMember(d => d.PhoneNumber, opt => opt.MapFrom(s => s.PhoneNumber))
                .ReverseMap();

            CreateMap<EmployeeRegisterRequestDto, EmployeeRegistrationCommand>();
        }
    }
}
