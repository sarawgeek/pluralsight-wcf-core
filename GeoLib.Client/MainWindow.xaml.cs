﻿using GeoLib.Client.Contracts;
using GeoLib.Contracts;
using GeoLib.Proxies;
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

namespace GeoLib.Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.Title = "UI Runing on Thread " + Thread.CurrentThread.ManagedThreadId +
                " | Process " + Process.GetCurrentProcess().Id.ToString();
        }

        private void btnGetZipInfo_Click(object sender, RoutedEventArgs e)
        {
            if (txtZipCode.Text!="")
            {
                GeoClient proxy = new GeoClient();
                try
                {
                    proxy.Open();
                    ZipCodeData data = proxy.GetZipInfo(txtZipCode.Text);
                    if (data != null)
                    {
                        lblCity.Content = data.City;
                        lblState.Content = data.State;
                    }
                    proxy.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Exception thrown by Service\n\r Exception Type: " + ex.GetType().Name
                        + "\r\nException Details : " + ex.Message + "\n\r"
                        + "Proxy State : " + proxy.State.ToString());
                }
                
                
            }

        }

        private void btnGetZipCodes_Click(object sender, RoutedEventArgs e)
        {
            if (txtState.Text!=null)
            {
                
            }
        }

        private void btnMakeCall_Click(object sender, RoutedEventArgs e)
        {
            ChannelFactory<IMessageService> factory = new ChannelFactory<IMessageService>("");
            IMessageService proxy = factory.CreateChannel();
            proxy.ShowMsg(txtToShow.Text);
            factory.Close();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            GeoClient proxy = new GeoClient();

            proxy.OneWayExample();

            MessageBox.Show("We Are at client side");

            proxy.Close();

            MessageBox.Show("Proxy is Closed");


        }
    }
}
