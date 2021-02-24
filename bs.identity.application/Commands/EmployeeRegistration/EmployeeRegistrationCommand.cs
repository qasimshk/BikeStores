using bs.identity.domain.Models;
using MediatR;
using System;

namespace bs.identity.application.Commands.EmployeeRegistration
{
    public class EmployeeRegistrationCommand : IRequest
    {
        protected EmployeeRegistrationCommand() { }

        public EmployeeRegistrationCommand(EmployeeRegisterRequestDto request)
        {
            FirstName = request.FirstName.Trim();
            LastName = request.LastName.Trim();
            StoreId = request.StoreId;
            DateOfBirth = request.DateOfBirth;
            EmailAddress = request.EmailAddress.Trim();
            Password = request.Password.Trim();
            Designation = request.Designation;
            PhoneNumber = request.PhoneNumber;
        }

        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public int StoreId { get; private set; }
        public DateTime DateOfBirth { get; private set; }
        public string EmailAddress { get; private set; }
        public string Password { get; private set; }
        public int Designation { get; private set; }
        public string PhoneNumber { get; private set; }
    }
}
