using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using MassTransit;
using Ninject;
using Topshelf;

namespace CQRS.ReadModel
{
    public class ReadModel
    {
        private IServiceBus _bus;

        public ReadModel(IServiceBus bus)
        {
            _bus = bus;
        }
        
        public void Start()
        {
            Console.WriteLine("Read-Model Initialized");
        }

        public void Stop()
        {
            _bus.Dispose(); // Clean up
            Console.WriteLine("Read-Model Stopped");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var kernel = RegisterModules();
            HostFactory.Run(x =>
            {
                x.Service<ReadModel>(c =>
                {
                    c.ConstructUsing(() => new ReadModel(kernel.Get<IServiceBus>()));
                    c.WhenStarted(d => d.Start());
                    c.WhenStopped(d => d.Stop());
                });
                x.RunAsLocalSystem();
                x.SetDescription("ReadModel-Description");
                x.SetDisplayName("ReadModel-DisplayName");
                x.SetServiceName("ReadModel-ServiceName");
            });
        }

        private static IKernel RegisterModules()
        {
            var loadedAssemblies = AppDomain.CurrentDomain.GetAssemblies().ToList();
            var loadedPaths = loadedAssemblies.Select(a => a.Location).ToArray();

            var referencedPaths = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.dll");
            var toLoad = referencedPaths.Where(r => !loadedPaths.Contains(r, StringComparer.InvariantCultureIgnoreCase)).ToList();
            toLoad.ForEach(path => loadedAssemblies.Add(AppDomain.CurrentDomain.Load(AssemblyName.GetAssemblyName(path))));

            var kernel = new StandardKernel();
            IEnumerable<Assembly> assemblies = loadedAssemblies
                .ToList()
                .Where(a => a.FullName.Contains("CQRS"));
            kernel.Load(assemblies);
            return kernel;
        }
    }
}
