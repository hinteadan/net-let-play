using System;

namespace H.Play.LinqExpressionTrees.CLI.BLL.Querying.Exceptions
{
    public class CoraxQueryExpressionNotSupportedException : InvalidOperationException
    {
        const string defaultMessage = "Expression is not supported by Corax Expression Querying";
        const string hintMessage = "Try constructing the query explicitly, instead of using LINQ expressions";
        const string defaultMessagePlusHint = $"{defaultMessage}. {hintMessage}.";
        public CoraxQueryExpressionNotSupportedException() : this(null)
        {
        }

        public CoraxQueryExpressionNotSupportedException(string message) : base(DecorateExceptionMessage(message))
        {
        }

        public CoraxQueryExpressionNotSupportedException(string message, Exception innerException) : base(DecorateExceptionMessage(message), innerException)
        {
        }

        private static string DecorateExceptionMessage(string message)
        {
            return string.IsNullOrWhiteSpace(message) ? defaultMessagePlusHint : $"{message}. {hintMessage}.";
        }
    }
}
