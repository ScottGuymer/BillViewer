namespace BillViewer
{
    using System;

    using Castle.Facilities.Logging;
    using Castle.Services.Logging.NLogIntegration;
    using Castle.Windsor;
    using Castle.Windsor.Installer;

    /// <summary>
    /// The container bootstrap.
    /// </summary>
    public class ContainerBootstrapper : IContainerAccessor, IDisposable
    {
        /// <summary>
        /// The container.
        /// </summary>
        private readonly IWindsorContainer container;

        /// <summary>
        /// Initializes a new instance of the <see cref="ContainerBootstrapper"/> class.
        /// </summary>
        /// <param name="container">The container.</param>
        private ContainerBootstrapper(IWindsorContainer container)
        {
            this.container = container;
        }

        /// <summary>
        /// Gets the container.
        /// </summary>
        /// <value>
        /// The container.
        /// </value>
        public IWindsorContainer Container => this.container;

        /// <summary>
        /// The bootstrap.
        /// </summary>
        /// <returns>
        /// The <see cref="ContainerBootstrapper"/>.
        /// </returns>
        public static ContainerBootstrapper Bootstrap()
        {
            var container = new WindsorContainer()
                .AddFacility<LoggingFacility>(f => f.LogUsing<NLogFactory>().WithAppConfig())              
                .Install(FromAssembly.InThisApplication());
            return new ContainerBootstrapper(container);
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            this.Container.Dispose();
        }
    }
}