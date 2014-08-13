﻿using System;
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


namespace VirtualExam
{
    public partial class MainForm : Form
    {
        #region 變數宣告
        private RadioButton[]            selections          = new RadioButton[4]; // 放置選項
        private Timer                    time;
        private int                      timeCount;
        private const int                TIME                = 900;    // 倒數計時時間 
        private bool                     isLoadQuestion      = false;  // 判斷是否已載入題庫
        private int                      examIndex           = 0;      // 目前題號
        public _Application              myExcel;
        public _Workbook                 myBook;
        public _Worksheet                mySheet;
        public Range                     myRange;
        public MyExcelCollection[]       question;
        public VESocket                  ves                = new VESocket(); // 建立連線並傳送相關資料
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
            selections[0] = radioButton1;
            selections[1] = radioButton2;
            selections[2] = radioButton3;
            selections[3] = radioButton4;

            timeCount       = TIME; //預設15分鐘
            time            = new Timer();
            time.Interval   = 1000;
            time.Tick       += new EventHandler(time_Tick);
        }

        // 倒數計時函式
        private void time_Tick(object sender, EventArgs e)
        {
            TimeSpan t = TimeSpan.FromSeconds(--timeCount);
            lbTimeCounter.Text = string.Format("{0:D2}:{1:D2}", t.Minutes, t.Seconds);
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
                question[count-1] = new MyExcelCollection(q, a, b, c, d, ans);
                Array.Resize(ref question, question.Length + 1);

                raws = "A" + ++count;
                myRange = mySheet.get_Range(raws);
                label5.Text = Convert.ToString(count - 1);
            }
            Array.Resize(ref question, question.Length - 1);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(myExcel);
            myBook = null;
            mySheet = null;
            myRange = null;
            myExcel = null;
            GC.Collect();
            btnNext.Enabled = true;
            btnAns.Enabled = true;
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
        private bool exam(int i)
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
            }
            return r;
        }

        // ???
        private void mark()
        {
            checkBox1.Checked = question[examIndex].getMark();
        }

        // 顯示答案之實做
        private void showAnswer()
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
            }
        }

        // 控制 CheckBox 勾選狀態
        private void usersAnswer()
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
        }

        #region 使用者控制項

        // 左側樹狀題庫操作
        private void treeView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ves.Download(treeView1.SelectedNode.Name);

            if(ves.getMyExcelCollection()!=null)
            {
                label1.Text = "題庫：" + treeView1.SelectedNode.Text;
                question = ves.getMyExcelCollection();
                label5.Text = Convert.ToString(question.Length);
                exam(0);

                isLoadQuestion = true;
                btnNext.Enabled = true;
                btnAns.Enabled = true;

                time.Start();
            }

            /*openExcel("C:\\Users\\CYY\\Desktop\\題庫\\" + treeView1.SelectedNode.Name);
            label1.Text = "題庫：" + treeView1.SelectedNode.Text;
            readExcel();
            exam(examIndex);
            isLoadQuestion = true;*/
        }

        // 開啟題庫
        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog file = new OpenFileDialog();
            if (file.ShowDialog() == DialogResult.OK)
            {
                openExcel(file.FileName);
                label1.Text = "題庫：" + file.SafeFileName;
                readExcel();
                exam(0);

                isLoadQuestion = true;
                time.Start();
            }

        }

        // 使用者選答案
        private void userAnswer_CheckedChanged(object sender, EventArgs e)
        {
            NullLoadFileException();
            if(radioButton1.Checked)
            {
                question[examIndex].setUsersAnswer("A");
            }
            else if (radioButton2.Checked)
            {
                question[examIndex].setUsersAnswer("B");
            }
            else if (radioButton3.Checked)
            {
                question[examIndex].setUsersAnswer("C");
            }
            else if (radioButton4.Checked)
            {
                question[examIndex].setUsersAnswer("D");
            }
        }

        // "檢查" 按鈕
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            question[examIndex].setMark(checkBox1.Checked);
        }

        // 上/下一題
        private void btnPrevious_Click(object sender, EventArgs e)
        {
            exam(--examIndex);
            if (examIndex == 0)
                btnPrevious.Enabled = false;
            if (!btnNext.Enabled)
                btnNext.Enabled = true;
            label3.Text = Convert.ToString(examIndex + 1);
            showAnswer();
        }
        private void btnNext_Click(object sender, EventArgs e)
        {

            exam(++examIndex);
            if (examIndex == question.Length - 1)
            {
                btnNext.Enabled = false;
            }
            if (examIndex != 0)
                btnPrevious.Enabled = true;
            label3.Text = Convert.ToString(examIndex + 1);

            showAnswer();
        }
     
        // 顯示答案 ( 顯示答案時間僅持續一題 )
        private void btnAns_Click(object sender, EventArgs e)
        {
            if(!question[examIndex].getShowAnswer())
            {
                question[examIndex].setShowAnswer(true);
                showAnswer();
                btnAns.Text = "隱藏答案";
            }
            else
            {
                question[examIndex].setShowAnswer(false);
                showAnswer();
                btnAns.Text = "顯示答案";
            }
        }

        // 跳題
        private void button2_Click(object sender, EventArgs e)
        {
            NullLoadFileException();
            if (textBox1.Text == "")
                MessageBox.Show("題號不可為空!");
            else
                examIndex = Convert.ToInt16(textBox1.Text) - 1; 

            exam(examIndex);
            label3.Text = Convert.ToString((Convert.ToInt16(textBox1.Text)));
        }

        // 打亂題目
        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            NullLoadFileException();
            if (checkBox2.Checked)
            {
                Random r = new Random();
                //int r1 = r.Next(0, question.Length - 1);
                for (int i = 0; i < question.Length; i++)
                {
                    int r1 = r.Next(0, question.Length - 1);
                    MyExcelCollection m = question[i]; question[i] = question[r1]; question[r1] = m;
                    exam(examIndex);
                }
            }
            
        }

        // 打亂選項
        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {

            NullLoadFileException();
            foreach (MyExcelCollection m in question)
                m.randOption();
            exam(examIndex);
        }

        #endregion

        private void MainForm_Load(object sender, EventArgs e)
        {
            radioButton1.CheckedChanged += new EventHandler(userAnswer_CheckedChanged);
            radioButton2.CheckedChanged += new EventHandler(userAnswer_CheckedChanged);
            radioButton3.CheckedChanged += new EventHandler(userAnswer_CheckedChanged);
            radioButton4.CheckedChanged += new EventHandler(userAnswer_CheckedChanged);
        }
        private void MainForm_Closing(object sender, FormClosingEventArgs e)
        {
            if(time.Enabled == true)
                time.Stop();
            
        }
    }
}
