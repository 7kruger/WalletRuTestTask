using WalletRu.Application.Interfaces;

namespace WalletRu.Application.Common.Result;

public class Result : IResult
{
    internal Result()
    {
        Errors = [];
    }

    private Result(bool success, IList<string> errors)
    {
        IsSuccess = success;
        Errors = errors;
    }

    public string ErrorMessage => string.Join(", ", Errors);
    public IList<string> Errors { get; init; }
    public bool IsSuccess { get; init; }

    public static Result Success()
    {
        return new Result(true, []);
    }

    public static Result Failure(IList<string> errors)
    {
        return new Result(false, errors);
    }
}

public class Result<T> : Result, IResult<T>
{
    public T? Data { get; init; }

    public static Result<T> Success(T data)
    {
        return new Result<T> { IsSuccess = true, Data = data };
    }

    public new static Result<T> Failure(IList<string> errors)
    {
        return new Result<T> { IsSuccess = false, Errors = errors };
    }
}