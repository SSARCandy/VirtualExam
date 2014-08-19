using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using myexcelcollection;
using System.Runtime.Serialization.Formatters.Binary;

namespace VirtualExam
{
    class OpenExam
    {
        public MyExcelCollection[] open()
        {
            MyExcelCollection[] mec = null;
            BinaryFormatter formatter = new BinaryFormatter();
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "/Exams";
            if (Directory.Exists(path))
            {
                Stream stream = new FileStream(path + "/test.myobj", FileMode.Open, FileAccess.Read, FileShare.Read);
                mec = (MyExcelCollection[])formatter.Deserialize(stream);
                stream.Close();
            }
            return mec;
        }
    }
}
