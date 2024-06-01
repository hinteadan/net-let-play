using System.Collections.Generic;
using System.Linq.Expressions;

namespace H.Play.LinqExpressionTrees.CLI.BLL.Querying
{
    public interface ICoraxQueryOperator
    {
        string Symbol { get; }
        string[] SymbolAliases { get; }
        ExpressionType[] ExpressionAliases { get; }
        IDictionary<string, object> Attributes { get; }
        string ToUnderlyingSymbol();
    }
}
