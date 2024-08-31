using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using WalletRu.Api.Hubs;
using WalletRu.Api.Models;
using WalletRu.Application.Common.Dto;
using WalletRu.Application.Common.Result;
using WalletRu.Application.Messages.Commands.CreateMessage;
using WalletRu.Application.Messages.Queries.GetMessages;

namespace WalletRu.Api.Controllers;

public class MessagesController : BaseController
{
    private readonly IHubContext<MessageHub> _hubContext;

    public MessagesController(IHubContext<MessageHub> hubContext)
    {
        _hubContext = hubContext;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Result<IList<MessageDto>>>> Get(DateTime? startDateTime, DateTime? endDateTime)
    {
        var query = new GetMessageListQuery
        {
            StartDateTime = startDateTime,
            EndDateTime = endDateTime
        };
        var result = await Mediator.Send(query);
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Result>> Add([FromBody] CreateMessageDto createMessageDto)
    {
        var command = createMessageDto.Adapt<CreateMessageCommand>();
        var result = await Mediator.Send(command);

        if (!result.IsSuccess)
        {
            return BadRequest(result);
        }

        await _hubContext.Clients.All.SendAsync("Receive", result.Data);

        return Ok(result);
    }
}