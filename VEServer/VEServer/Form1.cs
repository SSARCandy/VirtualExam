using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Runtime.Serialization.Formatters.Binary;
using Microsoft.Office.Interop.Excel;
using myexcelcollection;
using veclient;
using vesocketserver;
using downloadpath;
using OnlineClient;
namespace VEServer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        _Application myExcel=null;
        _Workbook myBook=null;
        _Worksheet mySheet=null;
        Range myRange=null;
        DownloadPath dlPath = new DownloadPath();
        MyExcelCollection[] question=null;
        Socket[] sckAccept;
        VESocket[] SckSs;
        string LocalIP;
        int port = 1234;
        int SckCIndex = 0;
        VEClient[] vec;
        OpenExam openExam = new OpenExam();



        int accumulate = 0;//累積連線人數
        OC onlineClient = new OC();//在線人數物件
        int online = 0;

        private void Form1_Load(object sender, EventArgs e)
        {
            lblHostName.Text = Dns.GetHostName();
            IPAddress[] ipa = Array.FindAll(Dns.GetHostEntry(string.Empty).AddressList, a => a.AddressFamily == AddressFamily.InterNetwork);
            LocalIP = ipa[0].ToString();
            lblHostIP.Text = "本機IP=" + ipa[0].ToString();
            Array.Resize(ref SckSs, 1);
            Array.Resize(ref sckAccept, 1);
            Array.Resize(ref vec, 1);

            sckAccept[0] = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            sckAccept[0].Bind(new IPEndPoint(IPAddress.Parse(LocalIP), port));
            sckAccept[0].Listen(5);
            SckSs[0] = new VESocket();
            vec[0] = new VEClient();
            //SckSs[0].setSck();
            SckSsWaitAccept();
        }

        private void SckSsWaitAccept()
        {
            SckCIndex = SckSs.Length;
            Array.Resize(ref SckSs, SckCIndex + 1);
            Array.Resize(ref sckAccept, SckCIndex + 1);
            Array.Resize(ref vec, SckCIndex + 1);
            Thread SckSAcceptTd = new Thread(SckSAcceptProc);
            SckSAcceptTd.Start();
        }

        private void SckSAcceptProc()
        {
            Form.CheckForIllegalCrossThreadCalls = false;
            int Scki = SckCIndex;
            SckSs[Scki] = new VESocket();
            vec[Scki - 1] = new VEClient();
            sckAccept[Scki] = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            try
            {
                //Socket s = SckSs[Scki].getSck();
                //s = sckAccept.Accept();
                //SckSs[Scki].SckAccept(sckAccept);
                sckAccept[Scki] = sckAccept[0].Accept();
                accumulate++;
                lblAcc.Text = Convert.ToString(accumulate);
                //SckSs[Scki].setSck(s);
                SckSsWaitAccept();
                byte[] clientData = new byte[1024];
                while (true)
                {
                   // if (SckSs[Scki].getSck().Connected == true)
                    if (sckAccept[Scki].Connected == true)
                    {                      
                        //SckSs[Scki].getSck().Receive(clientData);
                        sckAccept[Scki].Receive(clientData);
                        BinaryFormatter bf = new BinaryFormatter();
                        MemoryStream stream = new MemoryStream(clientData);
                        SQLiteDataReader sqlite_datareader;
                        object obj = bf.Deserialize(stream);
                        if (obj.GetType() == vec[Scki - 1].GetType())
                        {
                            vec[Scki - 1] = (VEClient)obj;
                            //lblclientComputerName.Text = vec.getComputerName();
                            lblClientIpAddress.Text = vec[Scki - 1].getIpAddress();
                            listBox1.Items.Add(vec[Scki - 1].getComputerName());
                            //讀取SQLite
                            /*SQLiteConnection sqlite_conn = new SQLiteConnection();
                            SQLiteCommand sqlite_cmd = new SQLiteCommand();
                            
                            bool isRepeated = true;
                            #region 檢查重複
                            
                            if (vec[Scki - 1].getCommand() == "檢查重複")
                            {
                                sqlite_conn = new SQLiteConnection("Data source=" + SQLitePath.path);
                                sqlite_conn.Open();
                                sqlite_cmd = sqlite_conn.CreateCommand();
                                sqlite_cmd.CommandText = "SELECT * FROM UsersData";
                                sqlite_datareader = sqlite_cmd.ExecuteReader();
                                while (sqlite_datareader.Read())
                                {
                                    if (sqlite_datareader["User"].ToString() == vec[Scki - 1].getUserName())
                                    {
                                        vec[Scki - 1].setCommand("重複");
                                        Serialize(vec[Scki - 1], Scki);
                                        isRepeated = true;
                                        break;
                                    }
                                    isRepeated = false;
                                }
                                if (!isRepeated)
                                {
                                    vec[Scki - 1].setCommand("可用");
                                    Serialize(vec[Scki - 1], Scki);
                                }
                                sqlite_datareader.Close();
                            }
                            #endregion
                            #region 註冊
                            else if(vec[Scki - 1].getCommand()=="註冊")
                            {
                                sqlite_conn = new SQLiteConnection("Data source=" + SQLitePath.path);
                                sqlite_conn.Open();
                                sqlite_cmd = sqlite_conn.CreateCommand();
                                sqlite_cmd.CommandText = "SELECT * FROM UsersData";
                                sqlite_cmd.ExecuteNonQuery();
                                sqlite_datareader = sqlite_cmd.ExecuteReader();
                                sqlite_cmd.CommandText = "INSERT INTO UsersData (User,Password,Name) VALUES('" + vec[Scki - 1].getUserName() + "','" + vec[Scki - 1].getPassword() + "','" + vec[Scki - 1].getName() + "')";
                                sqlite_cmd.ExecuteNonQuery();
                                sqlite_datareader.Close();
                            }
                            #endregion
                            #region 登入
                            else
                            {
                                sqlite_conn = new SQLiteConnection("Data source=" + SQLitePath.path);
                                sqlite_conn.Open();
                                sqlite_cmd = sqlite_conn.CreateCommand();
                                sqlite_cmd.CommandText = "SELECT * FROM UsersData";
                                sqlite_datareader = sqlite_cmd.ExecuteReader();
                                bool isCorrceted = true;
                                while (sqlite_datareader.Read())
                                {
                                    if (sqlite_datareader["User"].ToString() == vec[Scki - 1].getUserName())
                                    {
                                        if (sqlite_datareader["Password"].ToString() == vec[Scki - 1].getPassword())
                                        {
                                            vec[Scki - 1].setCommand("正確");
                                            Serialize(vec[Scki - 1], Scki);
                                            isCorrceted = true;
                                            break;
                                        }
                                    }
                                    isCorrceted = false;
                                   
                                }
                                if (!isCorrceted)
                                {
                                    sckAccept[Scki].Close();
                                }
                                sqlite_datareader.Close();
                                
                            }
                            #endregion*/
                        }
                        else if (obj.GetType() == dlPath.GetType())
                        {
                            dlPath = (DownloadPath)obj;
                            sendMyExcelCollection(dlPath.getPath(), Scki);
                        }

                    }
                }
            }
            catch (Exception e) 
            { 
                //MessageBox.Show(e.Message);
            }
        }
        void sendMyExcelCollection(string path,int scki)
        {
            question = openExam.open(path);
            Serialize(question, scki);
        }


        void openExcel(string path)
        {
            myExcel = new Microsoft.Office.Interop.Excel.Application();
            myExcel.Workbooks.Open(path);
            myExcel.DisplayAlerts = false;
            myExcel.Visible = false;
            myBook = myExcel.Workbooks[1];
            myBook.Activate();
            mySheet = (_Worksheet)myBook.Worksheets[1];
            mySheet.Activate();

        }
        void readExcel()
        {

            int count = 1;
            string raws = "A" + count;
            myRange = mySheet.get_Range(raws);
            Array.Resize(ref question, 1);
            while (Convert.ToString(myRange.Value) != null)
            {
                raws = "A" + count;
                myRange = mySheet.get_Range(raws);
                string q = Convert.ToString(myRange.Value);

                raws = "B" + count;
                myRange = mySheet.get_Range(raws);
                string a = Convert.ToString(myRange.Value);

                raws = "C" + count;
                myRange = mySheet.get_Range(raws);
                string b = Convert.ToString(myRange.Value);

                raws = "D" + count;
                myRange = mySheet.get_Range(raws);
                string c = Convert.ToString(myRange.Value);
                raws = "E" + count;
                myRange = mySheet.get_Range(raws);
                string d = Convert.ToString(myRange.Value);

                raws = "F" + count;
                myRange = mySheet.get_Range(raws);
                string ans = Convert.ToString(myRange.Value);
                question[count - 1] = new MyExcelCollection(q, a, b, c, d, ans);
                Array.Resize(ref question, question.Length + 1);

                raws = "A" + ++count;
                myRange = mySheet.get_Range(raws);
            }
            Array.Resize(ref question, question.Length - 1);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(myExcel);
            myBook = null;
            mySheet = null;
            myRange = null;
            myExcel = null;
            GC.Collect();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            foreach(Socket s in sckAccept)
            {
                    s.Close();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            online = 0;
            onlineClient.SetOnlineClient(0);
            listBox2.Items.Clear();
            for(int i=1;i<SckSs.Length-1;i++)
            {
                string s = "SocketIndex:" + i + " Connected:" + sckAccept[i].Connected + vec[i - 1].getUserName();
                listBox2.Items.Add(s);
                if (sckAccept[i].Connected)
                    online++;
            }
            lblSckIndex.Text = Convert.ToString(SckCIndex);
           
            lblOnline.Text = Convert.ToString(online);
            onlineClient.SetOnlineClient(online);
            OnlineClientBroadcast();
        }

        public void Serialize(MyExcelCollection[] m,int scki)
        {
            BinaryFormatter bf = new BinaryFormatter();
            MemoryStream stream = new MemoryStream();
            bf.Serialize(stream, m);
            byte[] bytesSend = new byte[1024];
            bytesSend = stream.ToArray();
            sckAccept[scki].Send(bytesSend);
        }
        public void Serialize(VEClient vec,int scki)
        {
            BinaryFormatter bf = new BinaryFormatter();
            MemoryStream stream = new MemoryStream();
            bf.Serialize(stream, vec);
            byte[] bytesSend = new byte[1024];
            bytesSend = stream.ToArray();
            sckAccept[scki].Send(bytesSend);
        }
        public void OnlineClientBroadcast()
        {
            for(int i=1;i<sckAccept.Length-1;i++)
            {
                if (sckAccept[i].Connected)
                    Serialize(onlineClient,i);
            }
        }
        public void Serialize(OC oc,int scki)
        {
            /*BinaryFormatter bf = new BinaryFormatter();
            MemoryStream stream = new MemoryStream();
            bf.Serialize(stream, oc);
            byte[] bytesSend = new byte[1024];
            bytesSend = stream.ToArray();
            sckAccept[scki].Send(bytesSend);*/
        }
    }
}
