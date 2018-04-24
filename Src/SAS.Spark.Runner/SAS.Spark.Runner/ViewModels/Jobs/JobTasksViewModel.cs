using SAS.Spark.Runner.REST.DataBricks;
using System.Windows.Input;
          
namespace SAS.Spark.Runner.ViewModels.Jobs
{
    public class JobTasksViewModel : INPCBase
    {
        public JobTasksViewModel(
          MainWindowViewModel parent,
          IMessageBoxService messageBoxService,
          IDatabricksWebApiClient databricksWebApiClient
          )
        {
            GoBackCommand = new SimpleCommand<object, object>(x => parent.CurrentTaskViewModel = parent);
            JobsListViewModel = new JobsListViewModel(messageBoxService, databricksWebApiClient);
            JobsRunsGetViewModel = new JobsRunsGetViewModel(messageBoxService, databricksWebApiClient);
            JobsRunNowViewModel = new JobsRunNowViewModel(messageBoxService, databricksWebApiClient);
        }

        public ICommand GoBackCommand { get; private set; }
        public JobsListViewModel JobsListViewModel { get; private set; }
        public JobsRunsGetViewModel JobsRunsGetViewModel { get; private set; }
        public JobsRunNowViewModel JobsRunNowViewModel { get; private set; }
    }
}
