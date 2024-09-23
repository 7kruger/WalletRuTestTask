using Mapster;
using MediatR;
using WalletRu.Application.Common.Dto;
using WalletRu.Application.Common.Result;
using WalletRu.Application.Interfaces;
using WalletRu.Application.Specifications.Messages;

namespace WalletRu.Application.Messages.Queries.GetMessages;

public class GetMessageListQueryHandler : IRequestHandler<GetMessageListQuery, Result<IList<MessageDto>>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetMessageListQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<IList<MessageDto>>> Handle(GetMessageListQuery request, CancellationToken cancellationToken)
    {
        var spec = new GetMessagesSpecification(request.StartDateTime, request.EndDateTime);
        var messages = await _unitOfWork.MessageRepository.ListAsync(spec, cancellationToken);
        
        return Result<IList<MessageDto>>.Success(messages.Adapt<IList<MessageDto>>());
    }
}