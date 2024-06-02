using H.Necessaire;
using H.Play.LinqExpressionTrees.CLI.BLL.Querying.Exceptions;
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

            if (body is ConstantExpression constantExpression)
            {
                return
                    new CoraxSimpleQueryCriteria(
                        target: null,
                        @operator: null,
                        value: new CoraxExplicitQueryValue(constantExpression.Value)
                    );
            }

            if (body is BinaryExpression binaryExpression)
            {
                return
                    BuildFromBinaryExpression(binaryExpression);
            }

            return null;
        }

        private ICoraxQueryCriteria BuildFromBinaryExpression(BinaryExpression binaryExpression)
        {
            ICoraxQueryTarget target = BuildCoraxQueryTarget(binaryExpression.Left);


            return null;
        }

        private ICoraxQueryTarget BuildCoraxQueryTarget(Expression targetExpression)
        {
            if (targetExpression is ParameterExpression parameterExpression)
            {
                return
                    new CoraxExplicitQueryTarget(null);
            }

            if (targetExpression is MemberExpression memberExpression)
            {
                string path = memberExpression.ToString();
                path = path.Substring(path.IndexOf(".") + 1);
                return
                    new CoraxExplicitQueryTarget(path);
            }

            throw new CoraxQueryNotSupportedException($"{targetExpression.NodeType} expression type having the concrete implementation {targetExpression.GetType().Name} is not supported by Corax Querying");
        }
    }
}
