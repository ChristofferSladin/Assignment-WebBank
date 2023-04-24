using Assignment_WebBank.BankAppData;
using Assignment_WebBank.Services;

var dbContext = new BankAppDataContext();
var transactionService = new TransactionService(dbContext);
var app = new Application(transactionService);
app.Run();
