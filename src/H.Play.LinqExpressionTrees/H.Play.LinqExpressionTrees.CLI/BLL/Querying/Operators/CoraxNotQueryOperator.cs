using H.Play.LinqExpressionTrees.CLI.BLL.Querying.Abstract;

namespace H.Play.LinqExpressionTrees.CLI.BLL.Querying.Operators
{
    public class CoraxNotQueryOperator : CoraxQueryOperatorBase
    {
        public static readonly ICoraxQueryOperator Instance = new CoraxNotQueryOperator();
        public override string Symbol { get; } = "!";
        public override string[] SymbolAliases { get; } = ["not", "Not", "NOT"];
    }
}
