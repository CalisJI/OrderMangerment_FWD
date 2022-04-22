using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace OrderManagerment_WPF.View
{
    /// <summary>
    /// Interaction logic for TinhToanView.xaml
    /// </summary>
    public partial class TinhToanView : Window
    {
        public TinhToanView()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
           
            datagrid1.UpdateLayout();
            datagrid1.Items.Refresh();
        }
    }
}
