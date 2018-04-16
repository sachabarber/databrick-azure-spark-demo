namespace SAS.Spark.Runner.REST.DataBricks.Responses
{
    public class DatabricksRunResponse
    {
        public int job_id { get; set; }
        public int run_id { get; set; }
        public int number_in_job { get; set; }
        public State state { get; set; }
        public Task task { get; set; }
        public ClusterSpec cluster_spec { get; set; }
        public ClusterInstance cluster_instance { get; set; }
        public OverridingParameters overriding_parameters { get; set; }
        public long start_time { get; set; }
        public int setup_duration { get; set; }
        public int execution_duration { get; set; }
        public int cleanup_duration { get; set; }
        public string trigger { get; set; }
    }
}
