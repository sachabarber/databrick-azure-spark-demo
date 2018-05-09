using SAS.Spark.Runner.REST.DataBricks;
using SAS.Spark.Runner.ViewModels.Jobs;
using System.Windows.Input;

namespace SAS.Spark.Runner.ViewModels.Factories
{
    public class JobTasksViewModelFactory
    {
        public JobTasksViewModelFactory(
            JobsListViewModel jobsListViewModel,
            JobsRunsGetViewModel jobsRunsGetViewModel,
            JobsRunNowViewModel jobsRunNowViewModel,
            JobsPickAndRunJarViewModel jobsPickAndRunJarViewModel
          )
        {
            _jobsListViewModel = jobsListViewModel;
            _jobsRunsGetViewModel = jobsRunsGetViewModel;
            _jobsRunNowViewModel = jobsRunNowViewModel;
            _jobsPickAndRunJarViewModel = jobsPickAndRunJarViewModel;
        }

        public JobTasksViewModel Create(MainWindowViewModel parent)
        {
            return new JobTasksViewModel(
                parent, 
                _jobsListViewModel,
                _jobsRunsGetViewModel, 
                _jobsRunNowViewModel, 
                _jobsPickAndRunJarViewModel);
        }
      
        private JobsListViewModel _jobsListViewModel;
        private JobsRunsGetViewModel _jobsRunsGetViewModel;
        private JobsRunNowViewModel _jobsRunNowViewModel;
        private JobsPickAndRunJarViewModel _jobsPickAndRunJarViewModel;


    }
}
