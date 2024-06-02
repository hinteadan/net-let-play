using System;

namespace H.Play.LinqExpressionTrees.CLI.BLL.Querying.Exceptions
{
    public class CoraxQueryNotSupportedException : InvalidOperationException
    {
        public CoraxQueryNotSupportedException()
        {
        }

        public CoraxQueryNotSupportedException(string message) : base(message)
        {
        }

        public CoraxQueryNotSupportedException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
