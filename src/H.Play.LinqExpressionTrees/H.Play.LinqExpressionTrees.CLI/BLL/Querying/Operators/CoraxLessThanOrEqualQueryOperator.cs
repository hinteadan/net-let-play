using H.Play.LinqExpressionTrees.CLI.BLL.Querying.Abstract;
using System.Linq.Expressions;

namespace H.Play.LinqExpressionTrees.CLI.BLL.Querying.Operators
{
    public class CoraxLessThanOrEqualQueryOperator : CoraxQueryOperatorBase
    {
        public static readonly ICoraxQueryOperator Instance = new CoraxLessThanOrEqualQueryOperator();
        public override string Symbol { get; } = "<=";
        public override ExpressionType[] ExpressionAliases => [ExpressionType.LessThanOrEqual];
    }
}
