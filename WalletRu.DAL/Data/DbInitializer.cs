using Microsoft.Extensions.Options;
using Npgsql;
using WalletRu.Application.Common.Options;

namespace WalletRu.DAL.Data;

public class DbInitializer
{
    private readonly string _connectionString;
    
    public DbInitializer(IOptions<DbOptions> dbOptions)
    {
        _connectionString = dbOptions.Value.ConnectionString;
    }

    public async Task EnsureCreatedAsync()
    {
        await CreateDatabaseIfNotExistsAsync(_connectionString);
        await CreateTablesAsync(_connectionString);
    }

    private async Task CreateDatabaseIfNotExistsAsync(string connectionString)
    {
        var builder = new NpgsqlConnectionStringBuilder(_connectionString);
        var dbName = builder.Database;

        builder.Database = "postgres";
        var masterConnectionString = builder.ConnectionString;

        var masterDbConnection = new NpgsqlConnection(masterConnectionString);
        await masterDbConnection.OpenAsync();

        var query = $"SELECT 1 FROM pg_database WHERE datname = '{dbName}'";
        await using var command = new NpgsqlCommand(query, masterDbConnection);
        var result = await command.ExecuteScalarAsync();

        if (result == null)
        {
            command.CommandText = $"CREATE DATABASE \"{dbName}\"";
            await command.ExecuteNonQueryAsync();
        }

        await masterDbConnection.CloseAsync();
    }

    private async Task CreateTablesAsync(string connectionString)
    {
        var connection = new NpgsqlConnection(connectionString);
        await connection.OpenAsync();

        var query = @"
            CREATE TABLE IF NOT EXISTS Messages
            (
                Id BIGSERIAL PRIMARY KEY,
                SerialNumber TEXT,
                Body varchar(128),
                Timestamp text,
                CreatedAt timestamp with time zone
            )
        ";

        await using var command = new NpgsqlCommand(query, connection);
        await command.ExecuteNonQueryAsync();
        await connection.CloseAsync();
    }
}