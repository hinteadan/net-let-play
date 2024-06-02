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


        public ICoraxQueryCriteria ComposedWithAnd(IEnumerable<ICoraxQueryCriteria> criterias)
        {
            throw new System.NotImplementedException();
        }

        public ICoraxQueryCriteria ComposedWithOr(IEnumerable<ICoraxQueryCriteria> criterias)
        {
            throw new System.NotImplementedException();
        }

        public ICoraxQueryValue ConstantValue(object value, IDictionary<string, object> attributes = null)
        {
            throw new System.NotImplementedException();
        }



        public ICoraxQueryValue ParameterValue(string parameterName, object parametersObject, IDictionary<string, object> attributes = null)
        {
            throw new System.NotImplementedException();
        }

        public ICoraxQueryCriteria Simple(ICoraxQueryTarget target, ICoraxQueryOperator @operator, ICoraxQueryValue value)
        {
            throw new System.NotImplementedException();
        }

        public ICoraxQueryCriteria Simple(string target, string @operator, object value)
        {
            throw new System.NotImplementedException();
        }

        public ICoraxQueryValue SubQueryValue(ICoraxQueryCriteria criteria, IDictionary<string, object> attributes = null)
        {
            throw new System.NotImplementedException();
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
