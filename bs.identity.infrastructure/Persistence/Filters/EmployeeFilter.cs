namespace bs.identity.infrastructure.Persistence.Filters
{
    public class EmployeeFilter
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int? StoreId { get; set; }
        public string PhoneNumber { get; set; }
    }
}
