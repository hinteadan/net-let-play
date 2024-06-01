using H.Play.LinqExpressionTrees.CLI.BLL.Querying.Abstract;

namespace H.Play.LinqExpressionTrees.CLI.BLL.Querying.Operators
{
    public class CoraxEqualsQueryOperator : CoraxQueryOperatorBase
    {
        public static readonly ICoraxQueryOperator Instance = new CoraxEqualsQueryOperator();
        public override string Symbol { get; } = "==";
        public override string[] SymbolAliases { get; } = ["="];
    }
}
