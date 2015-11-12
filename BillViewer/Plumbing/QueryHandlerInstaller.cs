namespace BillViewer.Plumbing
{
    using BillViewer.Core;

    using Castle.MicroKernel.Registration;
    using Castle.MicroKernel.SubSystems.Configuration;
    using Castle.Windsor;

    public class QueryHandlerInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
              Classes.FromAssemblyInThisApplication()
              .BasedOn(typeof(IQueryHandlerAsync<,>))
              .WithServiceAllInterfaces()
              .LifestylePerWebRequest());
        }
    }
}