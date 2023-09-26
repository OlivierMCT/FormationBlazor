using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CATodo.BLLContracts {
    public class CATodoException : Exception {
        public CATodoException() {
        }

        public CATodoException(string? message) : base(message) {
        }

        public CATodoException(string? message, Exception? innerException) : base(message, innerException) {
        }

        protected CATodoException(SerializationInfo info, StreamingContext context) : base(info, context) {
        }
    }
}
