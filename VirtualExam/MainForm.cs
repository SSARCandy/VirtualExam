using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Office.Interop.Excel;
using myexcelcollection;
using vesocket;
using veclient;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using OnlineClient;

namespace VirtualExam
{
    public partial class MainForm : Form
    {
        #region 變數宣告
        private RadioButton[]            selections          = new RadioButton[4]; // 放置選項
        private Timer                    time;
        private int                      timeCount;
        private bool                     isLoadQuestion      = false;  // 判斷是否已載入題庫
        public static bool askDownload = false;//是否要求下載
        private int                      examIndex           = 0;      // 目前題號
        public int                       TIME                = 7200;    // 倒數計時時間 
        public _Application              myExcel;
        public _Workbook                 myBook;
        public _Worksheet                mySheet;
        public Range                     myRange;
        public MyExcelCollection[]       question;
        public VESocket veSocket;                // 建立連線並傳送相關資料

        private SaveExam saveExam;
        private OpenExam openExam;

        ExamForm ef;
        DownloadForm dlf;
        #endregion

        public MainForm()
        {
            InitializeComponent();
            Initialize();
        }
        
        // 初始化
        private void Initialize()
        {
            isLoadQuestion = false;
            myBook         = null;
            myExcel        = null;
            mySheet        = null;
            myRange        = null;
            question       = null;

            //把選項存入array
            /*selections[0] = radioButton1;
            selections[1] = radioButton2;
            selections[2] = radioButton3;
            selections[3] = radioButton4;*/

            timeCount       = TIME; //預設15分鐘
            time            = new Timer();
            time.Interval   = 1000;
            time.Tick       += new EventHandler(time_Tick);

            saveExam = new SaveExam();
            openExam = new OpenExam();

            ef = new ExamForm();
            dlf = new DownloadForm();

            //veSocket = new VESocket(SignIn.IP);
            veSocket = new VESocket("219.85.200.148");
            toolStripStatusLabel2.Text = "連線中...";
            AddDownloadedExam();

        }

        // 倒數計時函式
        private void time_Tick(object sender, EventArgs e)
        {
            TimeSpan t = TimeSpan.FromSeconds(--timeCount);
            //lbTimeCounter.Text = string.Format("{0:D2}:{1:D2}:{2:D2}", t.Hours, t.Minutes, t.Seconds);
            ef.lbTimeCounter.Text = string.Format("{0:D2}:{1:D2}:{2:D2}", t.Hours, t.Minutes, t.Seconds);
            if(timeCount == 0)
            {
                time.Stop();
                MessageBox.Show("時間到!");
            }
        } 

        public void openExcel(string path)
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
        public void readExcel()
        {
            int count = 1;
            string raws = "A" + count;
            myRange = mySheet.get_Range(raws);
            Array.Resize(ref question, 1);
            while (Convert.ToString(myRange.Value)!=null)
            {
                label1.Text = Convert.ToString(question.Length);//載入題數
                raws = "A" + count;
                myRange = mySheet.get_Range(raws);
                //string q = Convert.ToString(myRange.Value);由於50%會變成0.5 故改用myRange.Text
                string q = myRange.Text;

                raws = "B" + count;
                myRange = mySheet.get_Range(raws);
                //string a = Convert.ToString(myRange.Value);
                string a = myRange.Text;

                raws = "C" + count;
                myRange = mySheet.get_Range(raws);
                //string b = Convert.ToString(myRange.Value);
                string b = myRange.Text;

                raws = "D" + count;
                myRange = mySheet.get_Range(raws);
                //string c = Convert.ToString(myRange.Value);
                string c =myRange.Text;

                raws = "E" + count;
                myRange = mySheet.get_Range(raws);
                //string d = Convert.ToString(myRange.Value);
                string d = myRange.Text;

                raws = "F" + count;
                myRange = mySheet.get_Range(raws);
                //string ans = Convert.ToString(myRange.Value);
                string ans = myRange.Text;

                question[count-1] = new MyExcelCollection(q, a, b, c, d, ans);
                Array.Resize(ref question, question.Length + 1);

                raws = "A" + ++count;
                myRange = mySheet.get_Range(raws);
                //label5.Text = Convert.ToString(count - 1);
            }
            Array.Resize(ref question, question.Length - 1);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(myExcel);
            myBook = null;
            mySheet = null;
            myRange = null;
            myExcel = null;
            GC.Collect();
        }
        public void writeExcel()
        {
            myRange = mySheet.get_Range("A1");
            myRange.Value = "test";
        }

        // 處理例外狀況--未開啟題庫
        private void NullLoadFileException()
        {
            if (!isLoadQuestion)
            {
                MessageBox.Show("請先開啟題庫");
                button1_Click(null, null);
            }
            else
                isLoadQuestion = true;
        }
      
        // 顯示本題 題目及選項
        /*private bool exam(int i)
        {
            bool r = false; 
            if (i < question.Length)
            {
                lblQuestion.Text = question[i].getQuestion();
                radioButton1.Text = "(A) " + question[i].getAnswerA();
                radioButton2.Text = "(B) " + question[i].getAnswerB();
                radioButton3.Text = "(C) " + question[i].getAnswerC();
                radioButton4.Text = "(D) " + question[i].getAnswerD();
                usersAnswer();
                mark();
                r = true;

                ef.lblQuestion.Text = question[i].getQuestion();
                ef.radioButton1.Text = "(A) " + question[i].getAnswerA();
                ef.radioButton2.Text = "(B) " + question[i].getAnswerB();
                ef.radioButton3.Text = "(C) " + question[i].getAnswerC();
                ef.radioButton4.Text = "(D) " + question[i].getAnswerD();
                usersAnswer();
                mark();
                r = true;
            }
            return r;
        }*/

        // ???紀錄作答完畢後需要檢查之題目

        // 顯示答案之實做
       /* private void showAnswer()
        {
            if(question[examIndex].getShowAnswer())
            { 
                //顯示答案
                foreach (RadioButton r in selections)
                {
                 //去掉選項標頭 ex.(A) (B) (C) (D)
                 if (r.Text.Substring(4) == question[examIndex].getAnswer())
                       r.ForeColor = Color.Green;
                 else
                      r.ForeColor = Color.Red;
                }
            }
            else
            {
                //把選項顏色恢復 黑色
                foreach (RadioButton r in selections)
                   r.ForeColor = Color.Black;
                btnAns.Text = "顯示答案";
            }
        }*/

        // 控制 CheckBox 勾選狀態
        /*private void usersAnswer()
        {
            if (question[examIndex].getUsersAnswer() != "")
            {
                if (question[examIndex].getUsersAnswer() == "A")
                    radioButton1.Checked = true;
                else if (question[examIndex].getUsersAnswer() == "B")
                    radioButton2.Checked = true;
                else if (question[examIndex].getUsersAnswer() == "C")
                    radioButton3.Checked = true;
                else if (question[examIndex].getUsersAnswer() == "D")
                    radioButton4.Checked = true;
            }
            else
            {
                radioButton1.Checked = false;
                radioButton2.Checked = false;
                radioButton3.Checked = false;
                radioButton4.Checked = false;
            }            
        }*/

        #region 使用者控制項

        private void download()
        {
            //傳遞下載路徑
            veSocket.Download(DownloadForm.dlExam);            
            //veSocket中的MyExcelCollection物件不為空，表示已成功由遠端伺服器下載題庫至該物件內
            if (veSocket.getMyExcelCollection() != null)
            {
                //label1.Text = "題庫：" + DownloadForm.dlExam;
                //將該題庫放入用戶端MyExcelCollection物件
                question = veSocket.getMyExcelCollection();
                //label5.Text = Convert.ToString(question.Length);
                //exam(0);
                question[0].setExamName(dlf.getExamName());
                saveExam.save(question,dlf.getExamName());
                Form.CheckForIllegalCrossThreadCalls = false;
                dlf.lblStat.Text = "狀態：下載完成!!";
            }
        }

        // 開啟題庫
        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog file = new OpenFileDialog();
            if (file.ShowDialog() == DialogResult.OK)
            {
                openExcel(file.FileName);
                //label1.Text = "題庫：" + file.SafeFileName;
                readExcel();
                //exam(0);

                isLoadQuestion = true;
           
                // 開新題庫重新計時
                /*timeCount = TIME;
                time.Start();*/
            }

        }

        

        // 跳題
        private void button2_Click(object sender, EventArgs e)
        {
            NullLoadFileException();
            if (textBox1.Text == "")
                MessageBox.Show("題號不可為空!");
            else
            {
                examIndex = Convert.ToInt16(textBox1.Text) - 1;
                //exam(examIndex);
                //label3.Text = Convert.ToString((Convert.ToInt16(textBox1.Text)));
            }
        }

        #endregion

        private void MainForm_Load(object sender, EventArgs e)
        {
            
        }
        private void MainForm_Closing(object sender, FormClosingEventArgs e)
        {
            if(time.Enabled == true)
                time.Stop();
            //veSocket.getSck().Shutdown(System.Net.Sockets.SocketShutdown.Both);
            (veSocket.getSck()).Close();
        }

        private void SetExam_Click(object sender, EventArgs e)
        {
            if(listBox1.SelectedIndex==-1)
            {
                MessageBox.Show("請先選擇題庫");
            }
            else 
            { 
            SetExamForm w = new SetExamForm();
            if (w.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {

                //this.TIME = w.Time;

                /*if (time.Enabled == true)
                    time.Stop();
                timeCount = this.TIME;
                time.Start();*/
                ef.TIME = w.Time;
                question = openExam.open(listBox1.SelectedItem.ToString());
                if (w.checkBox2.Checked)
                {
                    RandomQuestion();
                }
                string s = (string)listBox1.SelectedItem;
                ef.question = null;
                ef.question = this.question;

                ef.exam(0);
                ef.setExamIndex(0);
                if (ExamForm.enhanceMode)
                {
                    ef.Enhance();
                }
                if (w.comboBox2.Text != "練習全部")
                    ef.setQuestionAmt(Convert.ToInt16(w.comboBox2.Text));
                else
                    ef.setQuestionAmt(0);

                ef.Show();
                ef.question[0].setUsersAnswer("");

            }
            }
        }

        void RandomQuestion()
        {
            Random r = new Random();
            
            for (int i = 0; i < question.Length; i++)
            {
                int r1 = r.Next(0, question.Length);
                MyExcelCollection m = question[i]; question[i] = question[r1]; question[r1] = m;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            saveExam.save(question,"證券商業務員");
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            question = openExam.open("證券商業務員");
            isLoadQuestion = true;            
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void 下載題庫ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            dlf.Show();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (askDownload)
            {
                System.Threading.Thread dlTd = new System.Threading.Thread(download);
                dlTd.Start();
                //download();
                askDownload = false;
            }

            if (veSocket.getSck() != null)
            {
                if (veSocket.getSck().Connected)
                {
                    toolStripStatusLabel2.Text = "已連線";
                }
                else
                {
                    toolStripStatusLabel2.Text = "中斷連線";
                }
            }
            //toolStripStatusLabel4.Text = veSocket.GetOC() + "人";
        }
        void AddDownloadedExam()
        {
            //自動載入已下載題庫
            listBox1.Items.Clear();
            string examPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "/Exams";
            if (Directory.Exists(examPath))
            {
                string[] examName = Directory.GetFiles(examPath);
                foreach (string s in examName)
                {
                    listBox1.Items.Add(Path.GetFileNameWithoutExtension(s));
                }
            }
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            //AddDownloadedExam();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            AddDownloadedExam();
        }

        private void 關於我們ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        //將圖片儲存到題目裡面
        private void button4_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if(ofd.ShowDialog()==DialogResult.OK)
            {
                question[0].setImage(Image.FromStream(new FileStream(ofd.FileName, FileMode.Open, FileAccess.Read, FileShare.Read)));
            }
        }
        //將圖片從題目中讀出(未知錯誤，可能是bitmap無法序列化)
        private void button5_Click(object sender, EventArgs e)
        {
            object obj = question[0].getImage();

            Bitmap bitmap = (Bitmap)obj;
            Graphics tmpG = Graphics.FromImage(bitmap);
            tmpG.DrawImage(bitmap,
                               new System.Drawing.Rectangle(0, 0, 446, 387),
                               new System.Drawing.Rectangle(0, 0, bitmap.Width, bitmap.Height),
                               GraphicsUnit.Pixel);

            //pictureBox1.Image = bitmap;
        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            SetExam_Click(null, null);
        }

        private void toolStripStatusLabel1_Click(object sender, EventArgs e)
        {

        }

    }
}
