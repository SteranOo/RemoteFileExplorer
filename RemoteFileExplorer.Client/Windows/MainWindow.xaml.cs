using System.Windows;
using System.Configuration;
using RemoteFileExplorer.Client.Network;
using RemoteFileExplorer.Middleware.Network;
using System;

namespace RemoteFileExplorer.Client.Windows
{
    public partial class MainWindow : Window
    {
        private Window _explorerWindow;

        public MainWindow()
        {
            InitializeComponent();
            var defAddress = ConfigurationManager.AppSettings.Get("DefaultAddress");
            TbAddress.Text = defAddress;
        }

        private void BtnConnect_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(TbAddress.Text))
            {
                try
                {
                    var serverProxy = new ServiceClientFactory<IServerEngine>().Create(TbAddress.Text);
                    serverProxy.Connect();
                    Hide();
                    try
                    {
                        _explorerWindow = new ExplorerWindow(serverProxy);
                        _explorerWindow.ShowDialog();
                    }
                    catch (Exception ex)
                    {
                        if (_explorerWindow != null)
                        {
                            _explorerWindow.Close();
                            MessageBox.Show("Operation ended with error", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                    Show();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Invalid address", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }
    }
}
