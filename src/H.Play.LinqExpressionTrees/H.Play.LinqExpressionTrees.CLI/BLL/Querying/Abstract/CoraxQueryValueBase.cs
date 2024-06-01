using System.Collections.Generic;

namespace H.Play.LinqExpressionTrees.CLI.BLL.Querying.Abstract
{
    public abstract class CoraxQueryValueBase : ICoraxQueryValue
    {
        protected CoraxQueryValueBase(object value)
        {
            Value = value;
        }

        public virtual bool IsParameter { get; } = false;
        public object Value { get; }
        public IDictionary<string, object> Attributes { get; } = new Dictionary<string, object>();
        public virtual object ToUnderlyingSymbol() => Value;
    }
}
