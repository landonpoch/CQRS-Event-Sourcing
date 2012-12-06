using CQRS.Domain.Common;
using CQRS.Presentation.Common;
using CQRS.WriteServiceConsole.Services;
using Ninject.Modules;
using CQRS.Domain.Services;

namespace CQRS.WriteServiceConsole
{
    public class ApplicationModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IApplicationSettings>().To<ApplicationSettings>();
            Bind<ICommandHandlers>().To<CommandHandlers>();
        }
    }
}
