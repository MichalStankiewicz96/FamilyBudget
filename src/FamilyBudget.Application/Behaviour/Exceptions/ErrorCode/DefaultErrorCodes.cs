namespace FamilyBudget.Application.Behaviour.Exceptions.ErrorCode;

public sealed class DefaultErrorCodes
{
    public string AlreadyExist => "EntityAlreadyExist";
    public string ExternalServiceFailed => "ExternalServiceFailed";
    public string Forbidden => "Forbidden";
    public string NotFound => "NotFound";
    public string VerificationError => "VerificationError";
    public string ValidationFailed => "ValidationFailed";
}