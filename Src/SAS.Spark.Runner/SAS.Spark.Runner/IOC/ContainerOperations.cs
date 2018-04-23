using Autofac;
using Autofac.Core;
using SAS.Spark.Runner.REST.DataBricks;
using SAS.Spark.Runner.ViewModels;
using System;


namespace SAS.Spark.Runner.IOC
{
    public class ContainerOperations
    {
        private static Lazy<IContainer> _container = new Lazy<IContainer>(() => BuildContainer());

        private ContainerOperations()
        {

        }

        public static IContainer Container => _container.Value;

        private static IContainer BuildContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<MainWindowViewModel>();
            builder.RegisterType<DatabricksWebApiClient>().As<IDatabricksWebApiClient>().SingleInstance();
            builder.RegisterType<WPFMessageBoxService>().As<IMessageBoxService>().SingleInstance();
            return builder.Build();
        }
    }
}
