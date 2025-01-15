using Assignment_WebBank.SnowFlake;
using BankLibrary.DTOs;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;

namespace Assignment_WebBank.Pages;

public class IndexModel : PageModel
{
    private readonly BankLibrary.RandomAPIservice _randomAPIservice;
    private readonly SnowflakeService _snowflakeService;

    public IndexModel(BankLibrary.RandomAPIservice randomAPIservice, SnowflakeService snowflakeService)
    {
        _randomAPIservice = randomAPIservice;
        _snowflakeService = snowflakeService;
    }

    public List<RandomApiDTO.User> Users { get; set; }
    public DataTable Transactions { get; set; }

    public async Task OnGet()
    {
        Users = await _randomAPIservice.GetUsersAsync();
        Transactions = _snowflakeService.GetTransactions();
    }
}
