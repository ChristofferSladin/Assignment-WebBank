namespace Assignment_WebBank.Model
{
    public class CustomerVM
    {
        public int CustomerId { get; set; }
        public string? PersonalNr { get; set; }
        public int AccountNr { get; set; }
        public decimal Balance { get; set; }
        public string? Country { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Adress { get; set; }
        public string? City { get; set; }
        public string? Gender { get; set; }
        public DateTime? BirthDay { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
    }
}
