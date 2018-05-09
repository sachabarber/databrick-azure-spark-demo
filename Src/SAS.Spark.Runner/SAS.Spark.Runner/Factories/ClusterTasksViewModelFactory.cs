using SAS.Spark.Runner.REST.DataBricks;
using System.Windows.Input;

namespace SAS.Spark.Runner.ViewModels.Clusters
{
    public class ClusterTasksViewModelFactory
    {
        private ClustersListViewModel _clustersListViewModel;
        private ClusterGetViewModel _clusterGetViewModel;
        private ClusterStartViewModel _clusterStartViewModel;

        public ClusterTasksViewModelFactory(
            ClustersListViewModel clustersListViewModel,
            ClusterGetViewModel clusterGetViewModel,
            ClusterStartViewModel clusterStartViewModel
            )
        {
            _clustersListViewModel = clustersListViewModel;
            _clusterGetViewModel = clusterGetViewModel;
            _clusterStartViewModel = clusterStartViewModel;
        }


        public ClusterTasksViewModel Create(MainWindowViewModel parent)
        {
            return new ClusterTasksViewModel(
                parent,
                _clustersListViewModel,
                _clusterGetViewModel,
                _clusterStartViewModel);
        }

    }
}
