using H.Play.LinqExpressionTrees.CLI.BLL.Querying.Abstract;

namespace H.Play.LinqExpressionTrees.CLI.BLL.Querying.Concrete
{
    public class CoraxExplicitQueryTargetDecoration : CoraxQueryTargetDecorationBase
    {
        public CoraxExplicitQueryTargetDecoration(string name, string @namespace = null, string typeName = null) : base(name, @namespace, typeName)
        {
        }
    }
}
