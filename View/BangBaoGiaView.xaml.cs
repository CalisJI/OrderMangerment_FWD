using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace OrderManagerment_WPF.View
{
    /// <summary>
    /// Interaction logic for BangBaoGiaView.xaml
    /// </summary>
    public partial class BangBaoGiaView : System.Windows.Controls.UserControl
    {
        public BangBaoGiaView()
        {
            InitializeComponent();          
        }
       
        private void LinkClick(object sender, RoutedEventArgs e)
        {
            var destination = ((Hyperlink)e.OriginalSource).NavigateUri;

            Trace.WriteLine("Browsing to " + destination);

            using (Process browser = new Process())
            {
                browser.StartInfo = new ProcessStartInfo
                {
                    FileName = destination.ToString(),
                    UseShellExecute = true,
                    ErrorDialog = true
                };
                browser.Start();
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DtGrid.UpdateLayout();
            DtGrid.Items.Refresh();
        }


        class Data
        {
            public string link { get; set; }
            public string content { get; set; }
        }
    }
}
