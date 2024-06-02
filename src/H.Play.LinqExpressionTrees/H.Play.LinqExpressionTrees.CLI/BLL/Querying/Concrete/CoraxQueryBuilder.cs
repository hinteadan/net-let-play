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
            Explicit = dependencyProvider.Get<ICoraxExplicitQueryBuilder>();
        }

        public ICoraxExplicitQueryBuilder Explicit { get; private set; }

        public ICoraxQueryCriteria BuildFromExpression<TEntity>(Expression<Func<TEntity, bool>> criteriaExpression)
        {
            return BuildFromCriteriaExpression(criteriaExpression.Body);
        }

        private ICoraxQueryCriteria BuildFromCriteriaExpression(Expression criteriaExpression)
        {
            if (criteriaExpression is ConstantExpression constantExpression)
            {
                return
                    new CoraxSimpleQueryCriteria(
                        target: null,
                        @operator: null,
                        value: new CoraxExplicitQueryValue(constantExpression.Value)
                    );
            }

            if (criteriaExpression is BinaryExpression binaryExpression)
            {
                if (IsComposition(binaryExpression))
                {
                    return
                        BuildFromBinaryCompositionExpression(binaryExpression);
                }

                return
                    BuildFromBinaryExpression(binaryExpression);
            }

            return null;
        }

        private ICoraxQueryCriteria BuildFromBinaryCompositionExpression(BinaryExpression binaryExpression)
        {
            ICoraxQueryCriteria left = BuildFromCriteriaExpression(binaryExpression.Left);
            ICoraxQueryCriteria right = BuildFromCriteriaExpression(binaryExpression.Right);
            ICoraxQueryOperator queryOperator = BuildCoraxQueryOperator(binaryExpression);

            return new CoraxCompositeQueryCriteria([left, right], queryOperator);
        }

        private bool IsComposition(BinaryExpression binaryExpression)
        {
            return
                binaryExpression.NodeType == ExpressionType.And
                || binaryExpression.NodeType == ExpressionType.AndAlso
                || binaryExpression.NodeType == ExpressionType.Or
                || binaryExpression.NodeType == ExpressionType.OrElse
                ;
        }

        private ICoraxQueryCriteria BuildFromBinaryExpression(BinaryExpression binaryExpression)
        {
            ICoraxQueryTarget queryTarget = BuildCoraxQueryTarget(binaryExpression.Left);
            ICoraxQueryOperator queryOperator = BuildCoraxQueryOperator(binaryExpression);
            ICoraxQueryValue queryValue = BuildCoraxQueryValue(binaryExpression.Right);

            return new CoraxSimpleQueryCriteria(queryTarget, queryOperator, queryValue);
        }

        private ICoraxQueryValue BuildCoraxQueryValue(Expression valueExpression)
        {
            if (valueExpression is ConstantExpression constantExpression)
            {
                object value = constantExpression.Value;
                return new CoraxExplicitQueryValue(value);
            }

            if (valueExpression is MemberExpression memberExpression)
            {
                object value = GetMemberExpressionValue(memberExpression);
                return new CoraxExplicitQueryValue(value);
            }

            throw new CoraxQueryExpressionNotSupportedException($"{valueExpression.NodeType} value expression type having the concrete implementation {valueExpression.GetType().Name} is not supported by Corax Expression Querying");
        }

        private object GetMemberExpressionValue(MemberExpression memberExpression)
        {
            var objectMember = Expression.Convert(memberExpression, typeof(object));
            var getterLambda = Expression.Lambda<Func<object>>(objectMember);
            var getter = getterLambda.Compile();
            return getter();
        }

        private ICoraxQueryOperator BuildCoraxQueryOperator(BinaryExpression binaryExpression)
        {
            return
                coraxQueryPartsFactory.Operator(binaryExpression.NodeType)
                ??
                throw new CoraxQueryExpressionNotSupportedException($"{binaryExpression.NodeType} expression operator is not supported by Corax Expression Querying or by the underlying storage solution");
                ;
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

            if (targetExpression is UnaryExpression unaryExpression)
            {
                string path = unaryExpression.Operand.ToString();
                path = path.Substring(path.IndexOf(".") + 1);
                return
                    new CoraxExplicitQueryTarget(path);
            }

            throw new CoraxQueryExpressionNotSupportedException($"{targetExpression.NodeType} expression type having the concrete implementation {targetExpression.GetType().Name} is not supported by Corax Expression Querying");
        }
    }
}
