using System.Configuration;
using System.IO;
using System.Windows;
using SAS.Spark.Runner.REST.DataBricks;

namespace SAS.Spark.Runner
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var tokenFileName = ConfigurationManager.AppSettings["TokenFileLocation"];
            if (!File.Exists(tokenFileName))
            {
                MessageBox.Show("Expecting token file to be provided");
            }

            AccessToken = File.ReadAllText(tokenFileName);

            if(!AccessToken.StartsWith("token:"))
            {
                MessageBox.Show("Token file should start with 'token:' following directly by YOUR DataBricks initial token you created");
            }
        }

        public static string AccessToken { get; private set; }
    }
}
