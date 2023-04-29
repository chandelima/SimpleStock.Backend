using System.Runtime.Serialization;

namespace SimpleStock.Exception;

[Serializable]
public class NotFoundException : BaseException
{
    public NotFoundException(string message) : base(message) { }

    protected NotFoundException(SerializationInfo info, StreamingContext context)
    : base(info, context) { }
}
