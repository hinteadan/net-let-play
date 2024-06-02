using System.Collections.Generic;

namespace H.Play.LinqExpressionTrees.CLI.BLL.Querying.Abstract
{
    public abstract class CoraxQueryValueBase : ICoraxQueryValue
    {
        protected CoraxQueryValueBase(object value, bool isParameter = false)
        {
            Value = value;
            IsParameter = isParameter;
            IsCriteria = Value is ICoraxQueryCriteria;
        }

        public virtual bool IsParameter { get; }
        public bool IsCriteria { get; }
        public object Value { get; }
        public IDictionary<string, object> Attributes { get; } = new Dictionary<string, object>();
        public virtual object ToUnderlyingSymbol() => Value;
    }
}
