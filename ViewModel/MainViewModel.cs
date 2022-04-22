using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using OrderManagerment_WPF.ApplicationFileConfig;
using OrderManagerment_WPF.OrderObject;

namespace OrderManagerment_WPF.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        #region Model of MainViewModel
        private BaseViewModel _selectedview;
        public BaseViewModel SelectedViewModel
        {
            get => _selectedview;
            set => SetProperty(ref _selectedview, value, nameof(SelectedViewModel));
        }
        public MainViewModel mainViewModel;

        public ObservableCollection<DanhSachDonHang> DanhSachDonHangs
        {
            get => ApplicationFileCongfig.DanhSachDonHangs;
            set => SetProperty(ref ApplicationFileCongfig.DanhSachDonHangs, value, nameof(DanhSachDonHangs));
        }
        private bool _EnableEdit;
        public bool EnableEdit
        {
            get
            {
                return _EnableEdit;
            }
            set
            {
                SetProperty(ref _EnableEdit, value, nameof(EnableEdit));
            }
        }


        private static ICollectionView _employeeCollection;

        public ICollectionView EmployeeCollection
        {
            get { return _employeeCollection; }
            set { SetProperty(ref _employeeCollection, value); }
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

        #endregion

        #region Innitial
        #region Icommand
        public static ICommand UpdateProperty { get; set; }
        public ICommand DeleteDonHang { get; set; }
        public ICommand AddOrder { get; set; }
        public static ICommand Save { get; set; }
        public ICommand Edit { get; set; }
        public ICommand ItemDetermine { get; set; }
        public ICommand tesst { get; set; }
        public ICommand ThongkeTable { get; set; }
        public ICommand ChiTietBangBaoGia { get; set; }
        public ICommand QuayVe { get; set; }
        public ICommand Loaded { get; set; }
        public ICommand Search { get; set; }

        #endregion
        #region Variable
        public static DispatcherTimer TimerNotify;
        private int indexnew = 0;
        public static DanhSachDonHang OrderSelected;
        public static bool BoolTimer = false;
        private List<DanhSachDonHang> TempListOrder = new List<DanhSachDonHang>();
        public BangBaoGiaViewModel BangBaoGiaViewModel = BangBaoGiaViewModel.INS_BangBaoGiaViewModel;
        #endregion


        #region ViewModel
        DanhSachDonHang_ViewModel DanhSachDonHang_ViewModel = DanhSachDonHang_ViewModel.INS_DanhSachDonHangViewModel;
        BangThongKe_ViewModel BangThongKe_ViewModel = BangThongKe_ViewModel.INS_BangThongKe;
        #endregion

        #endregion
        public MainViewModel()
        {

            mainViewModel = this;
            mainViewModel.SelectedViewModel = DanhSachDonHang_ViewModel;
            Loaded = new RelayCommand<object>((p) => { return true; }, (p) =>
             {
                 TimerNotify = new DispatcherTimer();
                 TimerNotify.Interval = new TimeSpan(0, 30, 0);
                 TimerNotify.Tick += TimerNotify_Tick;
                 TimerNotify.IsEnabled = true;
                 TimerNotify.Start();
                 EmployeeCollection = CollectionViewSource.GetDefaultView(DanhSachDonHangs);

             });


            UpdateProperty = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                OnPropertyChanged(nameof(DanhSachDonHangs));
            });
            DeleteDonHang = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                try
                {
                    DeleteOrder((DanhSachDonHang)p);
                }
                catch (Exception ex)
                {

                    Console.WriteLine(ex.Message);
                }
            });
            AddOrder = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                try
                {
                    DanhSachDonHang danhSachDonHang = new DanhSachDonHang
                    {
                        Customer = "",
                        IDOrder = 0,
                        InputDay = DateTime.Today,
                        ProductDetails = new ObservableCollection<BangBaoGia>(),
                        Stage = TrangThai.ChuaBaoGia,
                        RangeAlarm = 3,
                        Note = "",
                        Alarm = Alarm.Request,
                        BangThongKe = new BangThongKe()
                        {

                        }


                    };
                    DanhSachDonHangs.Add(danhSachDonHang);
                    DanhSachDonHang a = DanhSachDonHangs.First(x => x.IDOrder == 0);
                    int max = DanhSachDonHangs.Max(x => x.IDOrder);
                    a.IDOrder = max + 1;
                    indexnew = a.IDOrder;
                    EnableEdit = true;

                }
                catch (Exception)
                {


                }
            });
            Save = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                new DanhSachDonHang();
                try
                {
                    EnableEdit = false;
                    if (indexnew > 0)
                    {
                        DanhSachDonHang a = DanhSachDonHangs.First(x => x.IDOrder == indexnew);
                        _ = ApplicationFileCongfig.Get_Data<DanhSachDonHang>(a.IDOrder.ToString());
                        ApplicationFileCongfig.Update_Data(a, a.IDOrder.ToString());
                        indexnew = -1;
                    }
                    foreach (DanhSachDonHang item in TempListOrder)
                    {
                        try
                        {
                            ApplicationFileCongfig.Update_Data(item, item.IDOrder.ToString());
                            if (!ApplicationFileCongfig.SystemConfig.DanhSachOrder.Contains(item.IDOrder.ToString()))
                            {
                                ApplicationFileCongfig.SystemConfig.DanhSachOrder.Add(item.IDOrder.ToString());
                                ApplicationFileCongfig.Update_Data(ApplicationFileCongfig.SystemConfig);
                            }
                        }
                        catch (Exception)
                        {


                        }

                    }
                    TempListOrder.Clear();

                }
                catch (Exception ex)
                {

                    Console.WriteLine(ex);
                }

            });
            Edit = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                EnableEdit = true;
            });
            ItemDetermine = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                try
                {
                    if (EnableEdit)
                    {
                        if (p != null)
                        {
                            OrderSelected = (DanhSachDonHang)p;
                           
                            if (!TempListOrder.Contains((DanhSachDonHang)p))
                            {
                                TempListOrder.Add((DanhSachDonHang)p);
                            }
                        }

                    }
                }
                catch (Exception)
                {


                }


            });
            ThongkeTable = new RelayCommand<object>((p) => { return true; }, (p) =>
            {

                mainViewModel.SelectedViewModel = BangThongKe_ViewModel;
            });

            ChiTietBangBaoGia = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                BoolTimer = true;
                TimerNotify.IsEnabled = false;
                TimerNotify.Stop();

                OrderSelected = (DanhSachDonHang)p;
                if (OrderSelected != null)
                {
                    BangBaoGiaViewModel.Orderselect = OrderSelected;

                    BangBaoGiaViewModel.EmployeeCollection = CollectionViewSource.GetDefaultView(BangBaoGiaViewModel.Orderselect.ProductDetails);
                    // BangBaoGiaViewModel.EmployeeCollection.Filter = BangBaoGiaViewModel.Filter;


                    if (BangBaoGiaViewModel.Orderselect.ProductDetails != null)
                    {
                        DateTime today = DateTime.Today;
                        List<BangBaoGia> a = BangBaoGiaViewModel.Orderselect.ProductDetails.Where(x => x.NOTE == HienTrang.ChuaMua).ToList();
                        foreach (var item in a)
                        {
                            if (item.LEADTIME_GIAO.Date > OrderSelected.InputDay.Date)
                            {
                                item._NOTE = "Quá Ngày?";
                            }
                            else
                            {
                                item._NOTE = "Còn " + (item.LEADTIME_GIAO.Date - today.Date).Days.ToString() + " Ngày";
                            }


                        }
                        List<BangBaoGia> b = BangBaoGiaViewModel.Orderselect.ProductDetails.Where(x => x.NOTE == HienTrang.DangGiao).ToList();
                        foreach (var item in b)
                        {
                            if ((item.LEADTIME_GIAO.Date - item.LEADTIME_MUA.Date).Days < 0)
                            {
                                item._NOTE = "Có Thể Gom Hàng Trễ " + (item.LEADTIME_MUA.Date - item.LEADTIME_GIAO.Date).Days.ToString() + " Ngày";
                            }
                            else if ((item.LEADTIME_MUA.Date - today.Date).Days < 0)
                            {
                                item._NOTE = "Bạn Đã Nhận Hàng Chưa";
                            }
                            else if ((item.LEADTIME_MUA.Date - today.Date).Days > 0)
                            {
                                item._NOTE = (item.LEADTIME_MUA.Date - today.Date).Days.ToString() + " Ngày Nữa Giao Đến";
                            }
                            else if ((item.LEADTIME_MUA.Date - today.Date).Days == 0)
                            {
                                item._NOTE = "Hôm Nay Nhớ Nhận Hàng";
                            }

                        }
                    }
                }

                mainViewModel.SelectedViewModel = BangBaoGiaViewModel;


            });
            QuayVe = new RelayCommand<object>((p) => { return true; }, (p) =>
            {

                TimerNotify.IsEnabled = true;
                TimerNotify.Start();
                if (BoolTimer == true)
                {
                    BangBaoGiaViewModel.TimerNotify1.IsEnabled = false;
                    BangBaoGiaViewModel.TimerNotify1.Stop();
                    BoolTimer = false;
                }

                mainViewModel.SelectedViewModel = DanhSachDonHang_ViewModel;

            });
            Search = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                EmployeeCollection.Filter = Filter;
                EmployeeCollection.Refresh();
            });
        }

        private void TimerNotify_Tick(object sender, EventArgs e)
        {
            Notify();


        }
        #region Method
        public void Notify()
        {
            DateTime dateTime = DateTime.Today;

            string Notify = string.Empty;


            List<DanhSachDonHang> a = ApplicationFileCongfig.DanhSachDonHangs.Where(x => x.RangeAlarm <= (int)((TimeSpan)(x.InputDay - dateTime)).TotalDays && x.Stage != TrangThai.DaGiao).ToList();

            foreach (DanhSachDonHang item in a)
            {
                if (item.Stage != TrangThai.DaGiao)
                {
                    if ((int)(item.InputDay - dateTime).TotalDays > item.RangeAlarm)
                    {
                        item.Alarm = Alarm.Pending;
                        Notify += "Đơn Hàng" + item.IDOrder.ToString() + $" Hạn còn {(int)(item.InputDay - dateTime).TotalDays} ngày" + Environment.NewLine;
                    }
                    else if ((int)(item.InputDay - dateTime).TotalDays <= item.RangeAlarm)
                    {
                        if ((int)(item.InputDay - dateTime).TotalDays > 0)
                        {
                            item.Alarm = Alarm.Late;
                            Notify += "Đơn Hàng" + item.IDOrder.ToString() + $" Hạn còn {(int)(item.InputDay - dateTime).TotalDays} ngày" + Environment.NewLine;
                        }
                        else
                        {
                            item.Alarm = Alarm.Late;
                            Notify += "Đơn Hàng" + item.IDOrder.ToString() + $" Trễ hạn {(int)(item.InputDay - dateTime).TotalDays} ngày" + Environment.NewLine;
                        }
                    }
                }
                else
                {
                    item.Alarm = Alarm.Done;

                }
            }
            //foreach (DanhSachDonHang item in DanhSachDonHangs)
            //{
            //    if (item.Alarm == Alarm.Late || item.Alarm == Alarm.Pending || item.Alarm == Alarm.Request)
            //    {
            //        Notify += "Đơn Hàng" + item.IDOrder.ToString()+ $" Hạn còn {left} ngày" + Environment.NewLine;
            //    }
            //}
            if (Notify != "")
            {
                App.NotifyIcon.ShowBalloonTip(10000, "Đơn hàng cần xử lý", Notify, System.Windows.Forms.ToolTipIcon.Warning);
            }
        }
        #endregion
        public static bool Filter(object obj)
        {
            try
            {
                DanhSachDonHang ds = obj as DanhSachDonHang;
                
                if (!string.IsNullOrEmpty(_employeeName))
                {
                    return ds.Customer.Contains(_employeeName);
                }
                if (ds.Customer == null)
                {
                    ds.Customer = "";
                }
                return ds.Customer.Contains(_employeeName);

            }
            catch (Exception)
            {

                throw;
            }

        }
        public void DeleteOrder(DanhSachDonHang danhSachDonHang)
        {
            try
            {
                if (danhSachDonHang == null)
                {
                    danhSachDonHang = DanhSachDonHangs.ElementAt(0);
                }
                _ = DanhSachDonHangs.Remove(danhSachDonHang);
                ApplicationFileCongfig.DeleteDonHang(danhSachDonHang.IDOrder.ToString());
                if (ApplicationFileCongfig.SystemConfig.DanhSachOrder.Contains(danhSachDonHang.IDOrder.ToString()))
                {
                    _ = ApplicationFileCongfig.SystemConfig.DanhSachOrder.Remove(danhSachDonHang.IDOrder.ToString());
                    ApplicationFileCongfig.Update_Data(ApplicationFileCongfig.SystemConfig);
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
        }
    }
    public class GridRowColor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch ((TrangThai)value)
            {
                case TrangThai.DaGiao:
                    return Colors.Blue;

                case TrangThai.PO:
                    return Colors.Green;
                case TrangThai.Dabaogia:
                    return Colors.Yellow;
                case TrangThai.ChuaBaoGia:
                    return Colors.WhiteSmoke;
                default:
                    return Colors.White;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
    public class EnumToString : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch ((TrangThai)value)
            {
                case TrangThai.DaGiao:
                    return "Đã Giao Hàng";
                case TrangThai.PO:
                    return "Đã Có Po (Chờ Hàng Về)";
                case TrangThai.Dabaogia:
                    return "Đã Báo Giá";
                case TrangThai.ChuaBaoGia:
                    return "Chưa Báo Giá";
                default:
                    return "";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
    public class AlarmToString : IValueConverter
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual bool SetProperty<T>(ref T storge, T value, [CallerMemberName] string propertyName = null)
        {
            if (Equals(storge, value))
            {
                return false;
            }

            storge = value;
            OnPropertyChanged(propertyName);

            return true;
        }
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public string Notify(DanhSachDonHang value)
        {
            if (value.Stage != TrangThai.DaGiao)
            {
                DateTime dateTime = DateTime.Today;
                TimeSpan timeSpan = value.InputDay - dateTime;
                double left = timeSpan.TotalDays;
                return left >= 0 ? string.Format("Còn lại {0} ngày", left) : string.Format("Trễ {0} ngày", Math.Abs(left));
            }
            else
            {
                return "Đã Giao";
            }


        }
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            DanhSachDonHang a = ApplicationFileCongfig.DanhSachDonHangs.First(x => x.IDOrder == (int)value);
            return Notify(a);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }

}
