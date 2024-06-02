using H.Necessaire;
using System;
using System.Linq.Expressions;

namespace H.Play.LinqExpressionTrees.CLI.BLL.Querying.Concrete
{
    internal class CoraxQueryBuilder : ICoraxQueryBuilder, ImADependency
    {
        ICoraxQueryPartsFactory coraxQueryPartsFactory;
        public void ReferDependencies(ImADependencyProvider dependencyProvider)
        {
            coraxQueryPartsFactory = dependencyProvider.Get<ICoraxQueryPartsFactory>();
        }

        public ICoraxQueryCriteria BuildFromExpression<TEntity>(Expression<Func<TEntity, bool>> criteriaExpression)
        {
            var body = criteriaExpression.Body;

            if(body is ConstantExpression constantExpression)
            {
                return
                    new CoraxSimpleQueryCriteria(
                        target: null,
                        @operator: null,
                        value: new CoraxExplicitQueryValue(constantExpression.Value)
                    );
            }

            return null;
        }
    }
}
