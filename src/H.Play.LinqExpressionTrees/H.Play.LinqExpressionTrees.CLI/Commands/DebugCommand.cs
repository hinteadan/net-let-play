using H.Necessaire;
using H.Necessaire.Runtime.CLI.Commands;
using H.Play.LinqExpressionTrees.CLI.BLL.Querying.Operators;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace H.Play.LinqExpressionTrees.CLI.Commands
{
    internal class DebugCommand : CommandBase
    {
        public override async Task<OperationResult> Run()
        {
            Log("Debugging...");
            using (new TimeMeasurement(x => Log($"DONE Debugging in {x}")))
            {
                var equals = CoraxEqualsQueryOperator.Instance;

                int t = 42;
                Expression<Func<int, bool>> expression = x => x == t;


                //TODO: Do stuff here


            }

            return OperationResult.Win();
        }
    }
}
