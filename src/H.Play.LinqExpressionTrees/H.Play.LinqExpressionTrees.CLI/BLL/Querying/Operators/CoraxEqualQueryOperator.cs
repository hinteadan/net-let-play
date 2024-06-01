using H.Play.LinqExpressionTrees.CLI.BLL.Querying.Abstract;
using System.Linq.Expressions;

namespace H.Play.LinqExpressionTrees.CLI.BLL.Querying.Operators
{
    public class CoraxEqualQueryOperator : CoraxQueryOperatorBase
    {
        public static readonly ICoraxQueryOperator Instance = new CoraxEqualQueryOperator();
        public override string Symbol { get; } = "==";
        public override string[] SymbolAliases { get; } = ["="];
        public override ExpressionType[] ExpressionAliases => [ExpressionType.Equal];
    }
}
