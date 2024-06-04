using System.Collections.Generic;

namespace H.Play.LinqExpressionTrees.CLI.BLL.Querying
{
    public interface ICoraxQueryTarget
    {
        string Path { get; }
        Queue<ICoraxQueryTargetDecoration> Decorations { get; }
        IDictionary<string, object> Attributes { get; }
        string ToUnderlyingPath();
    }
}
