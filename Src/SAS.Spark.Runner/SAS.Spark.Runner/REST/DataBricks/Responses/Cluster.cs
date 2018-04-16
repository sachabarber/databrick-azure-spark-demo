using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAS.Spark.Runner.REST.DataBricks.Responses
{
    public class Cluster
    {
        public string cluster_id { get; set; }
        public long spark_context_id { get; set; }
        public string cluster_name { get; set; }
        public string spark_version { get; set; }
        public string node_type_id { get; set; }
        public string driver_node_type_id { get; set; }
        public SparkEnvVars spark_env_vars { get; set; }
        public int autotermination_minutes { get; set; }
        public bool enable_elastic_disk { get; set; }
        public string cluster_source { get; set; }
        public bool enable_jdbc_auto_start { get; set; }
        public string state { get; set; }
        public string state_message { get; set; }
        public long start_time { get; set; }
        public long terminated_time { get; set; }
        public long last_state_loss_time { get; set; }
        public long last_activity_time { get; set; }
        public int num_workers { get; set; }
        public int cluster_memory_mb { get; set; }
        public double cluster_cores { get; set; }
        public DefaultTags default_tags { get; set; }
        public string creator_user_name { get; set; }
        public TerminationReason termination_reason { get; set; }
    }
}
