using H.Play.LinqExpressionTrees.CLI.BLL.Querying.Abstract;

namespace H.Play.LinqExpressionTrees.CLI.BLL.Querying.Concrete
{
    public class CoraxSimpleQueryCriteria : CoraxQueryCriteriaBase
    {
        public CoraxSimpleQueryCriteria(ICoraxQueryTarget target, ICoraxQueryOperator @operator, ICoraxQueryValue value)
            : base(target, @operator, value)
        {
        }
    }
}
