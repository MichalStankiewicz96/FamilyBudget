using FamilyBudget.Application.Behaviour.Exceptions;
using FluentAssertions;
using FluentAssertions.Specialized;

namespace FamilyBudget.UnitTests.Extensions;
public static class BaseApplicationExceptionValidationExtensions
{
    public static Task<ExceptionAssertions<TException>> WithErrorCode<TException>(this Task<ExceptionAssertions<TException>> task,
        string errorCode) where TException : BaseApplicationException
    {
        return task.Where(x => x.ErrorCode == errorCode);
    }
}