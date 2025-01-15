using Snowflake.Data.Client;

namespace Assignment_WebBank.Auth0;

public class Auth
{
    public void SaveUserToSnowflake(string auth0Id, string email, string role)
    {
        string connectionString = "your_snowflake_connection_string";

        using (var conn = new SnowflakeDbConnection())
        {
            conn.ConnectionString = connectionString;
            conn.Open();

            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = @"INSERT INTO Users (UserId, Auth0Id, Email, Role) 
                                        VALUES (UUID_STRING(), ?, ?, ?)";

                // Add parameters explicitly
                cmd.Parameters.Add(new SnowflakeDbParameter { ParameterName = "1", Value = auth0Id });
                cmd.Parameters.Add(new SnowflakeDbParameter { ParameterName = "2", Value = email });
                cmd.Parameters.Add(new SnowflakeDbParameter { ParameterName = "3", Value = role });

                cmd.ExecuteNonQuery();
            }
        }
    }
}
