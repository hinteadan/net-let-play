using System.Collections.Generic;
using System.Linq.Expressions;

namespace H.Play.LinqExpressionTrees.CLI.BLL.Querying
{
    public interface ICoraxExplicitQueryBuilder
    {
        ICoraxQueryTarget Target(string path, IDictionary<string, object> attributes = null);


        ICoraxQueryOperator Operator(string symbol, IDictionary<string, object> attributes = null);
        ICoraxQueryOperator Operator(ExpressionType type, IDictionary<string, object> attributes = null);


        ICoraxQueryValue ConstantValue(object value, IDictionary<string, object> attributes = null);
        ICoraxQueryValue ParameterValue(string parameterName, object parametersObject, IDictionary<string, object> attributes = null);
        ICoraxQueryValue SubQueryValue(ICoraxQueryCriteria criteria, IDictionary<string, object> attributes = null);


        ICoraxQueryCriteria Simple(ICoraxQueryTarget target, ICoraxQueryOperator @operator, ICoraxQueryValue value);
        ICoraxQueryCriteria Simple(string path, string operatorSymbol, object value);
        ICoraxQueryCriteria ComposedWithAnd(IEnumerable<ICoraxQueryCriteria> criterias);
        ICoraxQueryCriteria ComposedWithOr(IEnumerable<ICoraxQueryCriteria> criterias);
    }
}
