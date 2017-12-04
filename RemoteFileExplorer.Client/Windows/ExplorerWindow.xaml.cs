using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using RemoteFileExplorer.Middleware.Data;
using RemoteFileExplorer.Middleware.Network;

namespace RemoteFileExplorer.Client.Windows
{
    public class FileCopyInfo
    {
        public FileSystemObjectInfo FileSystemObjectInfo { get; set; }

        public FileCopyType FileCopyType { get; set; }
    }

    public enum FileCopyType
    {
        Copy,
        Cut
    }

    public partial class ExplorerWindow : Window
    {
        private readonly IServerEngine _serverProxy;

        private FileCopyInfo _copyInfo;

        public ExplorerWindow(IServerEngine serverProxy)
        {
            InitializeComponent();
            _serverProxy = serverProxy;
            LoadLogicalDrives();
            CmiPaste.IsEnabled = false;
        }

        private void LoadLogicalDrives()
        {
            var result = _serverProxy.GetLogicalDrives();
            if (!result.Success)
                return;

            CbDrives.ItemsSource = result.Response;
            CbDrives.SelectedIndex = 0;
        }

        private void OpenFolder(string path)
        {
            var result = _serverProxy.GetFileSystemObject(path);
            if (!result.Success)
            {
                MessageBox.Show(result.ErrorMessage, "Exception", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (result.Response == null)
            {
                MessageBox.Show("Object not found.", "Exception", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            var children = result.Response.GetChildren();
            LbFileTree.ItemsSource = children;
            TbCurrentPath.Text = path;
        }

        private void CbDrives_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            OpenFolder(CbDrives.SelectedItem.ToString());
        }

        private void LbFileTree_MouseDoubleClick(object sender, RoutedEventArgs e)
        {
            if (LbFileTree.SelectedIndex < 0)
                return;

            if (LbFileTree.SelectedItem is DirectoryObjectInfo)
            {
                
                OpenFolder(TbCurrentPath.Text + @"\" + LbFileTree.SelectedItem);
            }
        }

        private void BtnReloadDir_Click(object sender, RoutedEventArgs e)
        {
            OpenFolder(TbCurrentPath.Text);
        }

        private void BtnBackFromDir_Click(object sender, RoutedEventArgs e)
        {
            var index = TbCurrentPath.Text.LastIndexOf(@"\", StringComparison.Ordinal);
            if (index < 3)
                return;

            OpenFolder(TbCurrentPath.Text.Remove(index));
        }

        private void CmiDelete_OnClick(object sender, RoutedEventArgs e)
        {
            if (LbFileTree.SelectedIndex < 0)
                return;

            var selectedItemPath = TbCurrentPath.Text + @"\" + LbFileTree.SelectedItem;
            if (LbFileTree.SelectedItem is DirectoryObjectInfo)
            {
                var result = _serverProxy.DeleteDirectory(selectedItemPath, false);
                if (!result.Success)
                {
                    MessageBox.Show(result.ErrorMessage, "Exception", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
            }
            else
            {
                var result = _serverProxy.DeleteFile(selectedItemPath);
                if (!result.Success)
                {
                    MessageBox.Show(result.ErrorMessage, "Exception", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
            }
            OpenFolder(TbCurrentPath.Text);
        }

        private void CmiCut_OnClick(object sender, RoutedEventArgs e)
        {
            if (LbFileTree.SelectedIndex < 0)
                return;

            _copyInfo = new FileCopyInfo
            {
                FileSystemObjectInfo = (FileSystemObjectInfo)LbFileTree.SelectedItem,
                FileCopyType = FileCopyType.Cut
            };
            CmiPaste.IsEnabled = true;
        }

        private void CmiCopy_OnClick(object sender, RoutedEventArgs e)
        {
            if (LbFileTree.SelectedIndex < 0)
                return;

            _copyInfo = new FileCopyInfo
            {
                FileSystemObjectInfo = (FileSystemObjectInfo)LbFileTree.SelectedItem,
                FileCopyType = FileCopyType.Copy
            };
            CmiPaste.IsEnabled = true;
        }

        private void CmiPaste_OnClick(object sender, RoutedEventArgs e)
        {
            if (_copyInfo == null)
                return;

            var info = _copyInfo.FileSystemObjectInfo;
            OperationResult result = null;
            switch (_copyInfo.FileCopyType)
            {
                case FileCopyType.Copy:
                    if (info is DirectoryObjectInfo)
                        result = _serverProxy.CopyDirectory(info.Path, TbCurrentPath.Text + @"\" + info.Name, true, false);
                    else
                        result = _serverProxy.CopyFile(info.Path, TbCurrentPath.Text + @"\" + info.Name, false);
                    break;
                case FileCopyType.Cut:
                    if (info is DirectoryObjectInfo)
                        result = _serverProxy.MoveDirectory(info.Path, TbCurrentPath.Text + @"\" + info.Name);
                    else
                        result = _serverProxy.MoveFile(info.Path, TbCurrentPath.Text + @"\" + info.Name);
                    break;
            }
            if (result != null && !result.Success)
                MessageBox.Show(result.ErrorMessage, "Exception", MessageBoxButton.OK, MessageBoxImage.Warning);
            OpenFolder(TbCurrentPath.Text);
        }
    }
}
