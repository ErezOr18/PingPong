using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace pingPong.Common
{
    public class RequestInfoFormatter
    {
        private BinaryFormatter _formatter;

        public RequestInfoFormatter()
        {
            _formatter = new BinaryFormatter(); 
        }
        public RequestInfo Deserialize(byte[] buffer)
        {
            MemoryStream stream = new MemoryStream();   
            stream.Seek(0, SeekOrigin.Begin);   
            return _formatter.Deserialize(stream) as RequestInfo;
        }

        public byte[] Serialize(RequestInfo requestInfo)
        {
            MemoryStream stream = new MemoryStream();
            _formatter.Serialize(stream, requestInfo);
            return stream.ToArray();
        }
    }
}
