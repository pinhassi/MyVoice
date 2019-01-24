using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MyVoice.Model
{
    public class AppSettings
    {
        public void Save(string filepath)
        {
            string dir = Path.GetDirectoryName(filepath);
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);
            using (StreamWriter sw = new StreamWriter(filepath))
            {
                XmlSerializer xmls = new XmlSerializer(typeof(AppSettings));
                xmls.Serialize(sw, this);
            }
        }
        public static AppSettings Read(string filename)
        {
            AppSettings res = null;
            try
            {
                using (StreamReader sw = new StreamReader(filename))
                {
                    XmlSerializer xmls = new XmlSerializer(typeof(AppSettings));
                    res = xmls.Deserialize(sw) as AppSettings;
                }
            }
            catch {
                res = new AppSettings();
            }
            return res;

        }
        ////////////////////////////////////////////////////
        public string FilePath { get; set; }
        public string FolderPath { get; set; }
    }
}
