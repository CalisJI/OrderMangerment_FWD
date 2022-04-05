using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
<<<<<<< HEAD
using System.Windows.Input;

using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;
=======
>>>>>>> parent of da897f1 (datagrid Pennding)
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
        private ObservableCollection<DanhSachDonHang> _danhSachDonHangs;
        public ObservableCollection<DanhSachDonHang> DanhSachDonHangs
        {
            get 
<<<<<<< HEAD
            {
                return _danhSachDonHangs;
            }
            set 
            {
                SetProperty(ref _danhSachDonHangs, value, nameof(DanhSachDonHangs));
            }
        }
        private ObservableCollection<BangBaoGia> _BangBaoGias;
        public ObservableCollection<BangBaoGia> BangBaoGias
        {
            get
            {
                return _BangBaoGias;
            }
            set
            {
                SetProperty(ref _BangBaoGias, value, nameof(BangBaoGias));
=======
            {
                return _danhSachDonHangs;
            }
            set 
            {
                SetProperty(ref _danhSachDonHangs, value, nameof(DanhSachDonHangs));
>>>>>>> parent of da897f1 (datagrid Pennding)
            }
        }
        #region Innitial

        public void GetData() 
        {
            DanhSachDonHangs = new ObservableCollection<DanhSachDonHang>();
            DanhSachDonHang danhSachDonHang1 = new DanhSachDonHang();
            danhSachDonHang1.Alarm = Alarm.Pending;
            danhSachDonHang1.Customer = "aido";
            danhSachDonHang1.InputDay = DateTime.Today;
            danhSachDonHang1.Note = "note";
            danhSachDonHang1.Stage = TrangThai.ChuaBaoGia;
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
           
            DanhSachDonHangs.Add(danhSachDonHang1);
           
        }
        #endregion

        #region ICommand
       public static ICommand BangBaoGia { get; set; }
        #endregion
        #region ViewModel
        DanhSachDonHang_ViewModel DanhSachDonHang_ViewModel = DanhSachDonHang_ViewModel.INS_DanhSachDonHangViewModel;
        BangBaoGiaViewModel BangBaoGia_ViewModel = BangBaoGiaViewModel.INS_BangBaoGiaViewModel;
        #endregion

       
        public MainViewModel() 
        {
            GetData();
            mainViewModel = this;
            mainViewModel.SelectedViewModel = DanhSachDonHang_ViewModel;
<<<<<<< HEAD
            BangBaoGia= new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                mainViewModel.SelectedViewModel = BangBaoGia_ViewModel;
            });

=======
            
>>>>>>> parent of da897f1 (datagrid Pennding)
        }
    }
}
