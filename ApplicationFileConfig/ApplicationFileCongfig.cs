using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace OrderManagerment_WPF.ApplicationFileConfig
{
    
    public class ApplicationFileCongfig
    {
        private static string Systemconfig = Directory.GetCurrentDirectory() + @"\" + "ApplicationConfig.xml";
        #region File Initial
        /// <summary>
        /// Khởi tạo file mặc định
        /// </summary>
        /// <param name="filename"></param>
        public static void CheckFileExit(string filename)
        {
            if (!File.Exists(filename))
            {
                File.Create(filename).Close();
            }
        }
        
        #endregion

    }
}
