using AutoMapper;
using bs.identity.application.Commands.EmployeeRegistration;
using bs.identity.domain.Entities;
using bs.identity.domain.Models;
using System;
using System.Text.RegularExpressions;

namespace bs.identity.application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Employee, EmployeeInformationDto>()
                .ForMember(d => d.FullName,
                    opt => opt.MapFrom(s => $"{s.FirstName} {s.LastName}"))
                .ForMember(d => d.Age,
                    opt => opt.MapFrom(s => DateTime.Now.Year - s.DateOfBirth.Year))
                .ForMember(d => d.EmailAddress,
                    opt => opt.MapFrom(s => s.Email))
                .ForMember(d => d.EmailAddressVerified,
                    opt => opt.MapFrom(s => (s.EmailConfirmed) ? "Yes" : "No"))
                .ForMember(d => d.PhoneNumber,
                    opt => opt.MapFrom(s => FormatPhoneNumber(s.PhoneNumber)))
                .ForMember(d => d.PhoneNumberVerified,
                    opt => opt.MapFrom(s => (s.PhoneNumberConfirmed) ? "Yes" : "No"))
                .ForMember(d => d.Designation,
                    opt => opt.MapFrom(s => s.Designation.ToString()));


            CreateMap<Employee, EmployeeRegistrationCommand>()
                .ForMember(d => d.EmailAddress, opt => opt.MapFrom(s => s.Email))
                .ForMember(d => d.EmailAddress, opt => opt.MapFrom(s => s.UserName))
                .ForMember(d => d.PhoneNumber, opt => opt.MapFrom(s => s.PhoneNumber))
                .ReverseMap();

            CreateMap<EmployeeRegisterRequestDto, EmployeeRegistrationCommand>();
        }

        private string FormatPhoneNumber(string phoneNum)
        {
            var regexObj = new Regex(@"[^\d]");

            phoneNum = regexObj.Replace(phoneNum, "");

            return Convert.ToInt64(phoneNum).ToString("(###) ###-####");
        }
    }
}
