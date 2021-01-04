using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fistix.Training.Core.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException()
        {

        }
        public NotFoundException(string message)
        {
            Message = message;
        }

        public string Message { get; set; }
    }
}
