using Newtonsoft.Json;
using SAS.Spark.Runner.REST.DataBricks;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using SAS.Spark.Runner.Services;

namespace SAS.Spark.Runner.ViewModels.Clusters
{
    public class ClusterStartViewModel : INPCBase
    {
        private IMessageBoxService _messageBoxService;
        private IDatabricksWebApiClient _databricksWebApiClient;
        private string _clustersJson;
        private string _clusterId;

        public ClusterStartViewModel(
            IMessageBoxService messageBoxService,
            IDatabricksWebApiClient databricksWebApiClient)
        {
            _messageBoxService = messageBoxService;
            _databricksWebApiClient = databricksWebApiClient;
            StartClusterCommand = new SimpleAsyncCommand<object, object>(ExecuteStartClusterCommandAsync);
        }

        private async Task<object> ExecuteStartClusterCommandAsync(object param)
        {
            if(string.IsNullOrEmpty(_clusterId))
            {
                _messageBoxService.ShowError("You must supply 'ClusterId'");
                return System.Threading.Tasks.Task.FromResult<object>(null);
            }

            try
            {
                var clusterResponse = await _databricksWebApiClient.ClustersStartAsync(_clusterId);
                ClustersJson = JsonConvert.SerializeObject(clusterResponse, Formatting.Indented);
            }
            catch(Exception ex)
            {
                _messageBoxService.ShowError(ex.Message);
            }
            return System.Threading.Tasks.Task.FromResult<object>(null);
        }


        public string ClustersJson
        {
            get
            {
                return this._clustersJson;
            }
            set
            {
                RaiseAndSetIfChanged(ref this._clustersJson, value, () => ClustersJson);
            }
        }

        public string ClusterId
        {
            get
            {
                return this._clusterId;
            }
            set
            {
                RaiseAndSetIfChanged(ref this._clusterId, value, () => ClusterId);
            }
        }

        public ICommand StartClusterCommand { get; private set; }
    }
}
