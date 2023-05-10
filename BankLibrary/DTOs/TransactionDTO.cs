using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankLibrary.DTOs
{
    public class TransactionDTO
    {
        public int TransactionId { get; set; }
        public DateTime Date { get; set; }
        public string Operation { get; set; } = null!;
        public decimal Amount { get; set; }
        public decimal Balance { get; set; }

    }
}
