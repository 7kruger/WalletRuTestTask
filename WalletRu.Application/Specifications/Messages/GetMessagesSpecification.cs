using Ardalis.Specification;
using WalletRu.Domain.Entities;

namespace WalletRu.Application.Specifications.Messages;

public sealed class GetMessagesSpecification : Specification<Message>
{
    public GetMessagesSpecification(DateTime? startDateTime = default, DateTime? endDateTime = default)
    {
        if (startDateTime.HasValue)
        {
            Query.Where(message => message.CreatedAt >= startDateTime);
        }
        if (endDateTime.HasValue)
        {
            Query.Where(message => message.CreatedAt <= endDateTime);
        }
    }
}