using System.Collections.Generic;

namespace H.Play.LinqExpressionTrees.CLI.BLL.Querying
{
    public static class CoraxQueryExtensions
    {
        public static bool IsConstant(this ICoraxQueryCriteria coraxQueryCriteria)
        {
            return
                coraxQueryCriteria.Target is null
                && coraxQueryCriteria.Operator is null
                && coraxQueryCriteria.Value is not null
                ;
        }

        public static bool IsDecorated(this ICoraxQueryTarget coraxQueryTarget)
        {
            return coraxQueryTarget.Decorations.Count > 0;
        }

        public static IEnumerable<ICoraxQueryCriteria> AsCriteria(this ICoraxQueryValue value)
        {
            if (!value.IsCriteria)
                return null;

            if (value.Value is ICoraxQueryCriteria criteriaValue)
                return [criteriaValue];

            return value.Value as IEnumerable<ICoraxQueryCriteria>;
        }

        public static ICoraxQueryValue SetParametersObject(this ICoraxQueryValue coraxQueryValue, object parametersObject)
        {
            if (parametersObject is null)
                return coraxQueryValue;

            coraxQueryValue.Attributes.Remove("CoraxQueryParametersObject");
            coraxQueryValue.Attributes.Add("CoraxQueryParametersObject", parametersObject);
            return coraxQueryValue;
        }

        public static object GetParametersObject(this ICoraxQueryValue coraxQueryValue)
        {
            if (!coraxQueryValue.Attributes.TryGetValue("CoraxQueryParametersObject", out var result))
            {
                return null;
            }
            
            return result;
        }
    }
}
