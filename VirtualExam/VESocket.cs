using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using myexcelcollection;

namespace VirtualExam
{
    class VESocket
    {
        MyExcelCollection[] m = new MyExcelCollection[1];
        VEClient vec = null;
        public static Socket sck = null;
        Thread SckSReceiveTd = null;
        DownloadPath dlPath = new DownloadPath();
        public VESocket()
        {
            sck = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            sck.Connect(new IPEndPoint(IPAddress.Parse("219.85.200.148"), 1234));
            vec = new VEClient();
            SendVEClient(vec);
            SckSReceiveTd = new Thread(ReceiveData);
            SckSReceiveTd.Start();
        }
        public Socket getSck()
        {
            return sck;
        }
        private bool dlCmp = false;

        public void setSck()
        {
            sck = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            m[0] = new MyExcelCollection();
        }

        public void SendMyExcelCollection(MyExcelCollection[] m)
        {
            this.m = m;
            Serialize(m);
        }
        public void Serialize(MyExcelCollection[] m)
        {
            BinaryFormatter bf = new BinaryFormatter();
            MemoryStream stream = new MemoryStream();
            bf.Serialize(stream, m);
            byte[] bytesSend = new byte[1024];
            bytesSend = stream.ToArray();
            sck.Send(bytesSend);
        }



        public void SendVEClient(VEClient vec)
        {
            this.vec = vec;
            Serialize(vec);
        }
        public void Serialize(VEClient vec)
        {
            BinaryFormatter bf = new BinaryFormatter();
            MemoryStream stream = new MemoryStream();
            bf.Serialize(stream, vec);
            byte[] bytesSend = new byte[1048576];
            bytesSend = stream.ToArray();
            sck.Send(bytesSend);
        }

        private void Deserialize(byte[] data)
        {
            BinaryFormatter bf = new BinaryFormatter();
            MemoryStream stream = new MemoryStream(data);
            object obj = bf.Deserialize(stream);
            if (obj.GetType() == m.GetType())
            {
                this.m = (MyExcelCollection[])obj;
                dlCmp = true;
            }
        }
        public void ReceiveData()
        {
            try
            {
                byte[] clientData = new byte[1048576];  // 其中RDataLen為每次要接受來自 Client 傳來的資料長度
                while (true)
                {
                    sck.Receive(clientData);
                    Deserialize(clientData);
                }
            }
            catch { }
        }

        public MyExcelCollection[] getMyExcelCollection()
        {
            return this.m;
        }

        public void Download(String path)
        {
            dlCmp = false;
            dlPath.setPath(path);
            Serialize(dlPath);
        }
        public void Serialize(object obj)
        {
            BinaryFormatter bf = new BinaryFormatter();
            MemoryStream stream = new MemoryStream();
            bf.Serialize(stream, obj);
            byte[] bytesSend = new byte[1048576];
            bytesSend = stream.ToArray();
            sck.Send(bytesSend);
            while (true)
            {
                if (dlCmp)
                    break;
            }

        }

    }
}