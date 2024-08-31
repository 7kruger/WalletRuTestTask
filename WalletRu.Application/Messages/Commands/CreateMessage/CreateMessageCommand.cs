using MediatR;
using WalletRu.Application.Common.Dto;
using WalletRu.Application.Common.Result;

namespace WalletRu.Application.Messages.Commands.CreateMessage;

public class CreateMessageCommand : IRequest<Result<MessageDto>>
{
    public string SerialNumber { get; set; }
    public string Body { get; set; }
}