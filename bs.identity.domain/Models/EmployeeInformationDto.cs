namespace bs.identity.domain.Models
{
    public class EmployeeInformationDto
    {
        public string Id { get; set; }
        public string FullName { get; set; }
        public string Age { get; set; }
        public string EmailAddress { get; set; }
        public string EmailAddressVerified { get; set; }
        public string PhoneNumber { get; set; }
        public string PhoneNumberVerified { get; set; }
        public string Designation { get; set; }
    }
}
