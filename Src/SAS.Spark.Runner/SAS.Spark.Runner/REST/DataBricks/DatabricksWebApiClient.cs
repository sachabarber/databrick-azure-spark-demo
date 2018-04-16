using System;
using System.Configuration;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using SAS.Spark.Runner.REST.DataBricks.Responses;
using RestRequest = RestSharp.Serializers.Newtonsoft.Json.RestRequest;

namespace SAS.Spark.Runner.REST.DataBricks
{
    public class DatabricksWebApiClient 
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



        private static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }


        public async Task<int> SubmitJobAsync(string jsonJobRequest)
        {
            var request = new RestSharp.Serializers.Newtonsoft.Json.RestRequest("api/2.0/jobs/create", Method.POST);
            request.AddHeader("Authorization", $"Basic {_authHeader}");
            request.AddParameter("application/json", jsonJobRequest, ParameterType.RequestBody);

            var response = await _client.ExecuteTaskAsync(request);
            dynamic responseContent = JObject.Parse(response.Content);
            var jobId = responseContent.job_id;
            return jobId;
        }

        //public async Task<int> StartRunAsync(DatabricksRunRequest runRequest)
        //{
        //    var request = new RestSharp.Serializers.Newtonsoft.Json.RestRequest("api/2.0/jobs/run-now", Method.POST);
        //    request.AddHeader("Authorization", $"Basic {_authHeader}");
        //    request.AddJsonBody(runRequest);

        //    var response = await _client.ExecuteTaskAsync(request);
        //    dynamic responseContent = JObject.Parse(response.Content);
        //    var runId = responseContent.run_id;
        //    return runId;
        //}

        public async Task<DatabricksRunResponse> GetRunStatusAsync(int runId)
        {
            var request = new RestSharp.Serializers.Newtonsoft.Json.RestRequest("api/2.0/jobs/runs/get", Method.GET);
            request.AddHeader("Authorization", $"Basic {_authHeader}");
            request.AddQueryParameter("run_id", runId.ToString());

            var response = await _client.ExecuteTaskAsync<DatabricksRunResponse>(request);
            var dbResponse = JsonConvert.DeserializeObject<DatabricksRunResponse>(response.Content);
            return dbResponse;
        }

        //public async Task<DatabricksClusterResponseAws> StartClusterAsync(string clusterId)
        //{
        //    var request = new RestSharp.Serializers.Newtonsoft.Json.RestRequest("api/2.0/clusters/start", Method.POST);
        //    request.AddHeader("Authorization", $"Basic {_authHeader}");
        //    request.AddJsonBody(new { cluster_id = clusterId });

        //    var response = await _client.ExecuteTaskAsync<DatabricksClusterResponseAws>(request);
        //    var dbResponse = JsonConvert.DeserializeObject<DatabricksClusterResponseAws>(response.Content);
        //    return dbResponse;
        //}

        //public async Task<DatabricksClusterResponseAws> GetClusterStatusAsync(string clusterId)
        //{
        //    var request = new RestSharp.Serializers.Newtonsoft.Json.RestRequest("api/2.0/clusters/get", Method.GET);
        //    request.AddHeader("Authorization", $"Basic {_authHeader}");
        //    request.AddQueryParameter("cluster_id", clusterId);

        //    var response = await _client.ExecuteTaskAsync<DatabricksClusterResponseAws>(request);
        //    var dbResponse = JsonConvert.DeserializeObject<DatabricksClusterResponseAws>(response.Content);
        //    return dbResponse;
        //}


        public async Task<ClusterListResponse> GetClusterList()
        {
            try
            {
                var request = new RestRequest("api/2.0/clusters/list", Method.GET);
                request.AddHeader("Authorization", $"Basic {_authHeader}");

                var response = await _client.ExecuteTaskAsync<ClusterListResponse>(request);
                var dbResponse = JsonConvert.DeserializeObject<ClusterListResponse>(response.Content);
                return dbResponse;
            }
            catch (Exception e)
            {

                return null;
            }
        }
    }
}