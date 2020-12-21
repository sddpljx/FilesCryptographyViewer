using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;

namespace FilesCryptographyViewer
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.checkBoxFile.IsChecked = true;
        }

        private string GetVersion()
        {
            try
            {
                return App.ResourceAssembly.GetName(false).Version.ToString();
            }
            catch
            {
                return "Debug Mode";
            }
        }

        private void AppInitialize()
        {
            this.textBoxMD5.Text = "";
            this.textBoxSHA1.Text = "";
            this.textBoxSHA256.Text = "";
            this.textBoxMD5.Foreground = System.Windows.Media.Brushes.Black;
            this.textBoxSHA1.Foreground = System.Windows.Media.Brushes.Black;
            this.textBoxSHA256.Foreground = System.Windows.Media.Brushes.Black;
            this.textBoxInput.Text = "";
            this.textBlockCompare.Text = "Please choose a flie first.";
            this.textBlockCompare.Foreground = System.Windows.Media.Brushes.Black;
            this.progressBar.Value = 0;
            this.textBlockChosenFile.Text = "Choose a flie or drop into the window.";
            this.Width = 600;
            this.folderResult.SelectAll();
            this.folderResult.Selection.Text = "";
            this.checkBoxFile.IsChecked = true;
            this.checkBoxFolder.IsChecked = false;
            GlobalVar.cnt = 0;
            GlobalVar.listFilesCryptInfos = new List<FilesCryptInfo>();
        }

        public async void CalcFileCrypt(string path)
        {
            FileInfo fInfor = new FileInfo(path);
            this.textBlockChosenFile.Text = "File name: " + fInfor.Name;
            string strMD5 = "尚未计算";
            string strSHA1 = "尚未计算";
            string strSHA256 = "尚未计算";
            if (checkBoxMD5.IsChecked == true)
            {
                await Task.Run(() =>
                {
                    strMD5 = FilesCryptography.GetFileMD5(path);
                }
                );
                this.textBoxMD5.Text = strMD5;
            }
            if (checkBoxSHA1.IsChecked == true)
            {
                await Task.Run(() =>
                {
                    strSHA1 = FilesCryptography.GetFileSHA1(path);
                }
                );
                this.textBoxSHA1.Text = strSHA1;
            }
            if (checkBoxSHA256.IsChecked == true)
            {
                await Task.Run(() =>
                {
                    strSHA256 = FilesCryptography.GetFileSHA256(path);
                }
                );
                this.textBoxSHA256.Text = strSHA256;
            }
            this.progressBar.IsIndeterminate = false;
            this.progressBar.Value = 100;
            GlobalVar.strMD5 = strMD5;
            GlobalVar.strSHA1 = strSHA1;
            GlobalVar.strSHA256 = strSHA256;
            GlobalVar.wheCalc = true;
        }

        public async void CalcMultiFiles(string path)
        {
            this.Width = 1150;
            FilesCryptInfo filesCryptInfo = new FilesCryptInfo();
            if (this.checkBoxMD5.IsChecked == true)
            {
                await Task.Run(() =>
                {
                    filesCryptInfo.md5 = FilesCryptography.GetFileMD5(path);

                });
            }
            if (this.checkBoxSHA1.IsChecked == true)
            {
                await Task.Run(() =>
                {
                    filesCryptInfo.sha1 = FilesCryptography.GetFileSHA1(path);
                });
            }
            if (this.checkBoxSHA256.IsChecked == true)
            {
                await Task.Run(() =>
                {
                    filesCryptInfo.sha256 = FilesCryptography.GetFileSHA256(path);
                });
            }
            filesCryptInfo.path = path;
            GlobalVar.listFilesCryptInfos.Add(filesCryptInfo);
            this.folderResult.AppendText(filesCryptInfo.path + "\n");
            if (this.checkBoxMD5.IsChecked == true)
            {
                this.folderResult.AppendText("MD5: " + filesCryptInfo.md5 + "\n");

            }
            if (this.checkBoxMD5.IsChecked == true)
            {
                this.folderResult.AppendText("SHA1: " + filesCryptInfo.sha1 + "\n");

            }
            if (this.checkBoxMD5.IsChecked == true)
            {
                this.folderResult.AppendText("SHA256: " + filesCryptInfo.sha256 + "\n");

            }
            this.folderResult.AppendText("----------------------------------------------------------------------------------------\n");
            this.folderResult.ScrollToEnd();
        }

        public async void CalcFolderCrypt(string folderPath)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(folderPath);
            if (!directoryInfo.Exists)
            {
                return;
            }
            FileSystemInfo[] fileSystemInfos = directoryInfo.GetFileSystemInfos();
            foreach (FileSystemInfo item in fileSystemInfos)
            {
                FilesCryptInfo filesCryptInfo = new FilesCryptInfo();
                FileInfo fileInfo = item as FileInfo;
                if (fileInfo != null)
                {
                    filesCryptInfo.name = fileInfo.Name;
                    filesCryptInfo.path = Convert.ToString(new FileInfo(fileInfo.DirectoryName + "\\" + fileInfo.Name));
                    if (this.checkBoxMD5.IsChecked == true)
                    {
                        await Task.Run(() =>
                        {
                            filesCryptInfo.md5 = FilesCryptography.GetFileMD5(filesCryptInfo.path);
                        });
                    }
                    if (this.checkBoxSHA1.IsChecked == true)
                    {
                        await Task.Run(() =>
                        {
                            filesCryptInfo.sha1 = FilesCryptography.GetFileSHA1(filesCryptInfo.path);
                        });
                    }
                    if (this.checkBoxSHA256.IsChecked == true)
                    {
                        await Task.Run(() =>
                        {
                            filesCryptInfo.sha256 = FilesCryptography.GetFileSHA256(filesCryptInfo.path);
                        });
                    }
                    GlobalVar.listFilesCryptInfos.Add(filesCryptInfo);
                    this.folderResult.AppendText(filesCryptInfo.path + "\n");
                    if (this.checkBoxMD5.IsChecked == true)
                    {
                        this.folderResult.AppendText("MD5: " + filesCryptInfo.md5 + "\n");

                    }
                    if (this.checkBoxMD5.IsChecked == true)
                    {
                        this.folderResult.AppendText("SHA1: " + filesCryptInfo.sha1 + "\n");

                    }
                    if (this.checkBoxMD5.IsChecked == true)
                    {
                        this.folderResult.AppendText("SHA256: " + filesCryptInfo.sha256 + "\n");

                    }
                    this.folderResult.AppendText("----------------------------------------------------------------------------------------\n");
                    this.folderResult.ScrollToEnd();
                    GlobalVar.cnt++;
                }
                else
                {
                    string deepPath = folderPath + "\\" + item.ToString();
                    CalcFolderCrypt(deepPath);
                }
            }
            this.filecnt.Text = GlobalVar.cnt.ToString();
            this.progressBar.IsIndeterminate = false;
            this.progressBar.Value = 100;
            GlobalVar.wheCalc = true;
        }

        private void ButtonClick_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new Microsoft.Win32.OpenFileDialog()
            {
                Filter = "All(*.*)|*.*"
            };
            var result = openFileDialog.ShowDialog();
            if (result == true)
            {
                this.progressBar.IsIndeterminate = true;
                //await Task.Run(() =>
                //{
                CalcFileCrypt(openFileDialog.FileName);

                //}
                //);
            }
            else
            {
                MessageBoxButton btn = MessageBoxButton.OK;
                btn = MessageBoxButton.OK;
                MessageBox.Show("未选择任何文件！", "Warnning!", btn);
            }
        }

        private void buttonFolder_Click(object sender, RoutedEventArgs e)
        {
            //NavigationWindow window = new NavigationWindow();
            //window.Source = new Uri("FolderCryptPage.xaml", UriKind.Relative);
            //window.Title = "Folder Crypt";
            //window.Show();
            var dlg = new CommonOpenFileDialog();
            dlg.Title = "请选择文件夹";
            dlg.IsFolderPicker = true;
            dlg.InitialDirectory = Environment.CurrentDirectory;

            dlg.AddToMostRecentlyUsedList = false;
            dlg.AllowNonFileSystemItems = false;
            dlg.DefaultDirectory = Environment.CurrentDirectory;
            dlg.EnsureFileExists = true;
            dlg.EnsurePathExists = true;
            dlg.EnsureReadOnly = false;
            dlg.EnsureValidNames = true;
            dlg.Multiselect = false;
            dlg.ShowPlacesList = true;
            if (dlg.ShowDialog() == CommonFileDialogResult.Ok)
            {
                var folder = dlg.FileName;
                // Do something with selected folder string
                try
                {
                    CalcFolderCrypt(folder);
                }
                catch (Exception ea)
                {
                    MessageBoxButton btn = MessageBoxButton.OK;
                    btn = MessageBoxButton.OK;
                    MessageBox.Show(ea.ToString(), "Warnning!", btn);
                }
            }
            else
            {
                MessageBoxButton btn = MessageBoxButton.OK;
                btn = MessageBoxButton.OK;
                MessageBox.Show("未选择任何文件夹！", "Warnning!", btn);
            }
        }

        private void ButtonCompare_Click(object sender, RoutedEventArgs e)
        {
            if (GlobalVar.wheCalc == true)
            {
                if (GlobalVar.cnt != 1)
                {
                    bool flagMD5 = false;
                    bool flagSha1 = false;
                    bool flagSha256 = false;
                    int i;
                    string strCompare = this.textBoxInput.Text.ToLower();
                    for (i = 0; i < GlobalVar.cnt; i++)
                    {
                        if (string.Equals(strCompare, GlobalVar.listFilesCryptInfos[i].md5))
                        {
                            flagMD5 = true;
                            break;
                        }
                        else if (string.Equals(strCompare, GlobalVar.listFilesCryptInfos[i].sha1))
                        {
                            flagSha1 = true;
                            break;
                        }
                        else if (string.Equals(strCompare, GlobalVar.listFilesCryptInfos[i].sha256))
                        {
                            flagSha256 = true;
                            break;
                        }
                    }
                    if (flagMD5)
                    {
                        MessageBoxButton btn = MessageBoxButton.OK;
                        btn = MessageBoxButton.OK;
                        MessageBox.Show("Equal with MD5 of " + GlobalVar.listFilesCryptInfos[i].path + "!", "OK!", btn);
                    }
                    else if (flagSha1)
                    {
                        MessageBoxButton btn = MessageBoxButton.OK;
                        btn = MessageBoxButton.OK;
                        MessageBox.Show("Equal with SHA1 of " + GlobalVar.listFilesCryptInfos[i].path + "!", "OK!", btn);
                    }
                    else if (flagSha256)
                    {
                        MessageBoxButton btn = MessageBoxButton.OK;
                        btn = MessageBoxButton.OK;
                        MessageBox.Show("Equal with SHA256 of " + GlobalVar.listFilesCryptInfos[i].path + "!", "OK!", btn);
                    }
                    else
                    {
                        MessageBoxButton btn = MessageBoxButton.OK;
                        btn = MessageBoxButton.OK;
                        MessageBox.Show("Equal to nothing!", "Error!", btn);
                    }
                }
                else
                {
                    string strCompare = this.textBoxInput.Text.ToLower();
                    if (string.Equals(strCompare, GlobalVar.strMD5))
                    {
                        this.textBlockCompare.Text = "Equals to MD5.";
                        this.textBoxMD5.Foreground = System.Windows.Media.Brushes.Red;
                    }
                    else if (string.Equals(strCompare, GlobalVar.strSHA1))
                    {
                        this.textBlockCompare.Text = "Equals to SHA1.";
                        this.textBoxSHA1.Foreground = System.Windows.Media.Brushes.Red;
                    }
                    else if (string.Equals(strCompare, GlobalVar.strSHA256))
                    {
                        this.textBlockCompare.Text = "Equals to SHA256.";
                        this.textBoxSHA256.Foreground = System.Windows.Media.Brushes.Red;
                    }
                    else
                    {
                        this.textBlockCompare.Text = "Equals to NOTHING!";
                        this.textBlockCompare.Foreground = System.Windows.Media.Brushes.Red;
                    }
                }

            }
            else
            {
                MessageBoxButton btn = MessageBoxButton.OK;
                btn = MessageBoxButton.OK;
                MessageBox.Show("请先计算！", "Warnning!", btn);
            }
        }

        private void Window_Drop(object sender, DragEventArgs e)
        {
            this.rectangle.Visibility = Visibility.Hidden;
            this.textBlockShowDrop.Visibility = Visibility.Hidden;
            int cnt = ((System.Array)e.Data.GetData(DataFormats.FileDrop)).Length;
            if (cnt == 1)
            {
                string filePath = ((System.Array)e.Data.GetData(DataFormats.FileDrop)).GetValue(0).ToString();
                FileInfo fInfor = new FileInfo(filePath);
                if (fInfor.Attributes == FileAttributes.Directory)//文件夹
                {
                    string path = ((System.Array)e.Data.GetData(DataFormats.FileDrop)).GetValue(0).ToString();
                    this.checkBoxFile.IsChecked = false;
                    this.checkBoxFolder.IsChecked = true;
                    this.Width = 1160;
                    this.buttonFolder.Visibility = Visibility.Visible;
                    this.progressBar.IsIndeterminate = true;
                    CalcFolderCrypt(path);
                }
                else//文件
                {
                    this.progressBar.IsIndeterminate = true;
                    CalcFileCrypt(((System.Array)e.Data.GetData(DataFormats.FileDrop)).GetValue(0).ToString());
                }
            }
            else
            {
                this.progressBar.IsIndeterminate = true;
                for (int i = 0; i < cnt; i++)
                {
                    string filePath = ((System.Array)e.Data.GetData(DataFormats.FileDrop)).GetValue(i).ToString();
                    CalcMultiFiles(filePath);
                }
                this.progressBar.IsIndeterminate = false;
                this.progressBar.Value = 100;
                this.filecnt.Text = cnt.ToString();
                GlobalVar.cnt = cnt;
                GlobalVar.wheCalc = true;
            }
        }

        private void Window_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            //Application.Current.Shutdown();
            if (e.Key == System.Windows.Input.Key.Escape)
            {
                this.Close();
            }
            if (e.Key == System.Windows.Input.Key.F5)
            {
                AppInitialize();
            }
        }

        private void checkBoxFile_Checked(object sender, RoutedEventArgs e)
        {
            this.checkBoxFolder.IsChecked = false;
            this.Width = 600;
            this.buttonFolder.Visibility = Visibility.Hidden;
        }

        private void checkBoxFolder_Checked(object sender, RoutedEventArgs e)
        {
            this.checkBoxFile.IsChecked = false;
            this.Width = 1150;
            this.buttonFolder.Visibility = Visibility.Visible;
        }

        private void Window_DragEnter(object sender, DragEventArgs e)
        {
            this.rectangle.Visibility = Visibility.Visible;
            this.textBlockShowDrop.Visibility = Visibility.Visible;
        }

        private void Window_DragLeave(object sender, DragEventArgs e)
        {
            this.rectangle.Visibility = Visibility.Hidden;
            this.textBlockShowDrop.Visibility = Visibility.Hidden;
        }
    }

    public class GlobalVar
    {
        public static string strMD5 = "";
        public static string strSHA1 = "";
        public static string strSHA256 = "";
        public static string quickPath = "";
        public static bool wheCalc = false;
        public static List<FilesCryptInfo> listFilesCryptInfos = new List<FilesCryptInfo>();
        public static int cnt = 0;
    }
}

