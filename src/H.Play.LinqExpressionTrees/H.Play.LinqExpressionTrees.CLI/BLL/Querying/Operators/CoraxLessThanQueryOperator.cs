using H.Play.LinqExpressionTrees.CLI.BLL.Querying.Abstract;

namespace H.Play.LinqExpressionTrees.CLI.BLL.Querying.Operators
{
    public class CoraxLessThanQueryOperator : CoraxQueryOperatorBase
    {
        public static readonly ICoraxQueryOperator Instance = new CoraxLessThanQueryOperator();
        public override string Symbol { get; } = "<";
    }
}
