using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using OrderManagerment_WPF.ApplicationFileConfig;
using OrderManagerment_WPF.OrderObject;

namespace OrderManagerment_WPF.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        private BaseViewModel _selectedview;
        public BaseViewModel SelectedViewModel
        {
            get => _selectedview;
            set => SetProperty(ref _selectedview, value, nameof(SelectedViewModel));
        }
        public MainViewModel mainViewModel;
        
        public ObservableCollection<DanhSachDonHang> DanhSachDonHangs
        {
            get
            {
                return ApplicationFileCongfig.DanhSachDonHangs;
            }
            set => SetProperty(ref ApplicationFileCongfig.DanhSachDonHangs, value, nameof(DanhSachDonHangs));
        }
        #region Innitial

        public void GetData() 
        {
            DanhSachDonHangs = new ObservableCollection<DanhSachDonHang>();
            DanhSachDonHang danhSachDonHang1 = new DanhSachDonHang();
            danhSachDonHang1.Alarm = Alarm.Pending;
            danhSachDonHang1.Customer = "aido";
            danhSachDonHang1.OrderName = "Mạch Arduino";
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
            danhSachDonHang2.Alarm = Alarm.Pending;
            danhSachDonHang2.Customer = "Nguoikahc";
            danhSachDonHang2.OrderName = "DriverMotor";
            danhSachDonHang2.InputDay = DateTime.Today;
            danhSachDonHang2.Note = "Note";
            danhSachDonHang2.Stage = TrangThai.Dagiao;
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
        }

        #region ViewModel
        DanhSachDonHang_ViewModel DanhSachDonHang_ViewModel = DanhSachDonHang_ViewModel.INS_DanhSachDonHangViewModel;
        #endregion

        #endregion
        public MainViewModel() 
        {
            GetData();
            mainViewModel = this;
            mainViewModel.SelectedViewModel = DanhSachDonHang_ViewModel;
            
        }
    }
    public class GridRowColor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch ((TrangThai)value)
            {
                case TrangThai.Dagiao:
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
                case TrangThai.Dagiao:
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
    //public class AlarmToString : IValueConverter
    //{
    //    public string Notify(DateTime value) 
    //    {
    //        DateTime dateTime = DateTime.Today;
    //        TimeSpan timeSpan = dateTime - value;
    //        double left = timeSpan.TotalDays;
    //        return string.Format("Còn lại {0} ngày", left);
    //    }
    //    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    //    {
    //        switch ((Alarm)value)
    //        {
    //            case Alarm.Pending:
    //                retun
    //            case Alarm.Request:
    //                break;
    //            case Alarm.Late:
    //                break;
    //            default:
    //                break;
    //        }
    //    }

    //    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    //    {
    //        return null;
    //    }
    //}
}
