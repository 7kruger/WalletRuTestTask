namespace WalletRu.Application.Interfaces;

public interface IResult
{
    IList<string> Errors { get; init; }

    bool IsSuccess { get; init; }
}

public interface IResult<out T> : IResult
{
    T? Data { get; }
}