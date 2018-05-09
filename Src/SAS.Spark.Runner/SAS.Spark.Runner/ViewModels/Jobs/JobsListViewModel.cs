using Newtonsoft.Json;
using SAS.Spark.Runner.REST.DataBricks;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using SAS.Spark.Runner.Services;

namespace SAS.Spark.Runner.ViewModels.Jobs
{
    public class JobsListViewModel : INPCBase
    {
        private IMessageBoxService _messageBoxService;
        private IDatabricksWebApiClient _databricksWebApiClient;
        private string _jobsJson;

        public JobsListViewModel(
            IMessageBoxService messageBoxService,
            IDatabricksWebApiClient databricksWebApiClient)
        {
            _messageBoxService = messageBoxService;
            _databricksWebApiClient = databricksWebApiClient;
            FetchJobListCommand = new SimpleAsyncCommand<object, object>(ExecuteFetchJobListCommandAsync);
        }

        private async Task<object> ExecuteFetchJobListCommandAsync(object param)
        {
            try
            {
                var allJobs = await _databricksWebApiClient.JobsListAsync();
                JobsJson = allJobs.ToString();
            }
            catch(Exception ex)
            {
                _messageBoxService.ShowError(ex.Message);
            }
            return System.Threading.Tasks.Task.FromResult<object>(null);
        }


        public string JobsJson
        {
            get
            {
                return this._jobsJson;
            }
            set
            {
                RaiseAndSetIfChanged(ref this._jobsJson, value, () => JobsJson);
            }
        }

        public ICommand FetchJobListCommand { get; private set; }
    }
}
