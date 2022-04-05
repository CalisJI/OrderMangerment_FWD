using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;
using System.Xml;

namespace OrderManagerment_WPF.ApplicationFileConfig
{
    
    public class ApplicationFileCongfig
    {
        private static readonly string Systemconfig = Directory.GetCurrentDirectory() + @"\" + "ApplicationConfig.xml";
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
        public static string Create_MapFile(string FileName)
        {
            string Devicefile = System.IO.Directory.GetCurrentDirectory() + @"\" + FileName + ".xml";
            return Devicefile;
        }
        public static T Get_Data<T>(string Mapping_path = "")
        {
            if (Mapping_path != "") 
            {
                if (File.Exists(Create_MapFile(Mapping_path)))
                {

                    XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
                    Stream stream = new FileStream(Create_MapFile(Mapping_path), FileMode.Open);
                    T mapping = (T)xmlSerializer.Deserialize(stream);
                    stream.Close();
                    return mapping;

                }
                else
                {
                    T generic = (T)Activator.CreateInstance(typeof(T));

                    //
                    
                    XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
                    Stream stream = new FileStream(Create_MapFile(Mapping_path), FileMode.Create);
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

                    XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
                    Stream stream = new FileStream(Create_MapFile(Systemconfig), FileMode.Open);
                    T mapping = (T)xmlSerializer.Deserialize(stream);
                    stream.Close();
                    return mapping;

                }
                else
                {
                    T generic = (T)Activator.CreateInstance(typeof(T));

                    XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
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
        #endregion

    }
}
