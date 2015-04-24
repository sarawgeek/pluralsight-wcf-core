using GeoLib.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
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

namespace GeoLib.WindowsHost
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            btn_Start.IsEnabled = true;
            btn_Stop.IsEnabled = true;
        }

        ServiceHost _HostGeoManager = null;
        private void btn_Stop_Click(object sender, RoutedEventArgs e)
        {
            _HostGeoManager.Close();
            btn_Stop.IsEnabled = false;
            btn_Start.IsEnabled = true;
        }

        private void btn_Start_Click(object sender, RoutedEventArgs e)
        {
            _HostGeoManager = new ServiceHost(typeof(GeoManager));
            _HostGeoManager.Open();
            btn_Start.IsEnabled = false;
            btn_Stop.IsEnabled = true;
        }

        
    }
}
