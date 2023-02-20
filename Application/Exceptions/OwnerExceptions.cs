using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Exceptions;

internal class OwnerException : Exception
{
    public OwnerException(string message) : base(message) { }
}

internal class RepeatingOwnerException : OwnerException
{
    public RepeatingOwnerException(string message) : base(message) { }
}
