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

        public static ICoraxQueryCriteria AsCriteria(this ICoraxQueryValue value)
        {
            if (!value.IsCriteria)
                return null;

            return value.Value as ICoraxQueryCriteria;
        }

        public static ICoraxQueryValue SetParametersObject(this ICoraxQueryValue coraxQueryValue, object parametersObject)
        {
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
