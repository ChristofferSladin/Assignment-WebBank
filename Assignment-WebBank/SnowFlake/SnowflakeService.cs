using Snowflake.Data.Client;
using System.Data;

namespace Assignment_WebBank.SnowFlake;

public class SnowflakeService
{
    private readonly IConfiguration _configuration;

    public SnowflakeService(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public DataTable GetTransactions()
    {
        try
        {
            using (var conn = new SnowflakeDbConnection())
            {
                conn.ConnectionString = _configuration.GetConnectionString("SnowflakeConnection");
                conn.Open();

                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM BankAppData.Public.Transactions LIMIT 10";
                    using (var reader = cmd.ExecuteReader())
                    {
                        DataTable dataTable = new DataTable();
                        dataTable.Load(reader);
                        return dataTable;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            throw;
        }
    }
}