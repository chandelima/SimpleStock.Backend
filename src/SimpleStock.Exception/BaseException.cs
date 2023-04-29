using System.Runtime.Serialization;

namespace SimpleStock.Exception;

public abstract class BaseException : System.Exception
{
    protected BaseException(string message) : base(message) { }
    protected BaseException(SerializationInfo info, StreamingContext context) 
    : base(info, context) { }
}
