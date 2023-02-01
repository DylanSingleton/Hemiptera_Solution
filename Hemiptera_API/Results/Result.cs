namespace Hemiptera_API.Results;

public abstract class Result
{
    public bool IsSuccessful { get; protected set; }
    public bool IsUnsuccessful { get; protected set; }

    public Result(bool isSuccessful)
    {
        IsSuccessful = isSuccessful;
    }

    public Result()
    {
        IsSuccessful = !IsUnsuccessful;
    }
}

public abstract class Result<T> : Result
{
    private T? _payload;

    protected Result(T payload)
    {
        _payload = payload;
        IsSuccessful = true;
        IsUnsuccessful = !IsSuccessful;
    }

    public T Payload
    {
        get
        {
            if (IsSuccessful)
            {
                return _payload;
            }
            else
            {
                throw new AccessViolationException
                    ($"Access to {nameof(_payload)} when {nameof(IsSuccessful)} is false is prohibited.");
            }
        }

        set
        {
            _payload = value;
        }
    }
}