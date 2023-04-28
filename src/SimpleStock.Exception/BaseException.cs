namespace SimpleStock.Exception;

public abstract class BaseException : System.Exception
{
    public BaseException(string message) : base(message) { }
}
