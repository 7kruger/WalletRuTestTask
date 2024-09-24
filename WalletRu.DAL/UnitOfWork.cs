using WalletRu.Application.Interfaces;
using WalletRu.DAL.Data;
using WalletRu.DAL.Repositories;
using WalletRu.Domain.Entities;

namespace WalletRu.DAL;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _dbContext;
    private IRepository<Message>? _messageRepository;

    public UnitOfWork(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IRepository<Message> MessageRepository => _messageRepository ??= new EfRepository<Message>(_dbContext);
}