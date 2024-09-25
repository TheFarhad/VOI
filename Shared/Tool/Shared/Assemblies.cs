namespace Tool.Shared;

using System.Linq;
using System.Reflection;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyModel;

public class Assemblies
{
    public static List<Assembly> Get(params string[] assemblies)
    {
        List<Assembly> result = [];
        DependencyContext
            .Default?
            .RuntimeLibraries?
            .ToList()
            .ForEach(item =>
            {
                if (IsCandidateCompilationLibrary(item, assemblies))
                {
                    var assembly = Assembly.Load(new AssemblyName(item.Name));
                    result.Add(assembly);
                }
            });
        return result;
    }

    private static bool IsCandidateCompilationLibrary(RuntimeLibrary compilationLibrary, string[] assmblyName)
        => assmblyName
            .Any(d => compilationLibrary.Name.Contains(d)) ||
        compilationLibrary.Dependencies.Any(d => assmblyName.Any(c => d.Name.Contains(c)));

}
