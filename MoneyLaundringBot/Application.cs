using Assignment_WebBank.Services;
using System.Data.SqlTypes;

public class Application
{
    private readonly ITransactionService _transactionService;

    public Application(ITransactionService transactionService)
    {
        _transactionService = transactionService;
    }

    public void Run()
    {
       _transactionService.CheckForMoneyLaundering();
    }
}