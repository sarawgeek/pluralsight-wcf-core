using GeoLib.Services;
using GeoLib.WindowsHost.Contracts;
using GeoLib.WindowsHost.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
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
        public static MainWindow MainUI { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            btn_Start.IsEnabled = true;
            btn_Stop.IsEnabled = true;
            this.Title = "UI Runing on thread : " + Thread.CurrentThread.ManagedThreadId +
                " | Process " + Process.GetCurrentProcess().Id.ToString();
            MainUI = this;
            _SyncContext = SynchronizationContext.Current;
        }

        ServiceHost _HostGeoManager = null;
        ServiceHost _HostMessageManager = null;
        SynchronizationContext _SyncContext = null;
        private void btn_Stop_Click(object sender, RoutedEventArgs e)
        {
            _HostGeoManager.Close();
            _HostMessageManager.Close();
            btn_Stop.IsEnabled = false;
            btn_Start.IsEnabled = true;
        }

        private void btn_Start_Click(object sender, RoutedEventArgs e)
        {
            _HostGeoManager = new ServiceHost(typeof(GeoManager));
            _HostMessageManager = new ServiceHost(typeof(MessageManager));
            _HostGeoManager.Open();
            _HostMessageManager.Open();
            btn_Start.IsEnabled = false;
            btn_Stop.IsEnabled = true;
        }

        public void ShowMessage(string message)
        {
            int threadId = Thread.CurrentThread.ManagedThreadId;
            lblMessage.Content = message + Environment.NewLine + " from Thread id : " + threadId + " to thread :" +
                    Thread.CurrentThread.ManagedThreadId +
                    " | Process " + Process.GetCurrentProcess().Id.ToString();
            //SendOrPostCallback callback = new SendOrPostCallback(arg=>
            //{
            //    lblMessage.Content = message + Environment.NewLine + " from Thread id : " + threadId + " to thread :"+ 
            //        Thread.CurrentThread.ManagedThreadId +
            //        " | Process " + Process.GetCurrentProcess().Id.ToString();
            // });
            //_SyncContext.Send(callback, null);
        }

        private void btnInProcess_Click(object sender, RoutedEventArgs e)
        {
            Thread thread = new Thread(() =>
            {
                ChannelFactory<IMessageService> factory = new ChannelFactory<IMessageService>("");
                IMessageService proxy = factory.CreateChannel();
                proxy.ShowMessage(DateTime.Now.ToLongTimeString() + " In Process Call ");
                factory.Close();
            });
            thread.IsBackground = true;
            thread.Start();
        }
    }
}
