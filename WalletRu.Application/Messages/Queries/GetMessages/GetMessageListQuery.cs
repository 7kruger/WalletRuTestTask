using MediatR;
using WalletRu.Application.Common.Dto;
using WalletRu.Application.Common.Result;

namespace WalletRu.Application.Messages.Queries.GetMessages;

public class GetMessageListQuery : IRequest<Result<IList<MessageDto>>>
{
    public DateTime? StartDateTime { get; set; }
    public DateTime? EndDateTime { get; set; }
}