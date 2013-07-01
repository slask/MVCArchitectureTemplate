namespace Application
{
    public class OperationResult : IOperationResult
    {
        public OperationResult(bool success)
        {
            Success = success;
        }
        public bool Success { get; protected set; }
    }
}