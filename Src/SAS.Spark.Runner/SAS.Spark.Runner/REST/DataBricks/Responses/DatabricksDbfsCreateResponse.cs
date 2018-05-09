using Newtonsoft.Json;

namespace SAS.Spark.Runner.REST.DataBricks.Responses
{
    public class DatabricksDbfsCreateResponse
    {
        [JsonProperty(PropertyName = "handle")]
        public long Handle { get; set; }
    }
}
