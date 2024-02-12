namespace Application.Common.Exceptions;

public class CustomException(string message)
    : Exception
{
    public new string Message { get; } = message;
}