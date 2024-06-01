using H.Play.LinqExpressionTrees.CLI.BLL.Querying.Abstract;

namespace H.Play.LinqExpressionTrees.CLI.BLL.Querying.Concrete
{
    public class CoraxExplicitQueryValue : CoraxQueryValueBase
    {
        public CoraxExplicitQueryValue(object value) : base(value){}
    }

    public class CoraxExplicitQueryValue<T> : CoraxExplicitQueryValue
    {
        public CoraxExplicitQueryValue(T value) : base(value) { }

        public static implicit operator CoraxExplicitQueryValue<T>(T value)
        {
            return new CoraxExplicitQueryValue<T>(value);
        }
    }
}
