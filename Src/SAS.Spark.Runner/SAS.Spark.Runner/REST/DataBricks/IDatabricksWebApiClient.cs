using Newtonsoft.Json.Linq;
using SAS.Spark.Runner.REST.DataBricks.Requests;
using SAS.Spark.Runner.REST.DataBricks.Responses;
using System.IO;
using System.Threading.Tasks;

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

        //https://docs.azuredatabricks.net/api/latest/jobs.html#runs-submit
        Task<DatabricksRunNowResponse> JobsRunsSubmitJarTaskAsync(RunsSubmitJarTaskRequest runsSubmitJarTaskRequest);

        //https://docs.azuredatabricks.net/api/latest/clusters.html#start
        Task<DatabricksClusterStartResponse> ClustersStartAsync(string clusterId);

        //https://docs.azuredatabricks.net/api/latest/clusters.html#get
        Task<JObject> ClustersGetAsync(string clusterId);

        //https://docs.databricks.com/api/latest/clusters.html#list
        Task<ClusterListResponse> ClustersListAsync();

        //https://docs.azuredatabricks.net/api/latest/dbfs.html#list
        Task<DbfsListResponse> DbfsListAsync();

        //https://docs.azuredatabricks.net/api/latest/dbfs.html#put
        Task<JObject> DbfsPutAsync(FileInfo file);

        //https://docs.azuredatabricks.net/api/latest/dbfs.html#dbfsdbfsservicecreate
        Task<DatabricksDbfsCreateResponse> DbfsCreateAsync(DatabricksDbfsCreateRequest dbfsRequest);

        //https://docs.azuredatabricks.net/api/latest/dbfs.html#dbfsdbfsserviceaddblock
        Task<JObject> DbfsAddBlockAsync(DatabricksDbfsAddBlockRequest dbfsRequest);

        //https://docs.azuredatabricks.net/api/latest/dbfs.html#close
        Task<JObject> DbfsCloseAsync(DatabricksDbfsCloseRequest dbfsRequest);

        


    }
}