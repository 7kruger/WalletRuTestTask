using WalletRu.Domain.Entities;

namespace WalletRu.Application.Interfaces;

public interface IUnitOfWork
{
    public IRepository<Message> MessageRepository { get; }
}