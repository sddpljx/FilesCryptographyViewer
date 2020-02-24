using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Deployment.Application;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;

namespace FilesCryptographyViewer
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            if (e.Args.Length == 1)
            {
                FileInfo file = new FileInfo(e.Args[0]);
                if (file.Exists)
                {
                    GlobalVar.quickPath = file.FullName;
                    QuickCalc quickCalc = new QuickCalc();
                    quickCalc.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                    quickCalc.Title = "Files Cryptography Viewer - Quick Calclulate - " + GetVersion();
                    quickCalc.Show();
                }
            }
            else
            {
                MainWindow window = new MainWindow();
                window.Title = "Files Cryptography Viewer - " + GetVersion();
                window.Show();
            }
        }

        public string GetVersion()
        {
            try
            {
                return ApplicationDeployment.CurrentDeployment.CurrentVersion.ToString();
            }
            catch (Exception)
            {
                return "DEBUG";
                throw;
            }
        }
    }
}
