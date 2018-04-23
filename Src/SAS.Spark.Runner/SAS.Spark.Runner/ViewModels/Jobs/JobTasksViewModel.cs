using System.Windows.Input;

namespace SAS.Spark.Runner.ViewModels.Jobs
{
    public class JobTasksViewModel : INPCBase
    {
        public JobTasksViewModel(MainWindowViewModel parent)
        {
            GoBackCommand = new SimpleCommand<object, object>(x => parent.CurrentTaskViewModel = parent);
        }

        public ICommand GoBackCommand { get; private set; }
    }
}
