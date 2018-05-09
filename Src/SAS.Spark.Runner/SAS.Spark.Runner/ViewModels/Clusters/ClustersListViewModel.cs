using Newtonsoft.Json;
using SAS.Spark.Runner.REST.DataBricks;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using SAS.Spark.Runner.Services;

namespace SAS.Spark.Runner.ViewModels.Clusters
{
    public class ClustersListViewModel : INPCBase
    {
        private IMessageBoxService _messageBoxService;
        private IDatabricksWebApiClient _databricksWebApiClient;
        private string _clustersJson;

        public ClustersListViewModel(
            IMessageBoxService messageBoxService,
            IDatabricksWebApiClient databricksWebApiClient)
        {
            _messageBoxService = messageBoxService;
            _databricksWebApiClient = databricksWebApiClient;
            FetchClusterListCommand = new SimpleAsyncCommand<object, object>(ExecuteFetchClusterListCommandAsync);
        }

        private async Task<object> ExecuteFetchClusterListCommandAsync(object param)
        {
            try
            {
                var allClusters = await _databricksWebApiClient.ClustersListAsync();
                ClustersJson =  JsonConvert.SerializeObject(allClusters, Formatting.Indented);
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

        public ICommand FetchClusterListCommand { get; private set; }
    }
}
