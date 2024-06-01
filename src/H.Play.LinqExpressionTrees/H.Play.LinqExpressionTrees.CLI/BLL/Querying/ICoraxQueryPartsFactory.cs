using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Reflection;

namespace H.Play.LinqExpressionTrees.CLI.BLL.Querying
{
    public interface ICoraxQueryPartsFactory
    {
        ICoraxQueryOperator Operator(string symbol);


        public static ICoraxQueryPartsFactory New() => new Concrete();
        private class Concrete : ICoraxQueryPartsFactory
        {
            static readonly ConcurrentDictionary<Assembly, ICoraxQueryOperator[]> operatorsPerAssemblyDictionary = new ConcurrentDictionary<Assembly, ICoraxQueryOperator[]>();

            public ICoraxQueryOperator Operator(string symbol)
            {
                var assemblyToCheck = Assembly.GetExecutingAssembly();
                var assemblyOperators = operatorsPerAssemblyDictionary.GetOrAdd(assemblyToCheck, GetAllConcreteOperatorsInAssembly);

                return
                    assemblyOperators
                    .LastOrDefault(op =>
                        op.Symbol == symbol
                        || (op.SymbolAliases?.Any(alias => alias == symbol) == true)
                    )
                    ;
            }


            private static ICoraxQueryOperator[] GetAllConcreteOperatorsInAssembly(Assembly assemblyToCheck)
            {
                var types = GetAllImplementationTypes(assemblyToCheck, typeof(ICoraxQueryOperator));
                if(!types.Any())
                    return Array.Empty<ICoraxQueryOperator>();

                return
                    types
                    .Select(GetCoraxQueryOperatorInstance)
                    .ToArray()
                    ;
            }

            private static ICoraxQueryOperator GetCoraxQueryOperatorInstance(Type coraxQueryOperatorType)
            {
                var instanceProperty
                    = coraxQueryOperatorType
                    .GetProperties(BindingFlags.Public | BindingFlags.Static)
                    .FirstOrDefault(p => p.CanRead && typeof(ICoraxQueryOperator).IsAssignableFrom(p.PropertyType))
                    ;

                if (instanceProperty != null)
                    return instanceProperty.GetValue(null) as ICoraxQueryOperator;

                var instanceField
                    = coraxQueryOperatorType
                    .GetFields(BindingFlags.Public | BindingFlags.Static)
                    .FirstOrDefault(f => typeof(ICoraxQueryOperator).IsAssignableFrom(f.FieldType))
                    ;

                if (instanceField != null)
                    return instanceField.GetValue(null) as ICoraxQueryOperator;

                return
                    Activator.CreateInstance(coraxQueryOperatorType) as ICoraxQueryOperator;
            }

            private static Type[] GetAllImplementationTypes(Assembly assemblyToCheck, Type baseType)
            {
                return
                    assemblyToCheck
                    .GetTypes()
                    .Where(
                        p =>
                        p != baseType
                        && baseType.IsAssignableFrom(p)
                        && !p.IsAbstract
                    )
                    .ToArray();
            }
        }
    }
}
