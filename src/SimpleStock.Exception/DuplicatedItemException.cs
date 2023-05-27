using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SimpleStock.Exception;

[Serializable]
public class DuplicatedItemException : BaseException
{
    public DuplicatedItemException(string message) : base(message) { }

    protected DuplicatedItemException(SerializationInfo info, StreamingContext context)
    : base(info, context) { }
}
