using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using myexcelcollection;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace VEServer
{
    class OpenExam
    {
        public MyExcelCollection[] open(string examName)
        {
            MyExcelCollection[] mec = null;
            BinaryFormatter formatter = new BinaryFormatter();
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "/題庫/";
            if (Directory.Exists(path))
            {
                Stream stream = new FileStream(path + examName + ".ve", FileMode.Open, FileAccess.Read, FileShare.Read);
                mec = (MyExcelCollection[])formatter.Deserialize(stream);
                stream.Close();
            }
            return mec;
        }
    }
}
