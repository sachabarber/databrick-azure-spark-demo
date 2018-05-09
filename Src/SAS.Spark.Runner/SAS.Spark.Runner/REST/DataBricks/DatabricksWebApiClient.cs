using System;
using System.Configuration;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using SAS.Spark.Runner.REST.DataBricks.Requests;
using SAS.Spark.Runner.REST.DataBricks.Responses;
using RestRequest = RestSharp.Serializers.Newtonsoft.Json.RestRequest;

namespace SAS.Spark.Runner.REST.DataBricks
{
    public class DatabricksWebApiClient : IDatabricksWebApiClient
    {
        private readonly string _baseUrl;
        private readonly string _authHeader;
        private readonly RestClient _client;

        public DatabricksWebApiClient()
        {
            _baseUrl = ConfigurationManager.AppSettings["BaseUrl"];
            _authHeader = Base64Encode(App.AccessToken);
            _client = new RestClient(_baseUrl);
        }

        //https://docs.databricks.com/api/latest/jobs.html#create
        public async Task<CreateJobResponse> JobsCreateAsync(string jsonJobRequest)
        {
            var request = new RestSharp.Serializers.Newtonsoft.Json.RestRequest("api/2.0/jobs/create", Method.POST);
            request.AddHeader("Authorization", $"Basic {_authHeader}");
            request.AddParameter("application/json", jsonJobRequest, ParameterType.RequestBody);
            var response = await _client.ExecuteTaskAsync<CreateJobResponse>(request);
            var dbResponse = JsonConvert.DeserializeObject<CreateJobResponse>(response.Content);
            return dbResponse;
        }

        //https://docs.databricks.com/api/latest/jobs.html#jobsrunnow
        public async Task<DatabricksRunNowResponse> JobsRunNowAsync(DatabricksRunNowRequest runRequest)
        {
            var request = new RestRequest("api/2.0/jobs/run-now", Method.POST);
            request.AddHeader("Authorization", $"Basic {_authHeader}");
            request.AddJsonBody(runRequest);
            var response = await _client.ExecuteTaskAsync<DatabricksRunNowResponse>(request);
            var dbResponse = JsonConvert.DeserializeObject<DatabricksRunNowResponse>(response.Content);
            return dbResponse;
        }

        //https://docs.databricks.com/api/latest/jobs.html#runs-get
        public async Task<DatabricksRunResponse> JobsRunsGetAsync(int runId)
        {
            var request = new RestRequest("api/2.0/jobs/runs/get", Method.GET);
            request.AddHeader("Authorization", $"Basic {_authHeader}");
            request.AddQueryParameter("run_id", runId.ToString());
            var response = await _client.ExecuteTaskAsync<DatabricksRunResponse>(request);
            var dbResponse = JsonConvert.DeserializeObject<DatabricksRunResponse>(response.Content);
            return dbResponse;
        }

        //https://docs.azuredatabricks.net/api/latest/jobs.html#runs-submit
        public async Task<DatabricksRunNowResponse> JobsRunsSubmitJarTaskAsync(RunsSubmitJarTaskRequest runsSubmitJarTaskRequest)
        {
            var request = new RestRequest("2.0/jobs/runs/submit", Method.POST);
            request.AddHeader("Authorization", $"Basic {_authHeader}");
            request.AddJsonBody(runsSubmitJarTaskRequest);
            var response = await _client.ExecuteTaskAsync<DatabricksRunNowResponse>(request);
            var dbResponse = JsonConvert.DeserializeObject<DatabricksRunNowResponse>(response.Content);
            return dbResponse;
        }

        //https://docs.databricks.com/api/latest/jobs.html#list
        public async Task<JObject> JobsListAsync()
        {
            var request = new RestSharp.Serializers.Newtonsoft.Json.RestRequest("api/2.0/jobs/list", Method.GET);
            request.AddHeader("Authorization", $"Basic {_authHeader}");

            var response = await _client.ExecuteTaskAsync(request);
            JObject responseContent = JObject.Parse(response.Content);
            return responseContent;
        }

        //https://docs.azuredatabricks.net/api/latest/clusters.html#start
        public async Task<DatabricksClusterStartResponse> ClustersStartAsync(string clusterId)
        {
            var request = new RestSharp.Serializers.Newtonsoft.Json.RestRequest("api/2.0/clusters/start", Method.POST);
            request.AddHeader("Authorization", $"Basic {_authHeader}");
            request.AddJsonBody(new { cluster_id = clusterId });

            var response = await _client.ExecuteTaskAsync<DatabricksClusterStartResponse>(request);
            var dbResponse = JsonConvert.DeserializeObject<DatabricksClusterStartResponse>(response.Content);
            return dbResponse;
        }

        //https://docs.azuredatabricks.net/api/latest/clusters.html#get
        public async Task<JObject> ClustersGetAsync(string clusterId)
        {
            var request = new RestSharp.Serializers.Newtonsoft.Json.RestRequest("api/2.0/clusters/get", Method.GET);
            request.AddHeader("Authorization", $"Basic {_authHeader}");
            request.AddQueryParameter("cluster_id", clusterId);

            var response = await _client.ExecuteTaskAsync(request);
            JObject responseContent = JObject.Parse(response.Content);
            return responseContent;
        }

        //https://docs.databricks.com/api/latest/clusters.html#list
        public async Task<ClusterListResponse> ClustersListAsync()
        {
            var request = new RestRequest("api/2.0/clusters/list", Method.GET);
            request.AddHeader("Authorization", $"Basic {_authHeader}");

            var response = await _client.ExecuteTaskAsync<ClusterListResponse>(request);
            var dbResponse = JsonConvert.DeserializeObject<ClusterListResponse>(response.Content);
            return dbResponse;
        }


        //https://docs.azuredatabricks.net/api/latest/dbfs.html#list
        public async Task<DbfsListResponse> DbfsListAsync()
        {
            var request = new RestRequest("api/2.0/dbfs/list", Method.GET);
            request.AddHeader("Authorization", $"Basic {_authHeader}");
            request.AddQueryParameter("path", "/");

            var response = await _client.ExecuteTaskAsync<DbfsListResponse>(request);
            var dbResponse = JsonConvert.DeserializeObject<DbfsListResponse>(response.Content);
            return dbResponse;
        }

        //https://docs.azuredatabricks.net/api/latest/dbfs.html#put
        public async Task<JObject> DbfsPutAsync(FileInfo file)
        {
            var request = new RestRequest("api/2.0/dbfs/put", Method.POST);
            request.AddHeader("Authorization", $"Basic {_authHeader}");
            request.AddFile("back", file.FullName);
            request.AddHeader("Content -Type", "multipart/form-data");

            var response = await _client.ExecuteTaskAsync(request);
            JObject responseContent = JObject.Parse(response.Content);
            return responseContent;
        }

        //https://docs.azuredatabricks.net/api/latest/dbfs.html#dbfsdbfsservicecreate
        public async Task<DatabricksDbfsCreateResponse> DbfsCreateAsync(DatabricksDbfsCreateRequest dbfsRequest)
        {
            var request = new RestRequest("api/2.0/dbfs/create", Method.POST);
            request.AddHeader("Authorization", $"Basic {_authHeader}");
            request.AddJsonBody(dbfsRequest);

            var response = await _client.ExecuteTaskAsync<DatabricksDbfsCreateResponse>(request);
            var dbResponse = JsonConvert.DeserializeObject<DatabricksDbfsCreateResponse>(response.Content);
            return dbResponse;
        }

        //https://docs.azuredatabricks.net/api/latest/dbfs.html#dbfsdbfsserviceaddblock
        public async Task<JObject> DbfsAddBlockAsync(DatabricksDbfsAddBlockRequest dbfsRequest)
        {
            var request = new RestRequest("api/2.0/dbfs/add-block", Method.POST);
            request.AddHeader("Authorization", $"Basic {_authHeader}");
            request.AddJsonBody(dbfsRequest);

            var response = await _client.ExecuteTaskAsync(request);
            JObject responseContent = JObject.Parse(response.Content);
            return responseContent;
        }

        //https://docs.azuredatabricks.net/api/latest/dbfs.html#close
        public async Task<JObject> DbfsCloseAsync(DatabricksDbfsCloseRequest dbfsRequest)
        {
            var request = new RestRequest("api/2.0/dbfs/close", Method.POST);
            request.AddHeader("Authorization", $"Basic {_authHeader}");
            request.AddJsonBody(dbfsRequest);

            var response = await _client.ExecuteTaskAsync(request);
            JObject responseContent = JObject.Parse(response.Content);
            return responseContent;
        }

        private static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return Convert.ToBase64String(plainTextBytes);
        }
    }
}