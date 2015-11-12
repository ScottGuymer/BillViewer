namespace BillViewer.Plumbing
{
    using System.Web.Mvc;

    using Castle.MicroKernel.Registration;
    using Castle.MicroKernel.SubSystems.Configuration;
    using Castle.Windsor;
    using Castle.Windsor.Mvc;

    public class ControllersInstaller : IWindsorInstaller
    {
        /// <summary>
        /// Performs the installation in the <see cref="T:Castle.Windsor.IWindsorContainer" />.
        /// </summary>
        /// <param name="container">The container.</param>
        /// <param name="store">The configuration store.</param>
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Classes.FromThisAssembly()
                    .BasedOn<IController>()
                    .If(c => c.Name.EndsWith("Controller"))
                    .LifestylePerWebRequest());

            ControllerBuilder.Current.SetControllerFactory(new WindsorControllerFactory(container.Kernel));
        }
    }
}