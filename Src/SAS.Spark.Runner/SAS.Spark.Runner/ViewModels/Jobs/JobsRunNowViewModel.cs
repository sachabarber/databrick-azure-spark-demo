using Newtonsoft.Json;
using SAS.Spark.Runner.REST.DataBricks;
using SAS.Spark.Runner.REST.DataBricks.Requests;
using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SAS.Spark.Runner.ViewModels.Jobs
{
    public class JobsRunNowViewModel : INPCBase
    {
        private IMessageBoxService _messageBoxService;
        private IDatabricksWebApiClient _databricksWebApiClient;
        private string _jobJson;
        private int? _jobId;

        public JobsRunNowViewModel(
            IMessageBoxService messageBoxService,
            IDatabricksWebApiClient databricksWebApiClient)
        {
            _messageBoxService = messageBoxService;
            _databricksWebApiClient = databricksWebApiClient;
            JobsRunNowCommand = new SimpleAsyncCommand<object, object>(ExecuteJobsRunNowCommandAsync);
        }

        private async Task<object> ExecuteJobsRunNowCommandAsync(object param)
        {
            if(!_jobId.HasValue)
            {
                _messageBoxService.ShowError("You must supply 'JobId'");
                return System.Threading.Tasks.Task.FromResult<object>(null);
            }

            try
            {
                var request = new DatabricksRunNowRequest()
                {
                    JobId = _jobId.Value
                };
                var runResponse = await _databricksWebApiClient.JobsRunNowAsync(request);
                JobJson = JsonConvert.SerializeObject(runResponse, Formatting.Indented);
            }
            catch(Exception ex)
            {
                _messageBoxService.ShowError(ex.Message);
            }
            return System.Threading.Tasks.Task.FromResult<object>(null);
        }


        public string JobJson
        {
            get
            {
                return this._jobJson;
            }
            set
            {
                RaiseAndSetIfChanged(ref this._jobJson, value, () => JobJson);
            }
        }

        public int? JobId
        {
            get
            {
                return this._jobId;
            }
            set
            {
                RaiseAndSetIfChanged(ref this._jobId, value, () => JobId);
            }
        }

        public ICommand JobsRunNowCommand { get; private set; }
    }
}
