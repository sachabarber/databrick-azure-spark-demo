using SAS.Spark.Runner.REST.DataBricks;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using SAS.Spark.Runner.Services;

namespace SAS.Spark.Runner.ViewModels.Clusters
{
    public class ClusterGetViewModel : INPCBase
    {
        private IMessageBoxService _messageBoxService;
        private IDatabricksWebApiClient _databricksWebApiClient;
        private string _clustersJson;
        private string _clusterId;

        public ClusterGetViewModel(
            IMessageBoxService messageBoxService,
            IDatabricksWebApiClient databricksWebApiClient)
        {
            _messageBoxService = messageBoxService;
            _databricksWebApiClient = databricksWebApiClient;
            FetchClusterCommand = new SimpleAsyncCommand<object, object>(ExecuteFetchClusterCommandAsync);
        }

        private async Task<object> ExecuteFetchClusterCommandAsync(object param)
        {
            if(string.IsNullOrEmpty(_clusterId))
            {
                _messageBoxService.ShowError("You must supply 'ClusterId'");
                return System.Threading.Tasks.Task.FromResult<object>(null);
            }

            try
            {
                var cluster = await _databricksWebApiClient.ClustersGetAsync(_clusterId);
                ClustersJson = cluster.ToString();
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

        public ICommand FetchClusterCommand { get; private set; }
    }
}
