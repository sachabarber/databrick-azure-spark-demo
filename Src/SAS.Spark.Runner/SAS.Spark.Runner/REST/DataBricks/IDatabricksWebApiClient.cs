using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using SAS.Spark.Runner.REST.DataBricks.Requests;
using SAS.Spark.Runner.REST.DataBricks.Responses;

namespace SAS.Spark.Runner.REST.DataBricks
{
    public interface IDatabricksWebApiClient 
    {

        //https://docs.databricks.com/api/latest/jobs.html#create
        Task<CreateJobResponse> JobsCreateAsync(string jsonJobRequest);

        //https://docs.databricks.com/api/latest/jobs.html#jobsrunnow
        Task<DatabricksRunNowResponse> JobsRunNowAsync(DatabricksRunNowRequest runRequest);

        //https://docs.databricks.com/api/latest/jobs.html#runs-get
        Task<DatabricksRunResponse> JobsRunsGetAsync(int runId);

        //https://docs.databricks.com/api/latest/jobs.html#list
        Task<JObject> JobsListAsync();

        //https://docs.azuredatabricks.net/api/latest/clusters.html#start
        Task<DatabricksClusterStartResponse> ClustersStartAsync(string clusterId);

        //https://docs.azuredatabricks.net/api/latest/clusters.html#get
        Task<JObject> ClustersGetAsync(string clusterId);

        //https://docs.databricks.com/api/latest/clusters.html#list
        Task<ClusterListResponse> ClustersListAsync();
    }
}