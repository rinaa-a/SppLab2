using GeneratorBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.IO;
using Lab4Spp;
using System.Runtime.Loader;

namespace Lab2Spp
{
    class Generator : IGenerator
    {
        private List<IGenerator> generators;

        public Generator()
        {
            string[] pluginPaths =
            {
                @"BasicValueTypesGenerator\bin\Debug\netstandard2.0\BasicValueTypesGenerator.dll",
                @"StringGenerator\bin\Debug\netcoreapp3.1\StringGenerator.dll",
                @"DateTimeGenerator\bin\Debug\netcoreapp3.1\DateTimeGenerator.dll"
            };

            generators = pluginPaths.SelectMany(path =>
            {
                Assembly assembly = LoadPlugin(path);
                return CreateGenerators(assembly);
            }).ToList();
        }

        static Assembly LoadPlugin(string relativePath)
        {
            // Navigate up to the solution root
            string root = Path.GetFullPath(Path.Combine(
                Path.GetDirectoryName(
                    Path.GetDirectoryName(
                        Path.GetDirectoryName(
                            Path.GetDirectoryName(
                                Path.GetDirectoryName(typeof(Program).Assembly.Location)))))));

            string pluginLocation = Path.GetFullPath(Path.Combine(root, relativePath.Replace('\\', Path.DirectorySeparatorChar)));
            Console.WriteLine($"Loading generators from: {pluginLocation}");
            PluginLoadContext loadContext = new PluginLoadContext(pluginLocation);
            return loadContext.LoadFromAssemblyName(new AssemblyName(Path.GetFileNameWithoutExtension(pluginLocation)));
        }

        static IEnumerable<IGenerator> CreateGenerators(Assembly assembly)
        {
            int count = 0;

            foreach (Type type in assembly.GetTypes())
            {

                if (typeof(IGenerator).IsAssignableFrom(type))
                {
                    IGenerator result = Activator.CreateInstance(type) as IGenerator;
                    if (result != null)
                    {
                        count++;
                        yield return result;
                    }
                }
            }

            if (count == 0)
            {
                string availableTypes = string.Join(",", assembly.GetTypes().Select(t => t.FullName));
                throw new ApplicationException(
                    $"Can't find any type which implements IGenerator in {assembly} from {assembly.Location}.\n" +
                    $"Available types: {availableTypes}");
            }
        }
        public bool CanGenerate(Type type)
        {
            foreach (var g in generators)
            {
                if(g.CanGenerate(type))
                {
                    return true;
                }
            }
            return false;
        }

        public object Generate(Type type)
        {
            foreach (var g in generators)
            {
                if (g.CanGenerate(type))
                {
                    return g.Generate(type);
                }
            }
            return null;
        }
    }
}
