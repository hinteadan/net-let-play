using H.Play.LinqExpressionTrees.CLI.BLL.Querying.Abstract;

namespace H.Play.LinqExpressionTrees.CLI.BLL.Querying.Operators
{
    public class CoraxNotEqualQueryOperator : CoraxQueryOperatorBase
    {
        public static readonly ICoraxQueryOperator Instance = new CoraxNotEqualQueryOperator();
        public override string Symbol { get; } = "!=";
        public override string[] SymbolAliases { get; } = ["<=>"];
    }
}
