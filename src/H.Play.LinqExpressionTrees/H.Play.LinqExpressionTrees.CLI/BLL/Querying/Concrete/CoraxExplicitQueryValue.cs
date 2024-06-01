using H.Play.LinqExpressionTrees.CLI.BLL.Querying.Abstract;

namespace H.Play.LinqExpressionTrees.CLI.BLL.Querying.Concrete
{
    public class CoraxExplicitQueryValue : CoraxQueryValueBase
    {
        public CoraxExplicitQueryValue(object value, bool isParameter = false) : base(value, isParameter){}
    }

    public class CoraxExplicitQueryValue<T> : CoraxExplicitQueryValue
    {
        public CoraxExplicitQueryValue(T value, bool isParameter = false) : base(value, isParameter) { }

        public static implicit operator CoraxExplicitQueryValue<T>(T value)
        {
            return new CoraxExplicitQueryValue<T>(value);
        }

        public static implicit operator CoraxExplicitQueryValue<T>((T value, bool isParameter) props)
        {
            return new CoraxExplicitQueryValue<T>(props.value, props.isParameter);
        }
    }
}
