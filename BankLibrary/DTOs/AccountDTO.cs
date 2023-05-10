using Assignment_WebBank.BankAppData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankLibrary.DTOs
{
    public class AccountDTO
    {
        public int AccountId { get; set; }
        public decimal Balance { get; set; }
        public DateTime CreatedDate { get; set; }
        public List<TransactionDTO> Transactions { get; set; }
    }
}
