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
    class SaveExam
    {
        private FileStream saveFile;
        public void save(MyExcelCollection[] mec,String examName)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "/Exams/";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            //saveFile = File.Create(path + "/" + mec.getExamName());

            FileStream stream = File.Create(path + examName + ".ve");
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, mec);
            stream.Close();
        }
    }
}
