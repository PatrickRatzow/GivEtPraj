using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commentor.GivEtPraj.Domain.Exceptions
{
    public class InvalidLatitudeException : Exception
    {
        public InvalidLatitudeException(string? message) : base(message)
        {
        }
    }
}
