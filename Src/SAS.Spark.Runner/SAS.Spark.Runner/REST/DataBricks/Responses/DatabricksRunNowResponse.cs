using Newtonsoft.Json;

namespace SAS.Spark.Runner.REST.DataBricks.Requests
{
    public class DatabricksRunNowResponse
    {
        //[JsonProperty(PropertyName = "job_id")]
        //public int JobId { get; set; }

        [JsonProperty(PropertyName = "run_id")]
        public int? RunId { get; set; }
    }
}
