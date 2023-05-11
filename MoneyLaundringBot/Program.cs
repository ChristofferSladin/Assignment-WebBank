using Assignment_WebBank.BankAppData;
using Assignment_WebBank.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

class Program
{
    static async Task Main(string[] args)
    {
        ServiceCollection services = new ServiceCollection();

        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", false)
            .Build();

        ConfigureServices(services, configuration);

        ServiceProvider serviceProvider = services.BuildServiceProvider();

        var moneyLaundryService = serviceProvider.GetService<ITransactionService>();
        await moneyLaundryService.CheckForSuspiciousActivity();
    }

    private static void ConfigureServices(ServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<BankAppDataContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<ITransactionService, TransactionService>();
    }
}

