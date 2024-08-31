using WalletRu.Domain.Entities;

namespace WalletRu.Application.Interfaces;

public interface IMessageRepository
{
    public Task<IList<Message>> GetAllAsync(DateTime? startDateTime = default, DateTime? endDateTime = default);
    public Task CreateAsync(Message message);
}