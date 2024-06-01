using System.Collections.Generic;

namespace H.Play.LinqExpressionTrees.CLI.BLL.Querying
{
    public interface ICoraxQueryOperator
    {
        string Symbol { get; }
        string[] SymbolAliases { get; }
        IDictionary<string, object> Attributes { get; }
        string ToUnderlyingSymbol();
    }
}
