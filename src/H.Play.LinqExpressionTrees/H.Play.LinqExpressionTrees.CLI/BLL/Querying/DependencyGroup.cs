using H.Necessaire;

namespace H.Play.LinqExpressionTrees.CLI.BLL.Querying
{
    internal class DependencyGroup : ImADependencyGroup
    {
        public void RegisterDependencies(ImADependencyRegistry dependencyRegistry)
        {
            dependencyRegistry
                .Register<ICoraxQueryPartsFactory>(ICoraxQueryPartsFactory.New)
                .Register<ICoraxExplicitQueryBuilder>(() => new Concrete.CoraxExplicitQueryBuilder())
                .Register<ICoraxQueryBuilder>(() => new Concrete.CoraxQueryBuilder())
                ;
        }
    }
}
