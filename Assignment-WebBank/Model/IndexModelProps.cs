namespace Assignment_WebBank.Model
{
    public class IndexModelProps
    {
        public int Id { get; set; }
        public decimal Balance { get; set; }
        public string? Country { get; set; }
        public string? City { get; set; }
        public int CustomerCount { get; set; }
        public int AccountCount { get; set; }
        public decimal TotalBalance { get; set; }
       
    }
}
