using Newtonsoft.Json;
using SAS.Spark.Runner.REST.DataBricks;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using SAS.Spark.Runner.Services;

namespace SAS.Spark.Runner.ViewModels.Jobs
{
    public class JobsRunsGetViewModel : INPCBase
    {
        private IMessageBoxService _messageBoxService;
        private IDatabricksWebApiClient _databricksWebApiClient;
        private string _runJson;
        private int? _runId;

        public JobsRunsGetViewModel(
            IMessageBoxService messageBoxService,
            IDatabricksWebApiClient databricksWebApiClient)
        {
            _messageBoxService = messageBoxService;
            _databricksWebApiClient = databricksWebApiClient;
            JobsRunsGetCommand = new SimpleAsyncCommand<object, object>(ExecuteJobsRunsGetCommandAsync);
        }

        private async Task<object> ExecuteJobsRunsGetCommandAsync(object param)
        {
            if(!_runId.HasValue)
            {
                _messageBoxService.ShowError("You must supply 'RunId'");
                return System.Threading.Tasks.Task.FromResult<object>(null);
            }

            try
            {
                var runResponse = await _databricksWebApiClient.JobsRunsGetAsync(_runId.Value);
                RunJson = JsonConvert.SerializeObject(runResponse, Formatting.Indented);
            }
            catch(Exception ex)
            {
                _messageBoxService.ShowError(ex.Message);
            }
            return System.Threading.Tasks.Task.FromResult<object>(null);
        }


        public string RunJson
        {
            get
            {
                return this._runJson;
            }
            set
            {
                RaiseAndSetIfChanged(ref this._runJson, value, () => RunJson);
            }
        }

        public int? RunId
        {
            get
            {
                return this._runId;
            }
            set
            {
                RaiseAndSetIfChanged(ref this._runId, value, () => RunId);
            }
        }

        public ICommand JobsRunsGetCommand { get; private set; }
    }
}
