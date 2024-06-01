using H.Necessaire;

namespace H.Play.LinqExpressionTrees.CLI
{
    internal class CLIDependencyGroup : ImADependencyGroup
    {
        public void RegisterDependencies(ImADependencyRegistry dependencyRegistry)
        {
            dependencyRegistry
                .Register<BLL.DependencyGroup>(() => new BLL.DependencyGroup())
                ;
        }
    }
}
