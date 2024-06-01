using H.Play.LinqExpressionTrees.CLI.BLL.Querying.Abstract;

namespace H.Play.LinqExpressionTrees.CLI.BLL.Querying.Operators
{
    public class CoraxOrQueryOperator : CoraxQueryOperatorBase
    {
        public static readonly ICoraxQueryOperator Instance = new CoraxOrQueryOperator();
        public override string Symbol { get; } = "||";
        public override string[] SymbolAliases { get; } = ["|", "or", "Or", "OR"];
    }
}
