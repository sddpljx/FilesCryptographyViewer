using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

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

        private void ButtonCompare_Click(object sender, RoutedEventArgs e)
        {
            if (GlobalVar.wheCalc==true)
            {
                string strCompare = this.textBoxInput.Text;
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
            else
            {
                MessageBoxButton btn = MessageBoxButton.OK;
                btn = MessageBoxButton.OK;
                MessageBox.Show("请先计算！", "Warnning!", btn);
            }
        }

        private void Window_Drop(object sender, DragEventArgs e)
        {
            string filePath = ((System.Array)e.Data.GetData(DataFormats.FileDrop)).GetValue(0).ToString();
            FileInfo fInfor = new FileInfo(filePath);
            if (fInfor.Attributes == FileAttributes.Directory)//文件夹
            {
                MessageBoxButton btn = MessageBoxButton.OK;
                btn = MessageBoxButton.OK;
                MessageBox.Show("请拖入文件！", "Warnning!", btn);
            }
            else//文件
            {
                this.progressBar.IsIndeterminate = true;
                CalcFileCrypt(((System.Array)e.Data.GetData(DataFormats.FileDrop)).GetValue(0).ToString());
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
    }

    public class GlobalVar
    {
        public static string strMD5 = "";
        public static string strSHA1 = "";
        public static string strSHA256 = "";
        public static string quickPath = "";
        public static bool wheCalc = false;
    }
}

