using Npgsql;
using WalletRu.Application.Interfaces;
using WalletRu.DAL.Repositories;

namespace WalletRu.DAL;

public class UnitOfWork : IUnitOfWork
{
    private readonly NpgsqlConnection _connection;
    private IMessageRepository? _messageRepository;

    public UnitOfWork(NpgsqlConnection connection)
    {
        _connection = connection;
    }

    public IMessageRepository MessageRepository => _messageRepository ??= new MessageRepository(_connection);
}