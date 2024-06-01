namespace H.Play.LinqExpressionTrees.CLI.BLL.Querying.Abstract
{
    public abstract class CoraxQueryCriteriaBase : ICoraxQueryCriteria
    {
        protected CoraxQueryCriteriaBase(ICoraxQueryTarget target, ICoraxQueryOperator @operator, ICoraxQueryValue value)
        {
            Target = target;
            Operator = @operator;
            Value = value;
        }

        public ICoraxQueryTarget Target { get; }

        public ICoraxQueryOperator Operator { get; }

        public ICoraxQueryValue Value { get; }
    }
}
