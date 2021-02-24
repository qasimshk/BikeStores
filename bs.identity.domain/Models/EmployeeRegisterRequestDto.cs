using System;

namespace bs.identity.domain.Models
{
    public class EmployeeRegisterRequestDto 
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int StoreId { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public int Designation { get; set; }
        public string PhoneNumber { get; set; }
    }
}
