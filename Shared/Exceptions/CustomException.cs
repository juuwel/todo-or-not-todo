namespace Shared.Exceptions;

public abstract class CustomException(string error) : Exception(error);
