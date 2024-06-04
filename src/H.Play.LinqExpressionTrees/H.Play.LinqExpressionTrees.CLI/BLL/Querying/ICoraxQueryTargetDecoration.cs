using System.Collections.Generic;

namespace H.Play.LinqExpressionTrees.CLI.BLL.Querying
{
    public interface ICoraxQueryTargetDecoration
    {
        string Name { get; }
        string Namespace { get; }
        string TypeName { get; }
        IDictionary<string, object> Arguments { get; }
    }
}
