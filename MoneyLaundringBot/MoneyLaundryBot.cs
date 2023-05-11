using Assignment_WebBank.BankAppData;
using Assignment_WebBank.Model;
using Assignment_WebBank.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MoneyLaundringBot
{
    public class MoneyLaundryBot
    {
        private readonly ITransactionService _transactionService;
        public MoneyLaundryBot( ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        public List<TransactionVM> TransaactionsByCountry { get; set; }
        
        public void WriteTxtFile(string transactionDetails)
        {
            string filePath = "Results.txt";
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                writer.WriteLine(transactionDetails);
            }
        }
    }
}
