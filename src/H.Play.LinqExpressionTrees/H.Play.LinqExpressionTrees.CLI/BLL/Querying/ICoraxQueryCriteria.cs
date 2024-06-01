namespace H.Play.LinqExpressionTrees.CLI.BLL.Querying
{
    public interface ICoraxQueryCriteria
    {
        ICoraxQueryTarget Target { get; }
        ICoraxQueryOperator Operator { get; }
        ICoraxQueryValue Value { get; }
        string ToString();
    }
}
