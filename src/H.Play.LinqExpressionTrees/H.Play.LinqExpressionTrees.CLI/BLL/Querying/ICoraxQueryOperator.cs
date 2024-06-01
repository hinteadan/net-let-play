using H.Necessaire;
using System.Collections.Generic;

namespace H.Play.LinqExpressionTrees.CLI.BLL.Querying
{
    public interface ICoraxQueryOperator : IStringIdentity
    {
        string Symbol { get; }
        string[] SymbolAliases { get; }
        IDictionary<string, object> Attributes { get; }
        string ToStorageSymbol();
    }
}
