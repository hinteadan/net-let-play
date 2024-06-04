using H.Necessaire;
using H.Play.LinqExpressionTrees.CLI.BLL.Querying.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
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
                var result
                    = new CoraxExplicitQueryTarget(path);
                if (unaryExpression.NodeType == ExpressionType.Convert)
                {
                    result.Decorations.Enqueue(BuildConvertDecorationFromUnaryExpression(unaryExpression));
                }
                return result;
            }

            if (targetExpression is MethodCallExpression methodCallExpression)
            {
                return
                    BuildCoraxQueryTargetFromMethodCall(methodCallExpression, BuildCoraxQueryTargetDecorationFrom(methodCallExpression));
            }

            throw new CoraxQueryExpressionNotSupportedException($"{targetExpression.NodeType} expression type having the concrete implementation {targetExpression.GetType().Name} is not supported by Corax Expression Querying");
        }

        private ICoraxQueryTarget BuildCoraxQueryTargetFromMethodCall(MethodCallExpression methodCallExpression, params ICoraxQueryTargetDecoration[] decorations)
        {
            var target
                = methodCallExpression.Object
                ?? methodCallExpression.Arguments.FirstOrDefault()
                ;

            if (target is null)
                return null;

            if(target is MethodCallExpression decoratorExpression)
            {
                return BuildCoraxQueryTargetFromMethodCall(decoratorExpression, [BuildCoraxQueryTargetDecorationFrom(decoratorExpression), .. decorations]);
            }

            ICoraxQueryTarget result = BuildCoraxQueryTarget(target);

            foreach(var decoration in decorations)
            {
                result.Decorations.Enqueue(decoration);
            }

            return
                result;
        }

        private ICoraxQueryTargetDecoration BuildCoraxQueryTargetDecorationFrom(MethodCallExpression decorationExpression)
        {
            var isExtensionMethod = decorationExpression.Object is null;
            var methodName = decorationExpression.Method.Name;
            var methodOwnerType = decorationExpression.Method.DeclaringType.FullName;
            var methodNamespace = decorationExpression.Method.DeclaringType.Namespace;
            var arguments = decorationExpression.Arguments.Skip(isExtensionMethod ? 1 : 0).Select(BuildCoraxQueryTargetDecorationArgumentFrom).ToDictionary();

            var result
                = new CoraxExplicitQueryTargetDecoration(
                    name: methodName,
                    @namespace: methodNamespace,
                    typeName: methodOwnerType
                );

            AddArgumentsIfAny(arguments, result.Arguments);

            return result;
        }

        private KeyValuePair<string, object> BuildCoraxQueryTargetDecorationArgumentFrom(Expression argumentExpression, int index)
        {
            if(argumentExpression is ConstantExpression constantExpression)
            {
                return new KeyValuePair<string, object>($"ConstantArg{index}", constantExpression.Value);
            }

            throw new CoraxQueryExpressionNotSupportedException($"{argumentExpression.NodeType} expression type having the concrete implementation {argumentExpression.GetType().Name} is not supported by Corax Expression Querying as target decoration argument");
        }

        private static void AddArgumentsIfAny(IDictionary<string, object> source, IDictionary<string, object> destination)
        {
            if (source?.Any() != true)
                return;

            foreach (var entry in source)
            {
                destination.Add(entry);
            }
        }

        private static CoraxExplicitQueryTargetDecoration BuildConvertDecorationFromUnaryExpression(UnaryExpression unaryExpression)
        {
            return new CoraxExplicitQueryTargetDecoration("Convert", @namespace: unaryExpression.Type.Namespace, typeName: unaryExpression.Type.Name);
        }
    }
}
