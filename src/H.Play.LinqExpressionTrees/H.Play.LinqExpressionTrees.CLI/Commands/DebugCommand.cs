using H.Necessaire;
using H.Necessaire.Runtime.CLI.Commands;
using H.Play.LinqExpressionTrees.CLI.BLL.Querying;
using H.Play.LinqExpressionTrees.CLI.BLL.Querying.Concrete;
using System;
using System.Collections.Generic;
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
                //var a =coraxQueryBuilder.BuildFromExpression<int>(t => true);
                //var b = coraxQueryBuilder.BuildFromExpression<DateTime>(t => t == DateTime.UtcNow);
                //var c = coraxQueryBuilder.BuildFromExpression<DateTime>(t => t.Date.Minute == 1);
                //var d = coraxQueryBuilder.BuildFromExpression<DateTime>(t => t.Date.Minute == 1 && (t.Date.DayOfWeek >= DayOfWeek.Monday || t.Date.Month > 3));
                //var e = coraxQueryBuilder.BuildFromExpression<DateTime>(t => t.Date.Minute.Print() == "");
                var f = coraxQueryBuilder.BuildFromExpression<DateTime>(t => Math.Round((decimal)t.Date.Minute, 2) == 1);

                foreach(var deco in f.Target.Decorations)
                {

                }

                var x = coraxQueryBuilder.Explicit.Simple("Hintee", ">", (CoraxExplicitQueryValue<string>)("test", true), new Dictionary<string, object> { { "tt", 1 } });
            }

            return OperationResult.Win();
        }
    }
}
