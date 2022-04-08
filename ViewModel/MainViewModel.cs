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
        
        #endregion

        #region Innitial
        #region Icommand
        public static ICommand  UpdateProperty { get; set; }
        public ICommand DeleteDonHang { get; set; }
        public ICommand AddOrder { get; set; }
        public static ICommand Save { get; set; }
        public ICommand Edit { get; set; }
        public ICommand ItemDetermine { get; set; }
        public ICommand tesst { get; set; }
        public ICommand ThongkeTable { get; set; }
        #endregion
        #region Variable
        private DispatcherTimer TimerNotify;
        private int indexnew = 0;
        private DanhSachDonHang OrderSelected = new DanhSachDonHang();
        private List<DanhSachDonHang> TempListOrder = new List<DanhSachDonHang>();
        #endregion
        public void GetData() 
        {
            DanhSachDonHangs = new ObservableCollection<DanhSachDonHang>();
            DanhSachDonHang danhSachDonHang1 = new DanhSachDonHang();
            danhSachDonHang1.RangeAlarm = 3;
            danhSachDonHang1.Customer = "aido";
            danhSachDonHang1.InputDay = DateTime.Today;
            danhSachDonHang1.Note = "note";
            danhSachDonHang1.Stage = TrangThai.PO;
            danhSachDonHang1.ProductDetails = new ObservableCollection<BangBaoGia>()
            {
               new BangBaoGia()
                {
                    ProductName = "ten1",
                    Nguoimua = "người 1",
                    Marker = "",
                    Note="dd",
                    But_Price_U = 198516,
                    LeadTime_Buy = 2,
                    LeadTime_Giao = 5,
                    Link_Buy_Spare = "",
                    Buy_Linking = "dffff",
                    Quantity=5,
                    STT = 1,
                    Total=8555,
                    Total_Price_B_P=555,
                    Transport_Fee=551,
                    Unit = "PCS"

                },
                new BangBaoGia()
                {
                    ProductName = "ten1",
                    Nguoimua = "người 2",
                    Marker = "",
                    Note="dd",
                    But_Price_U = 198516,
                    LeadTime_Buy = 2,
                    LeadTime_Giao = 5,
                    Link_Buy_Spare = "",
                    Buy_Linking = "dffff",
                    Quantity=5,
                    STT = 1,
                    Total=8555,
                    Total_Price_B_P=555,
                    Transport_Fee=551,
                    Unit = "PCS"

                },
                new BangBaoGia()
                {
                    ProductName = "ten1",
                    Nguoimua = "người 3",
                    Marker = "",
                    Note="dd",
                    But_Price_U = 198516,
                    LeadTime_Buy = 2,
                    LeadTime_Giao = 5,
                    Link_Buy_Spare = "",
                    Buy_Linking = "dffff",
                    Quantity=5,
                    STT = 1,
                    Total=8555,
                    Total_Price_B_P=555,
                    Transport_Fee=551,
                    Unit = "PCS"

                }
            };
            DanhSachDonHang danhSachDonHang2 = new DanhSachDonHang();
            
            danhSachDonHang2.Customer = "Nguoikahc";
            danhSachDonHang2.RangeAlarm = 2;
            danhSachDonHang2.InputDay = DateTime.Today;
            danhSachDonHang2.Note = "Note";
            danhSachDonHang2.Stage = TrangThai.DaGiao;
            danhSachDonHang2.ProductDetails = new ObservableCollection<BangBaoGia>()
            {
               new BangBaoGia()
                {
                    ProductName = "ten1",
                    Nguoimua = "người 1",
                    Marker = "",
                    Note="dd",
                    But_Price_U = 198516,
                    LeadTime_Buy = 2,
                    LeadTime_Giao = 5,
                    Link_Buy_Spare = "",
                    Buy_Linking = "dffff",
                    Quantity=5,
                    STT = 1,
                    Total=8555,
                    Total_Price_B_P=555,
                    Transport_Fee=551,
                    Unit = "PCS"

                },
                new BangBaoGia()
                {
                    ProductName = "ten1",
                    Nguoimua = "người 2",
                    Marker = "",
                    Note="dd",
                    But_Price_U = 198516,
                    LeadTime_Buy = 2,
                    LeadTime_Giao = 5,
                    Link_Buy_Spare = "",
                    Buy_Linking = "dffff",
                    Quantity=5,
                    STT = 1,
                    Total=8555,
                    Total_Price_B_P=555,
                    Transport_Fee=551,
                    Unit = "PCS"

                },
                new BangBaoGia()
                {
                    ProductName = "ten1",
                    Nguoimua = "người 3",
                    Marker = "",
                    Note="dd",
                    But_Price_U = 198516,
                    LeadTime_Buy = 2,
                    LeadTime_Giao = 5,
                    Link_Buy_Spare = "",
                    Buy_Linking = "dffff",
                    Quantity=5,
                    STT = 1,
                    Total=8555,
                    Total_Price_B_P=555,
                    Transport_Fee=551,
                    Unit = "PCS"

                }
            };
            DanhSachDonHangs.Add(danhSachDonHang1);
            DanhSachDonHangs.Add(danhSachDonHang2);
            List<DanhSachDonHang> a = DanhSachDonHangs.Where(x => x.IDOrder == 0).ToList();
            foreach (DanhSachDonHang item in a)
            {
                item.IDOrder = DanhSachDonHangs.IndexOf(item) + 1;
            }
        }

        #region ViewModel
        DanhSachDonHang_ViewModel DanhSachDonHang_ViewModel = DanhSachDonHang_ViewModel.INS_DanhSachDonHangViewModel;
        BangThongKe_ViewModel BangThongKe_ViewModel = BangThongKe_ViewModel.INS_BangThongKe;
        #endregion

        #endregion
        public MainViewModel()
        {
            //GetData();
            TimerNotify = new DispatcherTimer();
            TimerNotify.Interval = new TimeSpan(0,30,0);
            TimerNotify.Tick += TimerNotify_Tick;
            TimerNotify.IsEnabled = true;
            TimerNotify.Start();
            mainViewModel = this;
            mainViewModel.SelectedViewModel = DanhSachDonHang_ViewModel;
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
                            HQ = 500000
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
                        OrderSelected = (DanhSachDonHang)p;
                        if (!TempListOrder.Contains((DanhSachDonHang)p))
                        {
                            TempListOrder.Add((DanhSachDonHang)p);
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
                        if((int)(item.InputDay - dateTime).TotalDays > 0) 
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
