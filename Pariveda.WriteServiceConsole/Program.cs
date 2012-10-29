using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Ninject;
using Pariveda.Application.Services;
using Pariveda.Domain;
using Pariveda.Domain.Commands;
using Pariveda.Domain.Messages.Commands;
using Pariveda.WriteServiceConsole.Services;
using Topshelf;

namespace Pariveda.WriteServiceConsole
{
    public class WriteModel
    {
        public IBus _bus;
        public ICommandHandlers _handlers;

        public WriteModel(IBus bus, ICommandHandlers handlers)
        {
            _bus = bus;
            _handlers = handlers;
        }

        public void Start()
        {
            Console.WriteLine("Subscribing to transient messages");
            _bus.SubscribeHandler<CreateNewOrder, CreateNewOrderResult>(_handlers.Handle);
            _bus.SubscribeHandler<AddOrderItem, AddOrderItemResult>(_handlers.Handle);
            Console.WriteLine("Write-Model Initialized");
        }

        public void Stop()
        {
            _bus.Dispose(); // Clean up
            Console.WriteLine("Write-Model Stopped");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var kernel = RegisterModules(); // Sets up DI
            HostFactory.Run(x =>
            {
                x.Service<WriteModel>(c =>
                {
                    c.ConstructUsing(() => new WriteModel(kernel.Get<IBus>(), kernel.Get<ICommandHandlers>()));
                    c.WhenStarted(d => d.Start());
                    c.WhenStopped(d => d.Stop());
                });
                x.RunAsLocalSystem();
                x.SetDescription("WriteModel-Description");
                x.SetDisplayName("WriteModel-DisplayName");
                x.SetServiceName("WriteModel-ServiceName");
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
                .Where(a => a.FullName.Contains("Pariveda"));
            kernel.Load(assemblies);
            return kernel;
        }
    }
}
