using System;

namespace bs.identity.domain.Models
{
    public class EmployeeUpdateRequestDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
    }
}
