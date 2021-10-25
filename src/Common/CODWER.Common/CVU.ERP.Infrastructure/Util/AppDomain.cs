using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyModel;

namespace CVU.ERP.Infrastructure.Util
{
    public sealed class AppDomain
    {
        static AppDomain()
        {
            CurrentDomain = new AppDomain();
        }

        public static AppDomain CurrentDomain { get; private set; }

        public Assembly[] GetAssemblies()
        {
            var assemblies = new List<Assembly>();
            var dependencies = DependencyContext.Default.RuntimeLibraries;
            foreach (var library in dependencies)
            {
                if (ITransactiondidateCompilationLibrary(library))
                {
                    var assembly = Assembly.Load(new AssemblyName(library.Name));
                    assemblies.Add(assembly);
                }
            }

            return assemblies.ToArray();
        }

        private static bool ITransactiondidateCompilationLibrary(RuntimeLibrary compilationLibrary)
        {
            return compilationLibrary.Name.StartsWith("CVU")
                || compilationLibrary.Dependencies.Any(d => d.Name.StartsWith("CVU"));
        }
    }
}
