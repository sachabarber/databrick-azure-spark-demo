using Newtonsoft.Json;

namespace SAS.Spark.Runner.REST.DataBricks.Requests
{
    // If you want to pass args you can do so using extra properties
    // See : https://docs.databricks.com/api/latest/jobs.html#run-now
    // - jar_params : 
    //   A list of parameters for jobs with jar tasks, e.g. "jar_params": ["john doe", "35"]. 
    //   The parameters will be used to invoke the main function of the main class specified 
    //   in the spark jar task. If not specified upon run-now, it will default to an empty list.
    // - notebook_params : 
    //   A map from keys to values for jobs with notebook task, 
    //   e.g. "notebook_params": {"name": "john doe", "age":  "35"}. 
    //   The map is passed to the notebook and will be accessible through the 
    //   dbutils.widgets.get function
    // - python_params :
    //   A list of parameters for jobs with python tasks, e.g. "python_params": ["john doe", "35"]. 
    //   The parameters will be passed to python file as command line parameters. 
    // If specified upon run-now, it would overwrite the parameters specified in job setting. 
    public class DatabricksRunNowRequest
    {
        [JsonProperty(PropertyName = "job_id")]
        public int JobId { get; set; }
    }
}
