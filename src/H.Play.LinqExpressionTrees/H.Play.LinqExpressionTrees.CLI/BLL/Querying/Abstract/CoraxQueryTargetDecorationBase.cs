using System.Collections.Generic;

namespace H.Play.LinqExpressionTrees.CLI.BLL.Querying.Abstract
{
    public abstract class CoraxQueryTargetDecorationBase : ICoraxQueryTargetDecoration
    {
        protected CoraxQueryTargetDecorationBase(string name, string @namespace = null, string typeName = null)
        {
            Name = name;
            Namespace = @namespace;
            TypeName = typeName;
        }

        public string Name { get; }

        public string Namespace { get; }

        public string TypeName { get; }

        public IDictionary<string, object> Arguments { get; } = new Dictionary<string, object>();
    }
}
