using System;
using System.Collections.Generic;

namespace H.Play.LinqExpressionTrees.CLI.BLL.Querying.Abstract
{
    public abstract class CoraxQueryOperatorBase : ICoraxQueryOperator
    {
        public abstract string Symbol { get; }

        public virtual string[] SymbolAliases { get; } = Array.Empty<string>();

        public IDictionary<string, object> Attributes { get; } = new Dictionary<string, object>();

        public virtual string ToUnderlyingSymbol() => Symbol;
    }
}
