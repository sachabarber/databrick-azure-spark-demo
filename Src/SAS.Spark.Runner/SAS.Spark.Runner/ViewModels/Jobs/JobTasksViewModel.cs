using System.Windows.Input;
          
namespace SAS.Spark.Runner.ViewModels.Jobs
{
    public class JobTasksViewModel : INPCBase
    {
        public JobTasksViewModel(
            MainWindowViewModel parent,
            JobsListViewModel jobsListViewModel,
            JobsRunsGetViewModel jobsRunsGetViewModel,
            JobsRunNowViewModel jobsRunNowViewModel,
            JobsPickAndRunJarViewModel jobsPickAndRunJarViewModel
          )
        {
            JobsListViewModel = jobsListViewModel;
            JobsRunsGetViewModel = jobsRunsGetViewModel;
            JobsRunNowViewModel = jobsRunNowViewModel;
            JobsPickAndRunJarViewModel = jobsPickAndRunJarViewModel;
            GoBackCommand = new SimpleCommand<object, object>(x => parent.CurrentTaskViewModel = parent);
        }

        public JobsListViewModel JobsListViewModel { get; private set; }
        public JobsRunsGetViewModel JobsRunsGetViewModel { get; private set; }
        public JobsRunNowViewModel JobsRunNowViewModel { get; private set; }
        public JobsPickAndRunJarViewModel JobsPickAndRunJarViewModel { get; private set; }
        public ICommand GoBackCommand { get; private set; }
    }
}
