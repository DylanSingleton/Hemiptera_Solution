namespace Hemiptera_API.Results;

public class SuccessResult : Result
{
    public SuccessResult()
    {
        IsSuccessful = true;
    }
}

public class SuccessResult<T> : Result<T>
{
    // find a way to fix isSuccesful and isUnsuccesful in Result class
    public SuccessResult(T payload) : base(payload)
    {
    }
}