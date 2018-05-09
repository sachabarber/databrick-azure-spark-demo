using Newtonsoft.Json;

namespace SAS.Spark.Runner.REST.DataBricks.Requests
{
    public class DatabricksDbfsAddBlockRequest
    {
        public long handle { get; set; }

        //base64 encoded string
        public string data { get; set; }
    }
}
