using Newtonsoft.Json;

namespace SAS.Spark.Runner.REST.DataBricks.Requests
{
    public class CreateJobResponse
    {
        [JsonProperty(PropertyName = "job_id")]
        public int JobId { get; set; }
    }
}
