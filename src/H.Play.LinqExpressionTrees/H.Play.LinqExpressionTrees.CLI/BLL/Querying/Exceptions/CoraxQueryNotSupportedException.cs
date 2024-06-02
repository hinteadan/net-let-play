using System;

namespace H.Play.LinqExpressionTrees.CLI.BLL.Querying.Exceptions
{
    public class CoraxQueryNotSupportedException : InvalidOperationException
    {
        const string defaultMessage = "Query is not supported by Corax Expression Querying";
        public CoraxQueryNotSupportedException() : this(defaultMessage)
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
