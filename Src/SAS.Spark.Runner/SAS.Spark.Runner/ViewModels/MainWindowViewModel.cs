using SAS.Spark.Runner.REST.DataBricks;
using SAS.Spark.Runner.ViewModels.Clusters;
using SAS.Spark.Runner.ViewModels.Factories;
using SAS.Spark.Runner.ViewModels.Jobs;
using System.Windows.Input;

namespace SAS.Spark.Runner.ViewModels
{
    public class MainWindowViewModel : INPCBase
    {
        private object _currentTaskViewModel;
        private ClusterTasksViewModel _clusterTasksViewModel;
        private JobTasksViewModel _jobTasksViewModel;

        public MainWindowViewModel(
            JobTasksViewModelFactory jobTasksViewModelFactory,
            ClusterTasksViewModelFactory clusterTasksViewModelFactory
            )
        {
            _clusterTasksViewModel = clusterTasksViewModelFactory.Create(this);
            _jobTasksViewModel = jobTasksViewModelFactory.Create(this);

            ClusterCommand = new SimpleCommand<object, object>(x => CurrentTaskViewModel = _clusterTasksViewModel);
            JobsCommand = new SimpleCommand<object, object>(x => CurrentTaskViewModel = _jobTasksViewModel);

            CurrentTaskViewModel = this;
        }

        public ICommand ClusterCommand { get; private set; }
        public ICommand JobsCommand { get; private set; }

        public object CurrentTaskViewModel
        {
            get
            {
                return this._currentTaskViewModel;
            }
            set
            {
                RaiseAndSetIfChanged(ref this._currentTaskViewModel, value, () => CurrentTaskViewModel);
            }
        }

    }
}
