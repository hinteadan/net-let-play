using H.Necessaire;
using H.Necessaire.Runtime.CLI.Commands;
using H.Play.LinqExpressionTrees.CLI.BLL.Querying;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace H.Play.LinqExpressionTrees.CLI.Commands
{
    internal class DebugCommand : CommandBase
    {
        ICoraxQueryBuilder coraxQueryBuilder;

        public override void ReferDependencies(ImADependencyProvider dependencyProvider)
        {
            base.ReferDependencies(dependencyProvider);
            coraxQueryBuilder = dependencyProvider.Get<ICoraxQueryBuilder>();
        }

        public override async Task<OperationResult> Run()
        {
            Log("Debugging...");
            using (new TimeMeasurement(x => Log($"DONE Debugging in {x}")))
            {
                var criteria = coraxQueryBuilder.BuildFromExpression<int>(t => true);

            }

            return OperationResult.Win();
        }
    }
}
