using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagerment_WPF.OrderObject
{
    public class BangBaoGia
    {

        public int STT { get; set; }
        public string PRODUCTNAME { get; set; }
        public string CODE_PRODUCTNAME { get; set; }
        public string MAKER { get; set; }
        public DateTime LEADTIME_GIAO { get; set; } // có hàng trươc bao nhiêu ngày
        public string UNIT { get; set; }
        public int QUANTITY { get; set; }
        public int BUY_PRICE_U { get; set; }
        public int TOTAL_B_P { get; set; }
        public int TRANSPORT_FEE { get; set; }
        public int TOTAL { get; set; }
        public string BUYINGLINK { get; set; }
        public string ContentLink { get; set; }
        public DateTime LEADTIME_MUA { get; set; } // chờ ship về
        public HienTrang NOTE { get; set; }
        public string SPARE { get; set; }
        public string NGUOI_MUA { get; set; }
        public string _NOTE { get; set; }
        public int TONGGIAMUA { get; set; }
        public int TONGGIABAN { get; set; }
        public double VAT { get; set; }
        public double PHANTRAM { get; set; }
        public int VANCHUYEN { get; set; }
        public int HQ { get; set; }
        public int TONGPHIVANCHUYEN { get; set; }
        public int BaoTruocXngay { get; set; }
    }

    public enum HienTrang
    {
        DaNhan, DangGiao, ChuaMua,

    }



}
