namespace Hemiptera_API.Results;

public class ErrorResult : Result
{
    public string? Message { get; set; }
    public IReadOnlyCollection<Error>? Errors { get; set; }

    public ErrorResult()
    {
    }

    public ErrorResult(string message)
    {
        IsSuccessful = false;
        Message = message;
        Errors = Array.Empty<Error>();
    }

    public ErrorResult(string message, IReadOnlyCollection<Error> errors)
    {
        IsSuccessful = false;
        Message = message;
        Errors = errors ?? Array.Empty<Error>();
    }
}

public class ErrorResult<T> : Result<T>
{
    public string? Message { get; }
    public IReadOnlyCollection<Error>? Errors { get; }

    public ErrorResult() : base(default)
    {
    }

    public ErrorResult(string message) : base(default)
    {
        IsSuccessful = false;
        Message = message;
        Errors = Array.Empty<Error>();
    }

    public ErrorResult(string message, IReadOnlyCollection<Error> errors) : base(default)
    {
        IsSuccessful = false;
        Message = message;
        Errors = errors ?? Array.Empty<Error>();
    }
}