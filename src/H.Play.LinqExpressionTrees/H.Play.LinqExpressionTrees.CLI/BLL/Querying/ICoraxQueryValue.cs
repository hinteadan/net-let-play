using System.Collections.Generic;

namespace H.Play.LinqExpressionTrees.CLI.BLL.Querying
{
    public interface ICoraxQueryValue
    {
        bool IsParameter { get; }
        object Value { get; }
        IDictionary<string, object> Attributes { get; }
        object ToUnderlyingSymbol();
    }
}
