
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
