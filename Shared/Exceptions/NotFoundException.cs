namespace Shared.Exceptions;

public class NotFoundException(string error) : CustomException(error)
{
    public NotFoundException() : this("Wrong username or password") { }
}
