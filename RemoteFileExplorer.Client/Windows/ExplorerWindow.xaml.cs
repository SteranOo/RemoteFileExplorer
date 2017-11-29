using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using RemoteFileExplorer.Middleware.Network;

namespace RemoteFileExplorer.Client.Windows
{
    public partial class ExplorerWindow : Window
    {
        private readonly IServerEngine _serverProxy;

        public ExplorerWindow(IServerEngine serverProxy)
        {
            InitializeComponent();
            _serverProxy = serverProxy;
            CbDrives.ItemsSource = _serverProxy.GetLogicalDrives();
            CbDrives.SelectedIndex = 0;
        }

        private void OpenFolder()
        {
            var directories = _serverProxy.GetSubdirectories(TbCurrentPath.Text);
            var files = _serverProxy.GetFiles(TbCurrentPath.Text);

            var folder = new List<FileSystemInfo>();
            folder.AddRange(directories);
            folder.AddRange(files);

            LbFileTree.ItemsSource = folder;
        }

        private void CbDrives_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TbCurrentPath.Text = CbDrives.SelectedItem.ToString();
            OpenFolder();
        }

        private void LbFileTree_MouseDoubleClick(object sender, RoutedEventArgs e)
        {
            if (LbFileTree.SelectedIndex < 0)
                return;

            if (LbFileTree.SelectedItem is DirectoryInfo)
            {
                TbCurrentPath.Text += @"\" + LbFileTree.SelectedItem;
                OpenFolder();
            }
        }

        private void BtnBackFromDir_Click(object sender, RoutedEventArgs e)
        {
            int index = TbCurrentPath.Text.LastIndexOf(@"\", StringComparison.Ordinal);
            if (index < 3)
                return;

            TbCurrentPath.Text = TbCurrentPath.Text.Remove(index);
            OpenFolder();
        }

        private void CmiDelete_OnClick(object sender, RoutedEventArgs e)
        {
            if (LbFileTree.SelectedIndex < 0)
                return;

            var selectedItemPath = TbCurrentPath.Text + @"\" + LbFileTree.SelectedItem;
            if (LbFileTree.SelectedItem is DirectoryInfo)
            {
                _serverProxy.DeleteDirectory(selectedItemPath, false);
            }
            else
            {
                _serverProxy.DeleteFile(selectedItemPath);
            }
            OpenFolder();
        }

        private void CmiCut_OnClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void CmiPaste_OnClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
            
        }

        private void CmiCopy_OnClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
