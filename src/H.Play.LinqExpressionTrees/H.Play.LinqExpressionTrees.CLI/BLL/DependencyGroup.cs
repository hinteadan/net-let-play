using H.Necessaire;

namespace H.Play.LinqExpressionTrees.CLI.BLL
{
    internal class DependencyGroup : ImADependencyGroup
    {
        public void RegisterDependencies(ImADependencyRegistry dependencyRegistry)
        {
            dependencyRegistry
                .Register<Querying.DependencyGroup>(() => new Querying.DependencyGroup())
                ;
        }
    }
}
