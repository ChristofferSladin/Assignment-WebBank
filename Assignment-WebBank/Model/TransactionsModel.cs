﻿namespace Assignment_WebBank.Model
{
    public class TransactionsModel
    {
        public int TransactionId { get; set; }
        public int AccountId { get; set; }
        public string? Date { get; set; }
        public string? Operation { get; set; }
        public decimal Amount { get; set; }
        public decimal Balance { get; set; }
        public string? Account { get; set; }
        public string? Bank { get; set; }
    }
}