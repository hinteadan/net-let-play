using H.Play.LinqExpressionTrees.CLI.BLL.Querying.Abstract;

namespace H.Play.LinqExpressionTrees.CLI.BLL.Querying.Concrete
{
    public class CoraxExplicitQueryTarget : CoraxQueryTargetBase
    {
        public CoraxExplicitQueryTarget(string path) : base(path){}

        public static implicit operator CoraxExplicitQueryTarget(string path)
        {
            return new CoraxExplicitQueryTarget(path);
        }
    }
}
