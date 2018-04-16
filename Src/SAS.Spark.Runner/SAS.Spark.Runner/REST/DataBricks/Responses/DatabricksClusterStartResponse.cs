using Newtonsoft.Json;

namespace SAS.Spark.Runner.REST.DataBricks.Requests
{
   
    public class DatabricksClusterStartResponse
    {
        [JsonProperty(PropertyName = "error_code")]
        public string ErrorCode { get; set; }

        [JsonProperty(PropertyName = "message")]
        public string Message { get; set; }
    }
}
