using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using myexcelcollection;

namespace VirtualExam
{
    public partial class ExamForm : Form
    {
        public ExamForm()
        {
            InitializeComponent();
            Initialize();
        }

        private void Initialize()
        {
            //把選項存入array
            selections[0] = radioButton1;
            selections[1] = radioButton2;
            selections[2] = radioButton3;
            selections[3] = radioButton4;

            timeCount = TIME; //預設15分鐘
            time = new Timer();
            time.Interval = 1000;
            time.Tick += new EventHandler(time_Tick);

            radioButton1.CheckedChanged += new EventHandler(userAnswer_CheckedChanged);
            radioButton2.CheckedChanged += new EventHandler(userAnswer_CheckedChanged);
            radioButton3.CheckedChanged += new EventHandler(userAnswer_CheckedChanged);
            radioButton4.CheckedChanged += new EventHandler(userAnswer_CheckedChanged);
        }


        #region 變數宣告
        private RadioButton[] selections = new RadioButton[4]; // 放置選項
        private bool isLoadQuestion = false;  // 判斷是否已載入題庫
        private int examIndex = 0;      // 目前題號
        public int TIME = 900;    // 倒數計時時間 
        private int timeCount;
        private Timer time;

        public static bool enhanceMode = false;//是某為加強練習模式
        #endregion
        public MyExcelCollection[] question=null;
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
        // 倒數計時函式
        private void time_Tick(object sender, EventArgs e)
        {
            TimeSpan t = TimeSpan.FromSeconds(--timeCount);
            lbTimeCounter.Text = string.Format("{0:D2}:{1:D2}:{2:D2}", t.Hours, t.Minutes, t.Seconds);
            //ef.lbTimeCounter.Text = string.Format("{0:D2}:{1:D2}:{2:D2}", t.Hours, t.Minutes, t.Seconds);
            if (timeCount == 0)
            {
                time.Stop();
                MessageBox.Show("時間到!");
            }
        }
        public bool exam(int i)
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
                int eh = 0;//紀錄不熟題目題數
                while (enhanceMode && examIndex < question.Length-1)
                {
                    foreach(MyExcelCollection m in question)
                    {
                        if (m.getEnhance())
                            eh++;
                    }
                    MessageBox.Show(Convert.ToString(eh));
                    if (!question[i].getEnhance())
                    {
                        exam(++examIndex);
                    }
                    else
                    {
                        break;
                    }
                }
            }
            return r;
        }

        // ???紀錄作答完畢後需要檢查之題目
        private void mark()
        {
            checkBox1.Checked = question[examIndex].getMark();
            checkBox2.Checked = question[examIndex].getEnhance();
        }
        // 顯示答案之實做
        public void showAnswer()
        {
            if (question[examIndex].getShowAnswer())
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
        // 使用者選答案
        private void userAnswer_CheckedChanged(object sender, EventArgs e)
        {
           // NullLoadFileException();
            if (radioButton1.Checked)
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

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            question[examIndex].setMark(checkBox1.Checked);
        }

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

        private void btnAns_Click(object sender, EventArgs e)
        {
            if (!question[examIndex].getShowAnswer())
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

        private void ExamForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (time.Enabled == true)
                time.Stop();
            e.Cancel = true;
            this.Hide();
        }

        private void checkAns_Click(object sender, EventArgs e)
        {

        }

        private void ExamForm_Load(object sender, EventArgs e)
        {

        }

        private void ExamForm_Shown(object sender, EventArgs e)
        {
            radioButton1.Checked = false;
            radioButton2.Checked = false;
            radioButton3.Checked = false;
            radioButton4.Checked = false;
            label5.Text = Convert.ToString(question.Length);
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            question[examIndex].setEnhance(checkBox2.Checked);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SaveExam se = new SaveExam();
            se.save(question);
        }

        


    }
}
