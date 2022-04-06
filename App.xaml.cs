using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;



namespace OrderManagerment_WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static System.Windows.Forms.NotifyIcon NotifyIcon = new System.Windows.Forms.NotifyIcon();
        public App() 
        {
            NotifyIcon.Icon = new System.Drawing.Icon(Directory.GetCurrentDirectory() + @"\" + "ICO" + @"\SaleManager.ico");
            NotifyIcon.Visible = true;
            //KNotifyIcon.ShowBalloonTip(10000, "Title", "Tip", System.Windows.Forms.ToolTipIcon.Info);
            
            NotifyIcon.ContextMenu = new System.Windows.Forms.ContextMenu();
            NotifyIcon.ContextMenu.MenuItems.Add("Open");
            NotifyIcon.ContextMenu.MenuItems.Add("Exit");
            NotifyIcon.ContextMenu.MenuItems[0].Click += App_Click;
            NotifyIcon.ContextMenu.MenuItems[1].Click += App_Click1;
            InstallMeOnStartUp();
        }

        private void App_Click1(object sender, EventArgs e)
        {
            Current.MainWindow.Close();
            Current.Shutdown();
        }

        private void App_Click(object sender, EventArgs e)
        {
            MainWindow.Visibility = Visibility.Visible;
            MainWindow.WindowState = WindowState.Normal;
        }

        private void InstallMeOnStartUp()
        {
            try
            {
                Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
                Assembly curAssembly = Assembly.GetExecutingAssembly();
                key.SetValue(curAssembly.GetName().Name, curAssembly.Location);
            }
            catch { }
        }
    }
}
