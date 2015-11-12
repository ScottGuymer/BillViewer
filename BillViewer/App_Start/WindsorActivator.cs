using BillViewer;

using WebActivatorEx;

[assembly: PreApplicationStartMethod(typeof(WindsorActivator), "PreStart")]
[assembly: ApplicationShutdownMethod(typeof(WindsorActivator), "Shutdown")]

namespace BillViewer
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    using Castle.Windsor;

    /// <summary>
    /// The windsor activator.
    /// </summary>
    public static class WindsorActivator
    {
        /// <summary>
        /// The bootstrapper.
        /// </summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed. Suppression is OK here.")]
        private static ContainerBootstrapper bootstrapper;

        /// <summary>
        /// Gets the container.
        /// </summary>
        /// <value>
        /// The container.
        /// </value>
        /// <exception cref="NullReferenceException">The WindsorActivator hasn't been initialized yet. Are you attempting to access the container too soon?</exception>
        public static IWindsorContainer Container
        {
            get
            {
                if (bootstrapper == null)
                {
                    throw new NullReferenceException(
                        "The WindsorActivator hasn't been initialized yet. Are you attempting to access the container too soon?");
                }

                return bootstrapper.Container;
            }
        }

        /// <summary>
        /// The pre start.
        /// </summary>
        public static void PreStart()
        {
            bootstrapper = ContainerBootstrapper.Bootstrap();
        }

        /// <summary>
        /// The shutdown.
        /// </summary>
        public static void Shutdown()
        {
            bootstrapper?.Dispose();
        }
    }
}