using System;
using Microsoft.AspNetCore.Identity;

namespace bs.identity.domain.Entities
{
    public class Employee : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int StoreId { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
