

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
        private ObservableCollection<BangBaoGia> DSBangBaoGia = new ObservableCollection<BangBaoGia>();

        ObservableCollection<string> _pageSheet;
        public ObservableCollection<string> PageSheet
        {
            get { return _pageSheet; }
            set
            {
                SetProperty(ref _pageSheet, value, nameof(PageSheet));
            }
        }
        private DanhSachDonHang ds;
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

        public ICommand Openfolder { get; set; }

        public BangBaoGiaViewModel()
        {
            Openfolder = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                PageSheet = new ObservableCollection<string>();
                try
                {
                    using (OpenFileDialog openFileDialog = new OpenFileDialog() { Filter = "Excel 97-2003 Workbook|*.xls|Excel Workbook|*.xlsx|Excel Comma Seprate|*.csv" })
                    {
                        if (openFileDialog.ShowDialog() == DialogResult.OK)
                        {
                            PageSheet.Clear();
                            PageSheet = new ObservableCollection<string>();
                            ObservableCollection<string> vs = new ObservableCollection<string>();
                            Get_excel(openFileDialog.FileName, ref vs);
                            PageSheet = vs;
                            Page_Count = PageSheet.Count + 1;
                            DatatableExcel = DataTableCollection[0];
                            for(int i =19; i<31; i++)
                            {
                                BangBaoGia bangBaoGia = new BangBaoGia();
                                DanhSachDonHang danhSachDonHang = new DanhSachDonHang();
                                bangBaoGia.STT =0;
                                bangBaoGia.Marker = "";
                                bangBaoGia.LeadTime_Buy = 0;
                                bangBaoGia.LeadTime_Giao = 0;
                                bangBaoGia.Link_Buy_Spare = "";
                                bangBaoGia.Nguoimua = "";
                                bangBaoGia.Note = "";
                                bangBaoGia.Quantity = 0;
                                bangBaoGia.Total = 0;
                                bangBaoGia.Total_Price_B_P =0;
                                bangBaoGia.Transport_Fee = 0;
                                bangBaoGia.Unit = "";
                                string a = DatatableExcel.Rows[i][2].ToString();
                                bangBaoGia.ProductName = a.ToString();
                               Orderselect.ProductDetails.Add(bangBaoGia);
                            }    
                            
                           


                        }
                    }
                }
                catch (Exception ex)
                {
                }

            });
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
}
