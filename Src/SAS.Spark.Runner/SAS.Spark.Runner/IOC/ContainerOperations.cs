using Autofac;
using SAS.Spark.Runner.REST.DataBricks;
using SAS.Spark.Runner.ViewModels;
using SAS.Spark.Runner.ViewModels.Clusters;
using SAS.Spark.Runner.ViewModels.Factories;
using SAS.Spark.Runner.ViewModels.Jobs;
using System;
using SAS.Spark.Runner.Services;


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

            //services
            builder.RegisterType<DatabricksWebApiClient>().As<IDatabricksWebApiClient>().SingleInstance();
            builder.RegisterType<WPFMessageBoxService>().As<IMessageBoxService>().SingleInstance();
            builder.RegisterType<WPFOpenFileService>().As<IOpenFileService>().SingleInstance();
            builder.RegisterType<DataBricksFileUploadService>().As<IDataBricksFileUploadService>().SingleInstance();

            builder.RegisterType<MainWindowViewModel>();

            //clusters
            builder.RegisterType<ClusterTasksViewModelFactory>();
            builder.RegisterType<ClusterGetViewModel>();
            builder.RegisterType<ClustersListViewModel>();
            builder.RegisterType<ClusterStartViewModel>();
            
            //jobs
            builder.RegisterType<JobTasksViewModelFactory>();
            builder.RegisterType<JobsListViewModel>();
            builder.RegisterType<JobsRunsGetViewModel>();
            builder.RegisterType<JobsRunNowViewModel>();
            builder.RegisterType<JobsPickAndRunJarViewModel>();

            return builder.Build();
        }
    }
}
