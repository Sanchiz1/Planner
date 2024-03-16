using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Exceptions
{
    public class PermissionDeniedException : Exception
    {
        public PermissionDeniedException()
        {
        }

        public PermissionDeniedException(string message)
            : base(message)
        {
        }

        public PermissionDeniedException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
