using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagerment_WPF.OrderObject
{
    public class DanhSachDonHang
    {
        public string Customer { get; set; }
        public string Note { get; set; }
        public DateTime InputDay { get; set; }
        public TrangThai Stage { get; set; }
        public Alarm Alarm { get; set; }
        public ObservableCollection<BangBaoGia> ProductDetails { get; set; }

    }
    public enum TrangThai 
    {
        Dagiao,
        PO,
        Dabaogia,
        ChuaBaoGia
    }
    public enum Alarm 
    {
        Pending,
        Request,
        Late
    }
}
