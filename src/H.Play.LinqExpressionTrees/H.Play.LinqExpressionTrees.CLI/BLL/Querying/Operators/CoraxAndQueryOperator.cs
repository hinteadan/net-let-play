using H.Play.LinqExpressionTrees.CLI.BLL.Querying.Abstract;
using System.Linq.Expressions;

namespace H.Play.LinqExpressionTrees.CLI.BLL.Querying.Operators
{
    public class CoraxAndQueryOperator : CoraxQueryOperatorBase
    {
        public static readonly ICoraxQueryOperator Instance = new CoraxAndQueryOperator();
        public override string Symbol { get; } = "&&";
        public override string[] SymbolAliases { get; } = ["&", "and", "And", "AND"];
        public override ExpressionType[] ExpressionAliases => [ExpressionType.And, ExpressionType.AndAlso];
    }
}
