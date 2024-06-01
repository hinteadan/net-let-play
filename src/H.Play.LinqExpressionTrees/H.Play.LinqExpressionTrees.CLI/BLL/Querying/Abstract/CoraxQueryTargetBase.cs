using System.Collections.Generic;

namespace H.Play.LinqExpressionTrees.CLI.BLL.Querying.Abstract
{
    public abstract class CoraxQueryTargetBase : ICoraxQueryTarget
    {
        protected CoraxQueryTargetBase(string path)
        {
            Path = path;
        }

        public string Path { get; }

        public IDictionary<string, object> Attributes { get; } = new Dictionary<string, object>();

        public virtual string ToUnderlyingPath() => Path;
    }
}
