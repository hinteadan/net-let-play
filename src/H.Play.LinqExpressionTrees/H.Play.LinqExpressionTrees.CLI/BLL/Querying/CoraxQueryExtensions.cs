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

        public static IEnumerable<ICoraxQueryCriteria> AsCriteria(this ICoraxQueryValue value)
        {
            if (!value.IsCriteria)
                return null;

            return value.Value as IEnumerable<ICoraxQueryCriteria>;
        }
    }
}
