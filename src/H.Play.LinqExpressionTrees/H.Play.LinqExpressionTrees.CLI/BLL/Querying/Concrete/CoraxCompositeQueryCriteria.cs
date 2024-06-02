using H.Play.LinqExpressionTrees.CLI.BLL.Querying.Abstract;
using System.Collections.Generic;

namespace H.Play.LinqExpressionTrees.CLI.BLL.Querying.Concrete
{
    public class CoraxCompositeQueryCriteria : CoraxQueryCriteriaBase
    {
        public CoraxCompositeQueryCriteria(IEnumerable<ICoraxQueryCriteria> criterias, ICoraxQueryOperator @operator)
            : base(target: null, @operator, value: new CoraxExplicitQueryValue(criterias))
        {
        }
    }
}
