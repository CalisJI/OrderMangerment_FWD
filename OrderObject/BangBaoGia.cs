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
        public string ProductName { get; set; }
        public string Marker  { get; set; }
        public int LeadTime_Giao { get; set; }
        public string  Unit { get; set; }
        public int Quantity { get; set; }
        public int But_Price_U { get; set; }
        public int Total_Price_B_P { get; set; }
        public int Transport_Fee { get; set; }
        public int Total { get; set; }
        public string Buy_Linking { get; set; }
        public int LeadTime_Buy { get; set; }
        public string Note  { get; set; }
        public string Link_Buy_Spare { get; set; }
        public string Nguoimua { get; set; }

    }
}
