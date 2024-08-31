using FluentValidation;

namespace WalletRu.Application.Messages.Commands.CreateMessage;

public class CreateMessageCommandValidator : AbstractValidator<CreateMessageCommand>
{
    public CreateMessageCommandValidator()
    {
        RuleFor(x => x.Body).NotEmpty().MaximumLength(128);
        RuleFor(x => x.SerialNumber).NotEmpty().Length(64);
    }
}