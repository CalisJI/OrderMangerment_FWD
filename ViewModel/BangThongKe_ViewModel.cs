using OrderManagerment_WPF.ApplicationFileConfig;
using OrderManagerment_WPF.OrderObject;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
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
        public ObservableCollection<DanhSachDonHang> _PhanThongKe;
        public ObservableCollection<DanhSachDonHang> PhanThongKe
        {
            get { return _PhanThongKe; }
            set { SetProperty(ref _PhanThongKe, value, nameof(PhanThongKe)); }
        }

        private static ICollectionView _employeeCollection;

        public ICollectionView EmployeeCollection
        {
            get => _employeeCollection;
            set => _ = SetProperty(ref _employeeCollection, value);
        }

        public static string _employeeName = string.Empty;
        public string EmployeeName
        {
            get { return _employeeName; }
            set
            {
                SetProperty(ref _employeeName, value);
                // EmployeeCollection.Refresh();
            }
        }

        #region Icommand
        public ICommand Edit { get; set; }
        public ICommand Save { get; set; }
        public ICommand Load { get; set; }
        public ICommand PO { get; set; }
        public ICommand ChiTietDon { get; set; }
        public ICommand Search { get; set; }
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
            PhanThongKe = new ObservableCollection<DanhSachDonHang>();
            
            Load = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                PhanThongKe.Clear();
                int stt = 0;
                List<DanhSachDonHang> a = DanhSachDons.Where(x => x.Stage == TrangThai.PO).ToList();
                foreach (var item in a)
                {
                    stt++;
                    item.BangThongKe.STT = stt;
                    item.BangThongKe.KhachHang = item.Customer;
                    item.BangThongKe.NguoiBan = item.BangThongKe.NguoiBan;
                    item.BangThongKe.GiaMua = item.TongGiaMua;
                    item.BangThongKe.GiaBan = item.TongGiaBan;
                    item.BangThongKe.LoiNhuan_Rong = Math.Round(item.TongGiaBan - (item.TongGiaMua - item.TongPhiShip) * item.VAT - item.VANCHUYEN - item.HQ,2);
                    item.BangThongKe.LN_Per_GiaBan =  Math.Round(item.BangThongKe.LoiNhuan_Rong / item.TongGiaBan * (double)100,2);
                    item.BangThongKe.PhiBH = 0;
                    item.BangThongKe.VanChuyen = item.VANCHUYEN;
                    item.BangThongKe.HQ = item.HQ;
                    item.BangThongKe.PhiShipMuaHang =  Math.Round(item.TongPhiShip,2);
                    item.BangThongKe.Percent_Increase = Math.Round(item.BangThongKe.LoiNhuan_Rong / (item.TongGiaBan - item.VANCHUYEN - item.HQ) * (double)100,2);
                    PhanThongKe.Add(item);
                    EmployeeCollection= CollectionViewSource.GetDefaultView(PhanThongKe);

                }


            });
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


            PO = new RelayCommand<object>((p) => { return true; }, (p) =>
            {

                Editable = true;
            });
            ChiTietDon = new RelayCommand<object>((p) => { return true; }, (p) =>
            {

            });
            Search = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                EmployeeCollection.Filter = Filter;
                EmployeeCollection.Refresh();
            });
        }
        public static bool Filter(object obj)
        {
            try
            {
                DanhSachDonHang ds = obj as DanhSachDonHang;

                if (!string.IsNullOrEmpty(_employeeName))
                {
                    return ds.BangThongKe.KhachHang.Contains(_employeeName);
                }
                if (ds.BangThongKe.KhachHang == null)
                {
                    ds.BangThongKe.KhachHang = "";
                }
                return ds.BangThongKe.KhachHang.Contains(_employeeName);

            }
            catch (Exception)
            {

                throw;
            }

        }
    }


}
