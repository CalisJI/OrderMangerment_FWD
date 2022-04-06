using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;
using System.Xml;
using System.Collections.ObjectModel;
using OrderManagerment_WPF.OrderObject;

namespace OrderManagerment_WPF.ApplicationFileConfig
{

    public class ApplicationFileCongfig
    {
        private static readonly string Systemconfig = Directory.GetCurrentDirectory() + @"\" + "ApplicationConfig.xml";
        public static readonly string FileStorageName = "FileDatabase";
        public static SystemConfig SystemConfig;
        public static bool ConnectServer = false;
        public static ObservableCollection<DanhSachDonHang> DanhSachDonHangs = new ObservableCollection<DanhSachDonHang>();
        #region File Initial
        public ApplicationFileCongfig()
        {

            SystemConfig = Get_Data<SystemConfig>();

            if (SystemConfig.LinkNas != "")
            {
                try
                {
                    if (!Directory.Exists(SystemConfig.LinkNas + @"\" + FileStorageName))
                    {
                        _ = Directory.CreateDirectory(SystemConfig.LinkNas + @"\" + FileStorageName);
                    }
                    else
                    {
                        if (SystemConfig.DanhSachOrder.Count == 0)
                        {
                            DirectoryInfo directory = new DirectoryInfo(SystemConfig.LinkNas + @"\" + FileStorageName);
                            FileInfo[] fileInfo = directory.GetFiles("*.xml");
                            foreach (FileInfo item in fileInfo)
                            {
                                SystemConfig.DanhSachOrder.Add(item.Name);
                            }
                            foreach (string item in SystemConfig.DanhSachOrder)
                            {
                                DanhSachDonHang donHang = Get_Data<DanhSachDonHang>(item);
                                DanhSachDonHangs.Add(donHang);
                            }
                            Update_Data(SystemConfig);
                        }
                        else
                        {

                            foreach (var item in SystemConfig.DanhSachOrder)
                            {
                                DanhSachDonHang donHang = Get_Data<DanhSachDonHang>(item);
                                DanhSachDonHangs.Add(donHang);
                            }
                        }

                    }
                    ConnectServer = true;
                }
                catch (Exception ex)
                {
                    ConnectServer = false;
                    Console.WriteLine(ex.Message);
                }

            }
            else
            {
                ConnectServer = false;
            }
        }
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
        public static string Create_MapFile(string FileName, bool data = false)
        {
            if (data)
            {
                string Devicefile = SystemConfig.LinkNas + @"\" + FileStorageName + @"\" + FileName + ".xml";
                return Devicefile;
            }
            else
            {
                string Devicefile = FileName;
                return Devicefile;
            }

        }
        public static T Get_Data<T>(string Mapping_path = "")
        {

            if (Mapping_path != "")
            {
                if (File.Exists(Create_MapFile(Mapping_path, true)))
                {

                    XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
                    Stream stream = new FileStream(Create_MapFile(Mapping_path, true), FileMode.Open);
                    T mapping = (T)xmlSerializer.Deserialize(stream);
                    stream.Close();
                    return mapping;

                }
                else
                {
                    T generic = (T)Activator.CreateInstance(typeof(T));

                    //

                    XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
                    Stream stream = new FileStream(Create_MapFile(Mapping_path, true), FileMode.Create);
                    using (XmlWriter xmlwriter = new XmlTextWriter(stream, Encoding.UTF8))
                    {
                        xmlSerializer.Serialize(xmlwriter, generic);
                        xmlwriter.Close();
                    }
                    stream.Close();
                    return generic;
                }
            }
            else
            {
                if (File.Exists(Create_MapFile(Systemconfig)))
                {

                    XmlSerializer xmlSerializer = new XmlSerializer(typeof(SystemConfig));
                    Stream stream = new FileStream(Create_MapFile(Systemconfig), FileMode.Open);
                    T mapping = (T)xmlSerializer.Deserialize(stream);
                    stream.Close();
                    return mapping;

                }
                else
                {
                    T generic = (T)Activator.CreateInstance(typeof(SystemConfig));

                    XmlSerializer xmlSerializer = new XmlSerializer(typeof(SystemConfig));
                    Stream stream = new FileStream(Create_MapFile(Systemconfig), FileMode.Create);
                    using (XmlWriter xmlwriter = new XmlTextWriter(stream, Encoding.UTF8))
                    {
                        xmlSerializer.Serialize(xmlwriter, generic);
                        xmlwriter.Close();
                    }
                    stream.Close();
                    return generic;
                }
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="MapFile_Path"></param>
        public static void Update_Data(object data = null, string MapFile_Path = "")
        {
            if (MapFile_Path != "")
            {
                Type type = data.GetType();
                CheckFileExit(Create_MapFile(MapFile_Path));
                XmlSerializer xmlSerializer = new XmlSerializer(type);
                using (TextWriter textWriter = new StreamWriter(Create_MapFile(MapFile_Path)))
                {
                    xmlSerializer.Serialize(textWriter, data);
                    textWriter.Close();
                }
            }
            else
            {
                Type type = data.GetType();
                CheckFileExit(Create_MapFile(Systemconfig));
                XmlSerializer xmlSerializer = new XmlSerializer(type);
                using (TextWriter textWriter = new StreamWriter(Create_MapFile(Systemconfig)))
                {
                    xmlSerializer.Serialize(textWriter, data);
                    textWriter.Close();
                }
            }
        }

        public static void DeleteDonHang(string fileName)
        {
            try
            {
                if (File.Exists(SystemConfig.LinkNas + @"\" + FileStorageName + @"\" + fileName + ".xml"))
                {
                    File.Delete(SystemConfig.LinkNas + @"\" + FileStorageName + @"\" + fileName + ".xml");
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
        }
        #endregion

    }
}
