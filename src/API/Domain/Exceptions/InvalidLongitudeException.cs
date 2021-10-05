using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commentor.GivEtPraj.Domain.Exceptions
{
    public class InvalidLongitudeException : Exception
    {
        public InvalidLongitudeException(string? message) : base(message)
        {
        }
    }
}
