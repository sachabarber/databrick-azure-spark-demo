using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ICSharpCode.AvalonEdit.Folding;
using SAS.Spark.Runner.REST.DataBricks;

namespace SAS.Spark.Runner
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private FoldingManager _foldingManager;
        public MainWindow()
        {
            InitializeComponent();

            _foldingManager = FoldingManager.Install(textEditor.TextArea);
   
        }

        private async void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            var clusterList = await new DatabricksWebApiClient().ClustersListAsync();
            var cluster = await new DatabricksWebApiClient().ClustersGetAsync(clusterList.clusters[0].cluster_id);
            var json = cluster.ToString();

            textEditor.Text = json;
            var foldingStrategy = new BraceFoldingStrategy();
            foldingStrategy.UpdateFoldings(_foldingManager, textEditor.Document);
        }
    }
}
