using SAS.Spark.Runner.REST.DataBricks;
using SAS.Spark.Runner.ViewModels.Clusters;
using SAS.Spark.Runner.ViewModels.Jobs;
using System.Windows.Input;

namespace SAS.Spark.Runner.ViewModels
{
    public class MainWindowViewModel : INPCBase
    {
        private object _currentTaskViewModel;
        private ClusterTasksViewModel _clusterTasksViewModel;
        private JobTasksViewModel _jobTasksViewModel;

        private IMessageBoxService _messageBoxService;
        private IDatabricksWebApiClient _databricksWebApiClient;


        public MainWindowViewModel(
            IMessageBoxService messageBoxService,
            IDatabricksWebApiClient databricksWebApiClient

            )
        {
            _messageBoxService = messageBoxService;
            _databricksWebApiClient = databricksWebApiClient;

            _clusterTasksViewModel = new ClusterTasksViewModel(this, messageBoxService, databricksWebApiClient);
            _jobTasksViewModel = new JobTasksViewModel(this);

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
