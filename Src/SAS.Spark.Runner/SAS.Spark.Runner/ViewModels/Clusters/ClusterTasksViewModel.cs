using SAS.Spark.Runner.REST.DataBricks;
using System.Windows.Input;

namespace SAS.Spark.Runner.ViewModels.Clusters
{
    public class ClusterTasksViewModel : INPCBase
    {
        public ClusterTasksViewModel(
            MainWindowViewModel parent,
            ClustersListViewModel clustersListViewModel,
            ClusterGetViewModel clusterGetViewModel,
            ClusterStartViewModel clusterStartViewModel
            )
        {
            ClustersListViewModel = clustersListViewModel;
            ClusterGetViewModel = clusterGetViewModel;
            ClusterStartViewModel = clusterStartViewModel;
            GoBackCommand = new SimpleCommand<object, object>(x => parent.CurrentTaskViewModel = parent);
        }

        public ClustersListViewModel ClustersListViewModel { get; private set; }
        public ClusterGetViewModel ClusterGetViewModel { get; private set; }
        public ClusterStartViewModel ClusterStartViewModel { get; private set; }
        public ICommand GoBackCommand { get; private set; }
    }
}
