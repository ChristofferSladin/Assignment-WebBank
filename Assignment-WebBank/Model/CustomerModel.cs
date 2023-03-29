﻿namespace Assignment_WebBank.Model
{
    public class CustomerModel
    {
        public int Id { get; set; }
        public int PersonalNr { get; set; }
        public int AccountNr { get; set; }
        public int TotalBalance { get; set; }
        public decimal Balance { get; set; }
        public string? Country { get; set; }
        public string? Name { get; set; }
        public string? Adress { get; set; }
        public string? City { get; set; }
    }
}