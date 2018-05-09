using Newtonsoft.Json;

namespace SAS.Spark.Runner.REST.DataBricks.Requests
{
    public class DatabricksRunNowResponse
    {
        [JsonProperty(PropertyName = "run_id")]
        public int? RunId { get; set; }
    }
}
