using System;
using System.Text.RegularExpressions;
using AutoMapper;
using bs.identity.domain.Entities;
using bs.identity.domain.Models;

namespace bs.identity.infrastructure.Persistence.Queries.GetEmployeeInformation
{
    public class GetEmployeeInformationQueryMapper : Profile
    {
        public GetEmployeeInformationQueryMapper()
        {
            CreateMap<Employee, EmployeeInformationDto>()
                .ForMember(d => d.FullName, 
                    opt => opt.MapFrom(s => $"{s.FirstName} {s.LastName}"))
                .ForMember(d => d.Age,
                    opt => opt.MapFrom(s => DateTime.Now.Year - s.DateOfBirth.Year))
                .ForMember(d => d.EmailAddress,
                    opt => opt.MapFrom(s => s.Email))
                .ForMember(d => d.PhoneNumber,
                    opt => opt.MapFrom(s => FormatPhoneNumber(s.PhoneNumber)))
                .ForMember(d => d.Designation,
                    opt => opt.MapFrom(s => s.Designation.ToString()));
        }

        private string FormatPhoneNumber(string phoneNum)
        {
            var regexObj = new Regex(@"[^\d]");

            phoneNum = regexObj.Replace(phoneNum, "");
            
            return Convert.ToInt64(phoneNum).ToString("(###) ###-####");
        }
    }
}
