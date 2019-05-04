using System.Collections.Generic;
using System.IO;

namespace SmartAss.Communication
{
    public interface IMessageReader
    {
        IEnumerable<object> Read(TextReader reader);
    }
}
