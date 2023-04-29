using System.Runtime.Serialization;

namespace SimpleStock.Exception;

[Serializable]
public class AlreadyExistsException : BaseException
{
    public AlreadyExistsException(string message) : base(message) { }

    protected AlreadyExistsException(SerializationInfo info, StreamingContext context)
    : base(info, context) { }
}
