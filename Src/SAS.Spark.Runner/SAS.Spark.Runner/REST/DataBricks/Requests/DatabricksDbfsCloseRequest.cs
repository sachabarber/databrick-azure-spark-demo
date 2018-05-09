using Newtonsoft.Json;

namespace SAS.Spark.Runner.REST.DataBricks.Requests
{
    public class DatabricksDbfsCloseRequest
    {
        public long handle { get; set; }
    }
}
