using System.Windows;
using SAS.Spark.Runner.REST.DataBricks;

namespace SAS.Spark.Runner
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);


            if (e.Args.Length != 1)
            {
                MessageBox.Show("Expecting token to be provided as command line arg (as raw straight from databricks token creation value)");
            }

            AccessToken = e.Args[0];



         

        }

        public static string AccessToken { get; private set; }
       
    }
}
