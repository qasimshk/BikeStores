using System;
using bs.identity.domain.Models;
using Microsoft.AspNetCore.Identity;

namespace bs.identity.domain.Entities
{
    public class Employee : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int StoreId { get; set; }
        public DateTime DateOfBirth { get; set; }
        public EmployeeRoles Designation { get; set; }

        public string FullName()
        {
            return $"{FirstName} {LastName}";
        }
    }
}
