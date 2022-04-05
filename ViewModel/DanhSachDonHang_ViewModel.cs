using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagerment_WPF.ViewModel
{
    public class DanhSachDonHang_ViewModel: BaseViewModel
    {
        private static DanhSachDonHang_ViewModel _ViewModel = new DanhSachDonHang_ViewModel();
        public  static DanhSachDonHang_ViewModel INS_DanhSachDonHangViewModel 
        {
            get 
            {
                return _ViewModel;
            }
            set 
            {
                _ViewModel = value;
            }
        }
        public DanhSachDonHang_ViewModel() 
        {
        
        }
    }
}
