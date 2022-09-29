using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Dictionary.Common.Exceptions
{
    public sealed class BusinessException:Exception
    {

        public BusinessException()
        {
            
        }

        public BusinessException(string? message):base(message)
        {
            
        }

        public BusinessException(string? message,Exception innerException):base(message,innerException)
        {
            
        }

        public BusinessException(SerializationInfo serializationInfo,StreamingContext context):base(serializationInfo,context)
        {
            
        }
    }
}
