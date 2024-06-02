using System;
using System.Linq.Expressions;

namespace H.Play.LinqExpressionTrees.CLI.BLL.Querying
{
    public interface ICoraxQueryBuilder
    {
        ICoraxQueryCriteria BuildFromExpression<TEntity>(Expression<Func<TEntity, bool>> criteriaExpression);
        ICoraxExplicitQueryBuilder Explicit { get; }
    }
}
