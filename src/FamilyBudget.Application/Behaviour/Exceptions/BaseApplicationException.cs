namespace FamilyBudget.Application.Behaviour.Exceptions;
public abstract class BaseApplicationException : Exception
{
    protected BaseApplicationException(string message, string errorCode) : base(message)
    {
        ErrorCode = errorCode;
    }

    protected BaseApplicationException(string message, string errorCode, Exception innerException) : base(message, innerException)
    {
        ErrorCode = errorCode;
    }

    public string ErrorCode { get; }
}