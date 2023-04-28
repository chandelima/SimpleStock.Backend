namespace SimpleStock.Exception;

public class AlreadyExistsException : BaseException
{
    public AlreadyExistsException(string message) : base(message) { }
}
