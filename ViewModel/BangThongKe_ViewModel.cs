using OrderManagerment_WPF.ApplicationFileConfig;
using OrderManagerment_WPF.OrderObject;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace OrderManagerment_WPF.ViewModel
{
    public class BangThongKe_ViewModel : BaseViewModel
    {
        public static BangThongKe_ViewModel INS_BangThongKe { get; set; } = new BangThongKe_ViewModel();

        public ObservableCollection<DanhSachDonHang> DanhSachDons
        {
            get => ApplicationFileCongfig.DanhSachDonHangs;
            set => SetProperty(ref ApplicationFileCongfig.DanhSachDonHangs, value, nameof(DanhSachDons));
        }

        #region Icommand
        public ICommand Edit { get; set; }
        public ICommand Save { get; set; }
        #endregion
        #region Model
        private static bool _edit = false;
        public bool Editable 
        {
            get 
            {
                return _edit;
            }
            set 
            {
                SetProperty(ref _edit, value, nameof(Editable));
            }
        }
        #endregion
        public BangThongKe_ViewModel()
        {
            Edit = new RelayCommand<object>((p) => { return true; }, (p) => 
            {
                Editable = true;
            });
            Save = new RelayCommand<object>((p) => { return true; }, (p) => 
            {
                foreach (var item in DanhSachDons)
                {
                    ApplicationFileCongfig.Update_Data(item, item.IDOrder.ToString());
                }
                Editable = false;
            });
        }
    }

    
}
