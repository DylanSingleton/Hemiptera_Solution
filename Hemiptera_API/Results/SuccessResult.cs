namespace Hemiptera_API.Results
{
    public class SuccessResult : Result
    {
        public SuccessResult() 
        {
            IsSuccessful = true;
        }
    }

    public class SuccessResult<T> : Result<T>
    {
        public SuccessResult(T payload) : base(payload)
        {
            IsSuccessful = true;
        }
    }
}
