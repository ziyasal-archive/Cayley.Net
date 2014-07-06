using System;
using System.Linq;

namespace Cayley.Net.Extensions
{
    internal static class AggregateExceptionExtensions
    {
        internal static bool TryUnwrap(this AggregateException ex, out Exception exception)
        {
            ex = ex.Flatten();
            if (ex.InnerExceptions.Count() == 1)
            {
                exception = ex.InnerExceptions.Single();
                return true;
            }
            exception = null;
            return false;
        }
    }
}