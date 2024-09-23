using Mapster;
using MediatR;
using WalletRu.Application.Common.Dto;
using WalletRu.Application.Common.Result;
using WalletRu.Application.Interfaces;
using WalletRu.Application.Specifications.Messages;
using WalletRu.Domain.Entities;

namespace WalletRu.Application.Messages.Commands.CreateMessage;

public class CreateMessageCommandHandler : IRequestHandler<CreateMessageCommand, Result<MessageDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateMessageCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<MessageDto>> Handle(CreateMessageCommand request, CancellationToken cancellationToken)
    {
        var message = new Message
        {
            SerialNumber = request.SerialNumber,
            Body = request.Body,
            Timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds().ToString(),
            CreatedAt = DateTime.UtcNow
        };

        await _unitOfWork.MessageRepository.AddAsync(message, cancellationToken);
        
        return Result<MessageDto>.Success(message.Adapt<MessageDto>());
    }
}