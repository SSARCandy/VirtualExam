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

namespace VirtualExam
{
    public partial class Form1 : Form
    {
        private RadioButton[] selections = new RadioButton[4]; 
        _Application myExcel;
        _Workbook myBook;
        _Worksheet mySheet;
        Range myRange;
        myExcelCollection[] question ;


        public Form1()
        {
            InitializeComponent();
            Initialize();
        }

        private void Initialize()
        {
            myBook = null;
            myExcel = null;
            mySheet = null;
            myRange = null;
            question = null;

            //把選項存入array
            selections[0] = radioButton1;
            selections[1] = radioButton2;
            selections[2] = radioButton3;
            selections[3] = radioButton4;
        } 
        int examIndex = 0;
        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog file = new OpenFileDialog();
            if (file.ShowDialog() == DialogResult.OK)
            {
                openExcel(file.FileName);
                label1.Text = "題庫：" + file.SafeFileName;
            }
            readExcel();
            exam(0);
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
                question[count-1] = new myExcelCollection(q, a, b, c, d, ans);
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
        void writeExcel()
        {
            myRange = mySheet.get_Range("A1");
            myRange.Value = "test";
        }
        bool exam(int i)
        {
            bool r=false;
            
            if (i < question.Length)
            {
                lblQuestion.Text = question[i].getQuestion();
                radioButton1.Text = question[i].getAnswerA();
                radioButton2.Text = question[i].getAnswerB();
                radioButton3.Text = question[i].getAnswerC();
                radioButton4.Text = question[i].getAnswerD();
                usersAnswer();
                mark();
                r = true;
            }

            
            return r;
        }



        private void treeView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
           
            openExcel("C:\\Users\\CYY\\Desktop\\題庫\\" + treeView1.SelectedNode.Name);
            label1.Text = "題庫：" + treeView1.SelectedNode.Text;
            readExcel();
            exam(examIndex);
        }


        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
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


        private void Form1_Load(object sender, EventArgs e)
        {
            radioButton1.CheckedChanged += new EventHandler(radioButton1_CheckedChanged);
            radioButton2.CheckedChanged += new EventHandler(radioButton1_CheckedChanged);
            radioButton3.CheckedChanged += new EventHandler(radioButton1_CheckedChanged);
            radioButton4.CheckedChanged += new EventHandler(radioButton1_CheckedChanged);
        }


        private void btnPrevious_Click(object sender, EventArgs e)
        {
            exam(--examIndex);
            if (examIndex == 0)
                btnPrevious.Enabled = false;
            if (!btnNext.Enabled)
                btnNext.Enabled = true;
            label3.Text = Convert.ToString(examIndex + 1);
       
            //把選像顏色恢復 黑色
            foreach (RadioButton r in selections)
                r.ForeColor = Color.Black;
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

            if(examIndex<question.Length)
                label3.Text = Convert.ToString(examIndex + 1);

            //把選像顏色恢復 黑色
            foreach (RadioButton r in selections)
                r.ForeColor = Color.Black;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            question[examIndex].setMark(checkBox1.Checked);
        }
        void usersAnswer()
        {
            if (question[examIndex].getUsersAnswer() != "")
            {
                if (question[examIndex].getUsersAnswer() == "A")
                {
                    radioButton1.Checked = true;
                }
                else if (question[examIndex].getUsersAnswer() == "B")
                {
                    radioButton2.Checked = true;
                }
                else if (question[examIndex].getUsersAnswer() == "C")
                {
                    radioButton3.Checked = true;
                }
                else if (question[examIndex].getUsersAnswer() == "D")
                {
                    radioButton4.Checked = true;
                }
            }
            else
            {
                radioButton1.Checked = false;
                radioButton2.Checked = false;
                radioButton3.Checked = false;
                radioButton4.Checked = false;
            }
        }
        void mark()
        {
            checkBox1.Checked = question[examIndex].getMark();
        }

        /*
         * 顯示答案 :  正解 綠色 ; 其餘 紅色 
         * 顯示答案時間僅持續一題
         */
        private void btnAns_Click(object sender, EventArgs e)
        {
            foreach (RadioButton r in selections)
            {
                if (r.Text == question[examIndex].getAnswer())
                    r.ForeColor = Color.Green;
                else
                    r.ForeColor = Color.Red;
            }
            /*
            if (radioButton1.Text == question[examIndex].getAnswer())
                radioButton1.ForeColor = Color.Green;
            else
                radioButton1.ForeColor=Color.Red;

            if (radioButton2.Text == question[examIndex].getAnswer())
                radioButton2.ForeColor = Color.Green;
            else
                radioButton2.ForeColor = Color.Red;

            if (radioButton3.Text == question[examIndex].getAnswer())
                radioButton3.ForeColor = Color.Green;
            else
                radioButton3.ForeColor = Color.Red;

            if (radioButton4.Text == question[examIndex].getAnswer())
                radioButton4.ForeColor = Color.Green;
            else
                radioButton4.ForeColor = Color.Red;
             */
        }

        private void button2_Click(object sender, EventArgs e)
        {
            examIndex = Convert.ToInt16(textBox1.Text) - 1;
            exam(examIndex);
            label3.Text = Convert.ToString((Convert.ToInt16(textBox1.Text)));
        }
    }
}
