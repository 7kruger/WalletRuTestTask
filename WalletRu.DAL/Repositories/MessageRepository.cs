using System.Text;
using Npgsql;
using WalletRu.Application.Interfaces;
using WalletRu.Domain.Entities;

namespace WalletRu.DAL.Repositories;

public class MessageRepository : IMessageRepository
{
    private readonly NpgsqlConnection _connection;

    public MessageRepository(NpgsqlConnection connection)
    {
        _connection = connection;
    }

    public async Task<IList<Message>> GetAllAsync(DateTime? startDateTime = default, DateTime? endDateTime = default)
    {
        var messages = new List<Message>();

        var queryBuilder = new StringBuilder(@"SELECT * FROM public.""messages"" WHERE 1=1");

        if (startDateTime.HasValue)
        {
            queryBuilder.Append(@" AND ""createdat"" >= @StartDateTime::timestamp");
        }
        if (endDateTime.HasValue)
        {
            queryBuilder.Append(@" AND ""createdat"" <= @EndDateTime::timestamp");
        }

        var query = queryBuilder.ToString();

        await using var command = new NpgsqlCommand(query, _connection);

        if (startDateTime.HasValue)
        {
            command.Parameters.AddWithValue("@StartDateTime", startDateTime.Value);
        }
        if (endDateTime.HasValue)
        {
            command.Parameters.AddWithValue("@EndDateTime", endDateTime.Value);
        }

        await using var reader = await command.ExecuteReaderAsync();

        if (reader.HasRows)
        {
            while (await reader.ReadAsync())
            {
                messages.Add(new Message
                {
                    Id = long.Parse(reader.GetValue(0).ToString()!),
                    SerialNumber = reader.GetValue(1).ToString()!,
                    Body = reader.GetValue(2).ToString()!,
                    Timestamp = reader.GetValue(3).ToString()!,
                    CreatedAt = DateTime.Parse(reader.GetValue(4).ToString()!)
                });
            }
        }

        return messages;
    }

    public async Task CreateAsync(Message message)
    {
        const string query = @"
            INSERT INTO Messages
            (SerialNumber, Body, Timestamp, CreatedAt)
            VALUES
            (@SerialNumber, @Body, @Timestamp, @CreatedAt)";

        await using var command = new NpgsqlCommand(query, _connection);
        command.Parameters.AddWithValue("SerialNumber", message.SerialNumber);
        command.Parameters.AddWithValue("Body", message.Body);
        command.Parameters.AddWithValue("Timestamp", message.Timestamp);
        command.Parameters.AddWithValue("CreatedAt", message.CreatedAt);

        await command.ExecuteNonQueryAsync();
    }
}