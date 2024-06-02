using H.Necessaire;
using H.Play.LinqExpressionTrees.CLI.BLL.Querying.Exceptions;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace H.Play.LinqExpressionTrees.CLI.BLL.Querying.Concrete
{
    internal class CoraxExplicitQueryBuilder : ICoraxExplicitQueryBuilder, ImADependency
    {
        ICoraxQueryPartsFactory coraxQueryPartsFactory;
        public void ReferDependencies(ImADependencyProvider dependencyProvider)
        {
            coraxQueryPartsFactory = dependencyProvider.Get<ICoraxQueryPartsFactory>();
        }


        public ICoraxQueryTarget Target(string path, IDictionary<string, object> attributes = null)
        {
            var result = new CoraxExplicitQueryTarget(path);
            AddAttributesIfAny(attributes, result.Attributes);
            return result;
        }


        public ICoraxQueryOperator Operator(string symbol, IDictionary<string, object> attributes = null)
        {
            var result
                = coraxQueryPartsFactory.Operator(symbol)
                ??
                throw new CoraxQueryNotSupportedException($"{symbol} operator is not supported by Corax Querying or by the underlying storage solution");
            ;
            AddAttributesIfAny(attributes, result.Attributes);
            return result;
        }

        public ICoraxQueryOperator Operator(ExpressionType type, IDictionary<string, object> attributes = null)
        {
            var result
                = coraxQueryPartsFactory.Operator(type)
                ??
                throw new CoraxQueryNotSupportedException($"{type} operator is not supported by Corax Querying or by the underlying storage solution");
            ;
            AddAttributesIfAny(attributes, result.Attributes);
            return result;
        }


        public ICoraxQueryValue ConstantValue(object value, IDictionary<string, object> attributes = null)
        {
            var result
                = new CoraxExplicitQueryValue(value);
            AddAttributesIfAny(attributes, result.Attributes);
            return result;
        }

        public ICoraxQueryValue ParameterValue(string parameterName, object parametersObject, IDictionary<string, object> attributes = null)
        {
            var result
                = new CoraxExplicitQueryValue(parameterName, isParameter: true)
                .SetParametersObject(parametersObject)
                ;
            AddAttributesIfAny(attributes, result.Attributes);
            return result;
        }

        public ICoraxQueryValue SubQueryValue(ICoraxQueryCriteria criteria, IDictionary<string, object> attributes = null)
        {
            var result
                = new CoraxExplicitQueryValue<ICoraxQueryCriteria>(criteria);
            AddAttributesIfAny(attributes, result.Attributes);
            return result;
        }


        public ICoraxQueryCriteria Simple(ICoraxQueryTarget target, ICoraxQueryOperator @operator, ICoraxQueryValue value)
        {
            return
                new CoraxSimpleQueryCriteria(target, @operator, value);
        }

        public ICoraxQueryCriteria Simple(string path, string operatorSymbol, object value, IDictionary<string, object> attributes = null)
        {
            return
                new CoraxSimpleQueryCriteria(Target(path, attributes), Operator(operatorSymbol, attributes), ConstantValue(value, attributes));
        }


        public ICoraxQueryCriteria ComposedWithAnd(IEnumerable<ICoraxQueryCriteria> criterias)
        {
            return
                new CoraxCompositeQueryCriteria(criterias, @operator: coraxQueryPartsFactory.Operator("&&"));
        }

        public ICoraxQueryCriteria ComposedWithOr(IEnumerable<ICoraxQueryCriteria> criterias)
        {
            return
                new CoraxCompositeQueryCriteria(criterias, @operator: coraxQueryPartsFactory.Operator("||"));
        }


        private static void AddAttributesIfAny(IDictionary<string, object> source, IDictionary<string, object> destination)
        {
            if (source?.Any() != true)
                return;

            foreach (var entry in source)
            {
                destination.Add(entry);
            }
        }
    }
}
