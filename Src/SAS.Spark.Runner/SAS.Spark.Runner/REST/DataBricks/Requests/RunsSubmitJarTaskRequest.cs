using System.Collections.Generic;

namespace SAS.Spark.Runner.REST.DataBricks.Requests
{
    public class RunsSubmitJarTaskRequest
    {
        public string run_name { get; set; }
        public NewCluster new_cluster { get; set; }
        public List<Library> libraries { get; set; }
        public int timeout_seconds { get; set; }
        public SparkJarTask spark_jar_task { get; set; }
    }

    public class NewCluster
    {
        public string spark_version { get; set; }
        public string node_type_id { get; set; }
        public int num_workers { get; set; }
    }

    public class Maven
    {
        public string coordinates { get; set; }
    }

    public class Library
    {
        public string jar { get; set; }
        public Maven maven { get; set; }
    }

    public class SparkJarTask
    {
        public string main_class_name { get; set; }
        public List<string> parameters { get; set; }
    }
}
