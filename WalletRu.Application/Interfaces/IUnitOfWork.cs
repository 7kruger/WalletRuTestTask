namespace WalletRu.Application.Interfaces;

public interface IUnitOfWork
{
    public IMessageRepository MessageRepository { get; }
}