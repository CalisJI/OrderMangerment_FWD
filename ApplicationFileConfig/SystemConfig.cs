using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagerment_WPF.ApplicationFileConfig
{
    public class SystemConfig
    {
        public SystemConfig() 
        {
            OutputExcelPath = "";
            FolderData = "";
            LinkNas = @"\\192.168.2.174\CloudFWD";
            DanhSachOrder = new List<string>();
        }
        public string OutputExcelPath { get; set; }
        public string FolderData { get; set; }
        public string LinkNas { get; set; }
        public List<string> DanhSachOrder { get; set; }
        //public List<string> BangThongKe { get; set; }
    }
    
}
