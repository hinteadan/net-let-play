using H.Play.LinqExpressionTrees.CLI.BLL.Querying.Abstract;
using System.Linq.Expressions;

namespace H.Play.LinqExpressionTrees.CLI.BLL.Querying.Operators
{
    public class CoraxGreaterThanQueryOperator : CoraxQueryOperatorBase
    {
        public static readonly ICoraxQueryOperator Instance = new CoraxGreaterThanQueryOperator();
        public override string Symbol { get; } = ">";
        public override ExpressionType[] ExpressionAliases => [ExpressionType.GreaterThan];
    }
}
