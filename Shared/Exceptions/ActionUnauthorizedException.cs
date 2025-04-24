namespace Shared.Exceptions;

public class ActionUnauthorizedException(string error) : CustomException(error);