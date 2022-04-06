﻿using System;
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
            get => ApplicationFileCongfig.DanhSachDonHangs;
            set => SetProperty(ref ApplicationFileCongfig.DanhSachDonHangs, value, nameof(DanhSachDonHangs));
        }
        #region Innitial
        #region Icommand
        public static ICommand  UpdateProperty { get; set; }
        public ICommand DeleteDonHang { get; set; }
        public ICommand AddOrder { get; set; }
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
            List<DanhSachDonHang> a = DanhSachDonHangs.Where(x => x.IDOrder == 0).ToList();
            foreach (DanhSachDonHang item in a)
            {
                item.IDOrder = DanhSachDonHangs.IndexOf(item) + 1;
            }
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
                        Alarm = Alarm.Request
                    };
                    DanhSachDonHangs.Add(danhSachDonHang);
                    DanhSachDonHang a = DanhSachDonHangs.First(x => x.IDOrder == 0);
                    a.IDOrder = DanhSachDonHangs.IndexOf(a) + 1;
                }
                catch (Exception)
                {

                    
                }
            });
        }
        public void DeleteOrder(DanhSachDonHang danhSachDonHang) 
        {
            try
            {
                _ = DanhSachDonHangs.Remove(danhSachDonHang);
                ApplicationFileCongfig.DeleteDonHang(danhSachDonHang.Customer + danhSachDonHang.IDOrder);
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
        public string Notify(DateTime value)
        {
            DateTime dateTime = DateTime.Today;
            TimeSpan timeSpan = dateTime - value;
            
            double left = timeSpan.TotalDays;
            List<DanhSachDonHang> a = ApplicationFileCongfig.DanhSachDonHangs.Where(x => x.InputDay == value).ToList();
            foreach (DanhSachDonHang item in a)
            {
                if(item.Stage == TrangThai.PO) 
                {
                    if (left > item.RangeAlarm)
                    {
                        item.Alarm = Alarm.Pending;
                    }
                    else if (left <= item.RangeAlarm)
                    {
                        item.Alarm = Alarm.Late;
                    }
                }
            }
            MainViewModel.UpdateProperty.Execute(null);
            return string.Format("Còn lại {0} ngày", left);
        }
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            
            return Notify((DateTime)value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
