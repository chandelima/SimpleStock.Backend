using System.Runtime.Serialization;

namespace SimpleStock.Exception;

[Serializable]
public class NotAllowedException : BaseException
{
    public NotAllowedException(string message) : base(message) { }

    protected NotAllowedException(SerializationInfo info, StreamingContext context)
    : base(info, context) { }
}
