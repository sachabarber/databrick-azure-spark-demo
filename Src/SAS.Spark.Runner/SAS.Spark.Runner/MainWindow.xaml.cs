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
using Autofac;
using ICSharpCode.AvalonEdit.Folding;
using MahApps.Metro.Controls;
using SAS.Spark.Runner.IOC;
using SAS.Spark.Runner.REST.DataBricks;
using SAS.Spark.Runner.ViewModels;

namespace SAS.Spark.Runner
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();

            this.DataContext = ContainerOperations.Container.Resolve<MainWindowViewModel>();
        }
    }
}
