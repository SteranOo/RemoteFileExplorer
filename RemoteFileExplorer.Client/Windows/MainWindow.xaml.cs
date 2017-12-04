using System.Windows;
using System.Configuration;
using RemoteFileExplorer.Client.Network;
using RemoteFileExplorer.Middleware.Network;
using System;

namespace RemoteFileExplorer.Client.Windows
{
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            var defAddress = ConfigurationManager.AppSettings.Get("DefaultAddress");
            TbAddress.Text = defAddress;
        }

        private void BtnConnect_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(TbAddress.Text))
                return;

            try
            {
                var serverProxy = new ServiceClientFactory<IServerEngine>().Create(TbAddress.Text);
                var result = serverProxy.Connect();
                if (!result.Success)
                    return;

                Hide();
                var explorerWindow = new ExplorerWindow(serverProxy);
                explorerWindow.ShowDialog();
                Show();
            }
            catch
            {
                MessageBox.Show("Cannot connect to server", "Connection error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
           
        }
    }
}
