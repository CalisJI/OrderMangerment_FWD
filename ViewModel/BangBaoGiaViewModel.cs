

using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Forms;
using OrderManagerment_WPF.OrderObject;
using OrderManagerment_WPF.ApplicationFileConfig;
using System.Windows.Data;
using System.ComponentModel;
using OrderManagerment_WPF.View;
using System.Globalization;
using System.Windows.Media;
using System.Windows.Threading;

namespace OrderManagerment_WPF.ViewModel
{
    public class BangBaoGiaViewModel : BaseViewModel
    {
        private static BangBaoGiaViewModel _ViewModel = new BangBaoGiaViewModel();
        public static BangBaoGiaViewModel INS_BangBaoGiaViewModel
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

        public DataTableCollection DataTableCollection { get; private set; }
        private ObservableCollection<string> _pageExcel;
        public ObservableCollection<string> PageExcel
        {
            get { return _pageExcel; }
            set { _pageExcel = value; }
        }
        List<string> item = new List<string>();


        ObservableCollection<string> _pageSheet;
        public ObservableCollection<string> PageSheet
        {
            get { return _pageSheet; }
            set
            {
                SetProperty(ref _pageSheet, value, nameof(PageSheet));
            }
        }

        public DanhSachDonHang Orderselect
        {
            get
            {
                return MainViewModel.OrderSelected;
            }
            set
            {
                SetProperty(ref MainViewModel.OrderSelected, value, nameof(Orderselect));



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

        private int PageCOunt;
        public int Page_Count
        {
            get
            {
                return PageCOunt;
            }
            set
            {
                SetProperty(ref PageCOunt, value, nameof(Page_Count));
            }
        }
        public static DataTable _data;
        public DataTable DatatableExcel
        {

            get { return _data; }
            set
            {
                SetProperty(ref _data, value, nameof(DatatableExcel));
            }
        }


        public static string _uri;
        public string uri
        {
            get { return _uri; }
            set { SetProperty(ref _uri, value, nameof(uri)); }
        }

        public static string _content;
        public string content
        {
            get { return _content; }
            set { SetProperty(ref _content, value, nameof(content)); }
        }



        ObservableCollection<int> _STT;
        public ObservableCollection<int> STT
        {
            get { return _STT; }
            set
            {
                SetProperty(ref _STT, value, nameof(STT));
            }
        }
        ObservableCollection<string> _MaSanPham;
        public ObservableCollection<string> MaSanPham
        {
            get { return _MaSanPham; }
            set { SetProperty(ref _MaSanPham, value, nameof(MaSanPham)); }
        }
        public static string str;
        public string _Str
        {
            get { return str; }
            set { SetProperty(ref str, value, nameof(_Str)); }
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
        public double _TongGiaMua;
        public double TongGiaMua
        {
            get { return _TongGiaMua; }
            set { SetProperty(ref _TongGiaMua, value, nameof(TongGiaMua)); }
        }
        public double _TongGiaBan;
        public double TongGiaBan
        {
            get { return _TongGiaBan; }
            set { SetProperty(ref _TongGiaBan, value, nameof(TongGiaBan)); }
        }

        public double _TongPhiShip;
        public double TongPhiShip
        {
            get { return _TongPhiShip; }
            set { SetProperty(ref _TongPhiShip, value, nameof(TongPhiShip)); }
        }
        public double _VAT;
        public double VAT
        {
            get { return _VAT; }
            set { SetProperty(ref _VAT, value, nameof(VAT)); }
        }
        public double _Phantram;
        public double Phantram
        {
            get { return _Phantram; }
            set { SetProperty(ref _Phantram, value, nameof(Phantram)); }

        }
        public double _Vanchuyen;
        public double Vanchuyen
        {
            get { return _Vanchuyen; }
            set { SetProperty(ref _Vanchuyen, value, nameof(Vanchuyen)); }
        }
        public int _Soluong;
        public int Soluong
        {
            get { return _Soluong; }
            set { SetProperty(ref _Soluong, value, nameof(Soluong)); }
        }
        public double _HQ;
        public double HQ
        {
            get { return _HQ; }
            set { SetProperty(ref _HQ, value, nameof(HQ)); }
        }

        public static DispatcherTimer TimerNotify1;

        public ICommand Openfolder { get; set; }
        public ICommand Search { get; set; }
        public ICommand SaveChiTietBangBaoGia { get; set; }
        public ICommand TinhToan { get; set; }
        public ICommand NhapLink { get; set; }
        public ICommand HoanThanhLink { get; set; }
        public ICommand STTLink { get; set; }
        public ICommand Edit { get; set; }
        public ICommand Loaded { get; set; }
        public ICommand LuuTinhToan { get; set; }
        public ICommand Unloaed { get; set; }
        public BangBaoGiaViewModel()
        {
            Loaded = new RelayCommand<object>((p) => { return true; }, (p) =>
             {
                 TimerNotify1 = new DispatcherTimer();
                 TimerNotify1.Interval = new TimeSpan(0, 1, 0);
                 TimerNotify1.Tick += TimerNotify1_Tick;
                 TimerNotify1.IsEnabled = true;
                 TimerNotify1.Start();
                 STT = new ObservableCollection<int>();
                 MaSanPham = new ObservableCollection<string>();
             });
            Unloaed = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                TimerNotify1.IsEnabled = false;
                TimerNotify1.Stop();
            });

            Openfolder = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                PageSheet = new ObservableCollection<string>();
                try
                {
                    using (OpenFileDialog openFileDialog = new OpenFileDialog() { Filter = "Excel 97-2003 Workbook|*.xls|Excel Workbook|*.xlsx|Excel Comma Seprate|*.csv" })
                    {
                        if (openFileDialog.ShowDialog() == DialogResult.OK)
                        {
                            if (Orderselect.ProductDetails != null)
                            {
                                Orderselect.ProductDetails.Clear();

                            }

                            PageSheet.Clear();
                            PageSheet = new ObservableCollection<string>();
                            ObservableCollection<string> vs = new ObservableCollection<string>();
                            Get_excel(openFileDialog.FileName, ref vs);
                            PageSheet = vs;
                            Page_Count = PageSheet.Count + 1;
                            DatatableExcel = DataTableCollection[0];
                            string[] a = new string[DatatableExcel.Rows.Count];
                            foreach (DataRow row in DatatableExcel.Rows)
                            {




                            }


                            for (int i = 3; i <= DatatableExcel.Rows.Count; i++)
                            {
                                BangBaoGia bangBaoGia = new BangBaoGia();

                                bangBaoGia.STT = i - 2;
                                var k = (Orderselect.InputDay.Month + "/" + (Orderselect.InputDay.Day - 2) + "/" + Orderselect.InputDay.Year);
                                bangBaoGia.SPARE = "";
                                bangBaoGia.LEADTIME_MUA = DateTime.Today;
                                bangBaoGia.LEADTIME_GIAO = DateTime.Parse(k, System.Globalization.CultureInfo.InvariantCulture);

                                bangBaoGia.NOTE = HienTrang.ChuaMua;
                                bangBaoGia.QUANTITY = 0;
                                bangBaoGia.TOTAL = 0;
                                bangBaoGia.TOTAL_B_P = 0;
                                bangBaoGia.TRANSPORT_FEE = 0;
                                bangBaoGia.PRODUCTNAME = DatatableExcel.Rows[i][1].ToString();
                                bangBaoGia.CODE_PRODUCTNAME = DatatableExcel.Rows[i][2].ToString();
                                Orderselect.VAT = 1.1;
                                Orderselect.PHANTRAM = 1.03;
                                Orderselect.HQ = 500000;
                                bangBaoGia.QUANTITY = int.Parse(DatatableExcel.Rows[i][3].ToString());
                                bangBaoGia.MAKER = DatatableExcel.Rows[i][6].ToString();
                                bangBaoGia.UNIT = DatatableExcel.Rows[i][4].ToString();

                                Orderselect.ProductDetails.Add(bangBaoGia);

                            }
                            EmployeeCollection = CollectionViewSource.GetDefaultView(Orderselect.ProductDetails);

                        }
                    }
                }
                catch (Exception ex)
                {
                }

            });
            SaveChiTietBangBaoGia = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                
                if (Orderselect.ProductDetails != null)
                {

                    DateTime today = DateTime.Today;



                    List<BangBaoGia> a = Orderselect.ProductDetails.Where(x => x.NOTE == HienTrang.DangGiao).ToList();
                    foreach (var item in a)
                    {
                        if ((item.LEADTIME_GIAO.Date - item.LEADTIME_MUA.Date).Days < 0)
                        {
                            item._NOTE = "Có Thể Gom Hàng Trễ " + (item.LEADTIME_MUA.Date - item.LEADTIME_GIAO.Date).Days.ToString() + " Ngày";
                        }
                        else if ((item.LEADTIME_MUA.Date - today.Date).Days > 0)
                        {
                            item._NOTE = "Trong Vòng " + (item.LEADTIME_MUA.Date - today.Date).Days.ToString() + " Ngày Nữa Giao Đến";
                        }
                        else if ((item.LEADTIME_MUA.Date - today.Date).Days == 0)
                        {
                            item._NOTE = "Hôm Nay Nhớ Nhận Hàng";
                        }

                        else if ((item.LEADTIME_MUA.Date - today.Date).Days < 0)
                        {
                            item._NOTE = "Đã Nhận Hàng Chưa";
                        }
                    }
                    List<BangBaoGia> b = Orderselect.ProductDetails.Where(x => x.NOTE == HienTrang.ChuaMua).ToList();
                    foreach (var item in b)
                    {
                        if (Orderselect.InputDay.Date < item.LEADTIME_GIAO.Date)
                        {
                            item._NOTE = "Quá Ngày?";
                            //MessageBox.Show("Lỗi đơn hàng " + item.STT.ToString() + " gom hàng sau ngày giao", MessageBoxIcon.Error.ToString());

                        }
                        else
                        {
                            item._NOTE = "Còn " + (item.LEADTIME_GIAO.Date - today.Date).Days.ToString() + " Ngày";
                        }


                    }

                    List<BangBaoGia> c = Orderselect.ProductDetails.Where(x => x.NOTE == HienTrang.DaNhan).ToList();
                    foreach (var item in c)
                    {
                        item._NOTE = "Đã Có Hàng";
                    }
                }


                ApplicationFileCongfig.Update_Data(Orderselect, Orderselect.IDOrder.ToString());
                EnableEdit = false;
                //dayne:

                //    ApplicationFileCongfig.Update_Data(Orderselect, Orderselect.IDOrder.ToString());



            });
            NhapLink = new RelayCommand<object>((p) => { return true; }, (p) =>

            {
                var a = Orderselect.ProductDetails.Select(x => x.STT).ToList();
                var b = Orderselect.ProductDetails.Select(x => x.PRODUCTNAME).ToList();
                foreach (var item in a)
                {
                    STT.Add(item);
                }
                foreach (var item in b)
                {
                    MaSanPham.Add(item);
                }
                NhapLinkView nhapLink = new NhapLinkView();
                nhapLink.ShowDialog();

            });
            int sttlink = 0;
            HoanThanhLink = new RelayCommand<object>((p) => { return true; }, (p) =>
            {

                Orderselect.ProductDetails[sttlink].BUYINGLINK = _uri;
                Orderselect.ProductDetails[sttlink].ContentLink = content;

            });

            STTLink = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                var a = p.ToString();

                sttlink = int.Parse(a) - 1;
                _Str = MaSanPham[sttlink];
            });
            Edit = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                EnableEdit = true;
                
            });
            TinhToan = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                TongGiaBan = Orderselect.TongGiaBan;
                TongGiaMua = Orderselect.TongGiaMua;
                TongPhiShip = Orderselect.TongPhiShip;
                VAT = Orderselect.VAT;
                Phantram = Orderselect.PHANTRAM;
                Vanchuyen = Orderselect.VANCHUYEN;
                HQ = Orderselect.HQ;
              
                TinhToanView tinhToan = new TinhToanView();
                _ = tinhToan.ShowDialog();
            });
            LuuTinhToan = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                Orderselect.TongGiaMua = 0;
                Orderselect.TongGiaBan = 0;
                Orderselect.TongPhiShip = 0;
                Orderselect.VAT = VAT;
                Orderselect.PHANTRAM = Phantram;
                Orderselect.VANCHUYEN = Vanchuyen;
                Orderselect.HQ = HQ;
                for (int i = 0; i < Orderselect.ProductDetails.Count; i++)
                {
                    Orderselect.ProductDetails[i].TOTAL = Orderselect.ProductDetails[i].BUY_PRICE_U * Orderselect.ProductDetails[i].QUANTITY + Orderselect.ProductDetails[i].TRANSPORT_FEE;
                    Orderselect.ProductDetails[i].TOTAL_B_P = Orderselect.ProductDetails[i].BUY_PRICE_U * Orderselect.ProductDetails[i].QUANTITY;
                    Orderselect.TongGiaMua += Orderselect.ProductDetails[i].TOTAL;

                    Orderselect.TongPhiShip += Orderselect.ProductDetails[i].TRANSPORT_FEE;
                }
                Orderselect.TongGiaBan = Orderselect.TongGiaMua * VAT * Phantram + Vanchuyen + HQ;

                TongGiaBan = Orderselect.TongGiaBan;
                TongGiaMua = Orderselect.TongGiaMua;
                TongPhiShip = Orderselect.TongPhiShip;


                foreach (var item in ApplicationFileCongfig.DanhSachDonHangs)
                {
                    ApplicationFileCongfig.Update_Data(item, item.IDOrder.ToString());
                }
            });

            Search = new RelayCommand<object>((p) => { return true; }, (p) =>
             {
                 EmployeeCollection.Filter = Filter;
                 EmployeeCollection.Refresh();
             });
        }

        private void TimerNotify1_Tick(object sender, EventArgs e)
        {
            Notify();
        }

        public static bool Filter(object obj)
        {
            try
            {
                BangBaoGia bang = obj as BangBaoGia;

                if (!string.IsNullOrEmpty(_employeeName))
                {
                    return bang.PRODUCTNAME.Contains(_employeeName);
                }
                if(bang.PRODUCTNAME==null)
                {
                    bang.PRODUCTNAME = "";
                }
                return bang.PRODUCTNAME.Contains(_employeeName);
                
            }
            catch (Exception)
            {

                throw;
            }

        }


        public void Notify()
        {
            string Notify = string.Empty;
            DateTime today = DateTime.Today;

            List<BangBaoGia> a = Orderselect.ProductDetails.Where(x => x.NOTE == HienTrang.ChuaMua && ((x.LEADTIME_GIAO.Date - today.Date).Days <= x.BaoTruocXngay)).ToList();
            List<BangBaoGia> b = Orderselect.ProductDetails.Where(x => x.NOTE == HienTrang.DangGiao).ToList();
            List<BangBaoGia> c = Orderselect.ProductDetails.Where(x => x.NOTE != HienTrang.DaNhan && (x.LEADTIME_GIAO.Date > Orderselect.InputDay.Date)).ToList();
            foreach (var item in a)
            {
                if ((item.LEADTIME_GIAO.Date - today.Date).Days <= item.BaoTruocXngay && item.NOTE == HienTrang.ChuaMua)
                {
                    Notify = "Đơn hàng " + item.STT.ToString() + " chưa mua, còn " + (item.LEADTIME_GIAO.Date - today.Date).Days.ToString() + " ngày";
                }
                if (Notify != "")
                {
                    App.NotifyIcon.ShowBalloonTip(2000, "Đơn hàng cần xử lý", Notify, System.Windows.Forms.ToolTipIcon.Warning);
                    Notify = "";
                }
            }
            foreach (var item in b)
            {

                if ((item.LEADTIME_GIAO.Date - item.LEADTIME_MUA.Date).Days < 0)
                {
                    Notify = "Đơn hàng " + item.STT.ToString() + " đã đặt, có thể gom hàng trễ " + (item.LEADTIME_MUA.Date - item.LEADTIME_GIAO.Date).Days.ToString() + " ngày," + " nhận hàng trong vòng "
                + (item.LEADTIME_MUA.Date - today.Date).Days.ToString() + " ngày";
                }
                else if ((item.LEADTIME_MUA.Date - today.Date).Days == 0)
                {
                    Notify = "Nhận đơn hàng " + item.STT.ToString() + " trong hôm nay!!!";
                }
                else if ((item.LEADTIME_MUA.Date - today.Date).Days > 0)
                {
                    Notify = "Đơn hàng " + item.STT.ToString() + " đã đặt, trong vòng " + (item.LEADTIME_MUA.Date - today.Date).Days.ToString() + " ngày nữa đến";
                }
                else if ((item.LEADTIME_MUA.Date - today.Date).Days < 0)
                {
                    Notify = " Bạn đã nhận đơn hàng " + item.STT.ToString() + " chưa, giao trễ " + (today.Date - item.LEADTIME_MUA.Date).Days.ToString() + " ngày";
                }

                if (Notify != "")
                {
                    App.NotifyIcon.ShowBalloonTip(2000, "Đơn hàng cần xử lý", Notify, System.Windows.Forms.ToolTipIcon.Warning);
                    Notify = "";
                }
            }
            foreach (var item in c)
            {

                Notify = "Đơn hàng " + item.STT.ToString() + " quá ngày";

                if (Notify != "")
                {
                    App.NotifyIcon.ShowBalloonTip(2000, "Đơn hàng cần xử lý", Notify, System.Windows.Forms.ToolTipIcon.Warning);
                    Notify = "";
                }
            }





        }

        public void Get_excel(string path, ref ObservableCollection<string> collection)
        {
            using (var stream = File.Open(path, FileMode.Open, FileAccess.Read))
            {
                using (IExcelDataReader reader = ExcelReaderFactory.CreateReader(stream))
                {
                    DataSet dataSet = reader.AsDataSet(new ExcelDataSetConfiguration()
                    {
                        ConfigureDataTable = (_) => new ExcelDataTableConfiguration() { UseHeaderRow = true }
                    });
                    DataTableCollection = dataSet.Tables;
                    item.Clear();
                    item.AddRange(DataTableCollection.Cast<DataTable>().Select(t => t.TableName).ToArray<string>());
                    PageExcel = new ObservableCollection<string>();
                    foreach (var it in item)
                    {
                        PageExcel.Add(it);
                    }
                    collection = PageExcel;
                }
            }
        }
    }
    public class GridRowColor2 : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch ((HienTrang)value)
            {
                case HienTrang.DaNhan:
                    return Colors.Blue;
                case HienTrang.DangGiao:
                    return Colors.Green;
                case HienTrang.ChuaMua:
                    return Colors.White;

                default:
                    return Colors.White;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }

}
