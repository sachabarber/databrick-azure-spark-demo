using System.Collections.Generic;

namespace SAS.Spark.Runner.REST.DataBricks.Responses
{
    public class DbfsListResponse
    {
        public List<File> files { get; set; }
    }
}
