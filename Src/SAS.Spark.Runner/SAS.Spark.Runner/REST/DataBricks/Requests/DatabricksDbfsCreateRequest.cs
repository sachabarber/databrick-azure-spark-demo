using Newtonsoft.Json;

namespace SAS.Spark.Runner.REST.DataBricks.Requests
{
    public class DatabricksDbfsCreateRequest
    {
        public string path { get; set; }

        public bool overwrite { get; set; }
    }
}
