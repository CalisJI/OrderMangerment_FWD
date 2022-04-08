using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagerment_WPF.OrderObject
{
    public class DanhSachDonHang
    {
        public int IDOrder { get; set; }
        public string Customer { get; set; }
        public int RangeAlarm { get; set; }
        public string Note { get; set; }
        public DateTime InputDay { get; set; }
        public TrangThai Stage { get; set; }
        public Alarm Alarm { get; set; }
        public ObservableCollection<BangBaoGia> ProductDetails { get; set; }
        // Phan Thống Kê
        public BangThongKe BangThongKe { get; set; }
    }
    public enum TrangThai 
    {
        [Description("Đã Giao")]
        DaGiao,
        [Description("PO")]
        PO,
        [Description("Đã Báo Giá")]
        Dabaogia,
        [Description("Chưa Báo Giá")]
        ChuaBaoGia
    }
    public enum Alarm 
    {
        Pending,
        Request,
        Late,
        Done
    }
}
