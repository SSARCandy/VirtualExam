using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualExam
{
    [Serializable]
    class DownloadPath
    {
        private string path;
        public void setPath(string path)
        {
            this.path = path;
        }
        public string getPath()
        {
            return this.path;
        }
    }
}
