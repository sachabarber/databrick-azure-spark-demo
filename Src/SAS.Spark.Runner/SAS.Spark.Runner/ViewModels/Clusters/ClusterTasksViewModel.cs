using SAS.Spark.Runner.REST.DataBricks;
using System.Windows.Input;

namespace SAS.Spark.Runner.ViewModels.Clusters
{
    public class ClusterTasksViewModel : INPCBase
    {
        public ClusterTasksViewModel(
            MainWindowViewModel parent,
            IMessageBoxService messageBoxService,
            IDatabricksWebApiClient databricksWebApiClient
            )
        {
            GoBackCommand = new SimpleCommand<object, object>(x => parent.CurrentTaskViewModel = parent);
            ClustersListViewModel = new ClustersListViewModel(messageBoxService, databricksWebApiClient);
        }

        public ICommand GoBackCommand { get; private set; }

        public ClustersListViewModel ClustersListViewModel { get; private set; }
    }
}
