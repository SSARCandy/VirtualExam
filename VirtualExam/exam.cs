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

            enhanceQuestion[0] = new MyExcelCollection();
        }


        #region 變數宣告
        private RadioButton[] selections = new RadioButton[4]; // 放置選項
        private bool isLoadQuestion = false;  // 判斷是否已載入題庫
        private int examIndex = 0;      // 目前題號
        public int TIME = 900;    // 倒數計時時間 
        private int timeCount;
        private Timer time;
        public static bool enhanceMode = false;//是否為加強練習模式
        public int questionAmt = 0;//練習題數
        #endregion
        public MyExcelCollection[] question = null;
        public MyExcelCollection[] enhanceQuestion = new MyExcelCollection[1];
        private void btnNext_Click(object sender, EventArgs e)
        {
            exam(++examIndex);
            if (examIndex == question.Length - 1 || examIndex == enhanceQuestion.Length - 1)
            {
                    btnNext.Enabled = false;
            }
            else if(questionAmt != 0)
            {
                if (examIndex>=questionAmt-1)
                {
                    btnNext.Enabled = false;
                }
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
            if (questionAmt==0)//預設值=0，則練習全部
            {
                if (i < question.Length && !enhanceMode)
                {
                    #region 正常模式
                    lblQuestion.Text = question[i].getQuestion();
                    radioButton1.Text = "(A) " + question[i].getAnswerA();
                    radioButton2.Text = "(B) " + question[i].getAnswerB();
                    radioButton3.Text = "(C) " + question[i].getAnswerC();
                    radioButton4.Text = "(D) " + question[i].getAnswerD();
                    usersAnswer();
                    mark();
                    r = true;//回覆狀態正常
                    #endregion
                }
                else if (i < enhanceQuestion.Length)
                {
                    #region 加強模式
                    lblQuestion.Text = enhanceQuestion[i].getQuestion();
                    radioButton1.Text = "(A) " + enhanceQuestion[i].getAnswerA();
                    radioButton2.Text = "(B) " + enhanceQuestion[i].getAnswerB();
                    radioButton3.Text = "(C) " + enhanceQuestion[i].getAnswerC();
                    radioButton4.Text = "(D) " + enhanceQuestion[i].getAnswerD();
                    usersAnswer();
                    mark();
                    r = true;//回覆狀態正常
                    #endregion
                }
            }else//非預設值，則練習使用者選擇的題數
            {
                label5.Text = Convert.ToString(questionAmt);
                if (i < question.Length && !enhanceMode&&i<questionAmt)
                {
                    #region 正常模式
                    lblQuestion.Text = question[i].getQuestion();
                    radioButton1.Text = "(A) " + question[i].getAnswerA();
                    radioButton2.Text = "(B) " + question[i].getAnswerB();
                    radioButton3.Text = "(C) " + question[i].getAnswerC();
                    radioButton4.Text = "(D) " + question[i].getAnswerD();
                    usersAnswer();
                    mark();
                    r = true;//回覆狀態正常
                    #endregion
                }
                else if (i < enhanceQuestion.Length&&i<questionAmt)
                {
                    #region 加強模式
                    lblQuestion.Text = enhanceQuestion[i].getQuestion();
                    radioButton1.Text = "(A) " + enhanceQuestion[i].getAnswerA();
                    radioButton2.Text = "(B) " + enhanceQuestion[i].getAnswerB();
                    radioButton3.Text = "(C) " + enhanceQuestion[i].getAnswerC();
                    radioButton4.Text = "(D) " + enhanceQuestion[i].getAnswerD();
                    usersAnswer();
                    mark();
                    r = true;//回覆狀態正常
                    #endregion
                }

            }
            return r;
        }
        public void Enhance()
        {
            int eh = 1;//紀錄不熟題目題數

            for (int j = 0; j < question.Length; j++)
            {
                if (question[j].getEnhance())
                {

                    Array.Resize(ref enhanceQuestion, eh);
                    enhanceQuestion[eh - 1] = question[j];
                    eh++;
                }

            }
            lblQuestion.Text = enhanceQuestion[0].getQuestion();
            radioButton1.Text = "(A) " + enhanceQuestion[0].getAnswerA();
            radioButton2.Text = "(B) " + enhanceQuestion[0].getAnswerB();
            radioButton3.Text = "(C) " + enhanceQuestion[0].getAnswerC();
            radioButton4.Text = "(D) " + enhanceQuestion[0].getAnswerD();
            usersAnswer();
            mark();
        }
        // ???紀錄作答完畢後需要檢查之題目
        private void mark()
        {
            if (enhanceMode)
            {
                #region 加強模式
                checkBox1.Checked = enhanceQuestion[examIndex].getMark();
                checkBox2.Checked = enhanceQuestion[examIndex].getEnhance();
                #endregion
            }
            else
            {
                #region 正常模式
                checkBox1.Checked = question[examIndex].getMark();
                checkBox2.Checked = question[examIndex].getEnhance();
                #endregion
            }
        }
        // 顯示答案之實做
        public void showAnswer()
        {
            if (enhanceMode)
            {
                #region 加強模式
                if (enhanceQuestion[examIndex].getShowAnswer())
                {
                    //顯示答案
                    foreach (RadioButton r in selections)
                    {
                        //去掉選項標頭 ex.(A) (B) (C) (D)
                        if (r.Text.Substring(4) == enhanceQuestion[examIndex].getAnswer())
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
                #endregion
            }
            else
            {
                #region 正常模式
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
                #endregion
            }
        }
        // 控制 CheckBox 勾選狀態
        public void usersAnswer()
        {
            if (enhanceMode)
            {
                #region 加強模式
                if (enhanceQuestion[examIndex].getUsersAnswer() != "")
                {
                    if (enhanceQuestion[examIndex].getUsersAnswer() == "A")
                        radioButton1.Checked = true;
                    else if (enhanceQuestion[examIndex].getUsersAnswer() == "B")
                        radioButton2.Checked = true;
                    else if (enhanceQuestion[examIndex].getUsersAnswer() == "C")
                        radioButton3.Checked = true;
                    else if (enhanceQuestion[examIndex].getUsersAnswer() == "D")
                        radioButton4.Checked = true;
                }
                else
                {
                    radioButton1.Checked = false;
                    radioButton2.Checked = false;
                    radioButton3.Checked = false;
                    radioButton4.Checked = false;
                }
                #endregion
            }
            else
            {
                #region 正常模式

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
                #endregion
            }
        }
        // 使用者選答案
        private void userAnswer_CheckedChanged(object sender, EventArgs e)
        {
            // NullLoadFileException();
            if (enhanceMode)
            {
                #region 加強模式使用者作答情形
                if (radioButton1.Checked)
                {
                    enhanceQuestion[examIndex].setUsersAnswer("A");
                }
                else if (radioButton2.Checked)
                {
                    enhanceQuestion[examIndex].setUsersAnswer("B");
                }
                else if (radioButton3.Checked)
                {
                    enhanceQuestion[examIndex].setUsersAnswer("C");
                }
                else if (radioButton4.Checked)
                {
                    enhanceQuestion[examIndex].setUsersAnswer("D");
                }
                #endregion
            }
            else
            {
                #region 正常模式使用者作答情形
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
                #endregion
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (enhanceMode)
            {
                enhanceQuestion[examIndex].setMark(checkBox1.Checked);
            }
            else
            {
                question[examIndex].setMark(checkBox1.Checked);
            }
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
            if (enhanceMode)
            {
                if (!enhanceQuestion[examIndex].getShowAnswer())
                {
                    enhanceQuestion[examIndex].setShowAnswer(true);
                    showAnswer();
                    btnAns.Text = "隱藏答案";
                }
                else
                {
                    enhanceQuestion[examIndex].setShowAnswer(false);
                    showAnswer();
                    btnAns.Text = "顯示答案";
                }
            }
            else
            {
                #region 正常模式
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
                #endregion
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

        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (enhanceMode)
            {
                enhanceQuestion[examIndex].setEnhance(checkBox2.Checked);
            }
            else
            {
                question[examIndex].setEnhance(checkBox2.Checked);
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            SaveExam se = new SaveExam();
            int eh = 0;
            for (int i = 0; i < question.Length; i++)
            {
                //重置使用者作答答案
                question[i].setUsersAnswer("");
                //重置顯示答案狀態
                question[i].setShowAnswer(false);
                //同步加強練習之陣列與正常模式之陣列
                if (eh < enhanceQuestion.Length && question[i].getQuestion() == enhanceQuestion[eh].getQuestion())
                {
                    question[i].setEnhance(enhanceQuestion[eh].getEnhance());
                    eh++;
                }
            }
            //儲存正常模式之陣列
            se.save(question, question[0].getExamName());
        }

        private void turnIn_Click(object sender, EventArgs e)
        {
            Score s = new Score();
            if (questionAmt == 0)
            {
                if (enhanceMode)
                {
                    int correct = 0;
                    foreach (MyExcelCollection m in enhanceQuestion)
                    {
                        if (m.compareAns())
                            correct++;

                    }
                    s.setUsersScore(correct * 100 / enhanceQuestion.Length);
                }
                else
                {
                    int correct = 0;
                    foreach (MyExcelCollection m in question)
                    {
                        if (m.compareAns())
                            correct++;

                    }
                    s.setUsersScore(correct * 100 / question.Length);
                }
            }
            else
            {
                if (enhanceMode)
                {
                    int correct = 0;
                    foreach (MyExcelCollection m in enhanceQuestion)
                    {
                        if (m.compareAns())
                            correct++;

                    }
                    s.setUsersScore(correct * 100 / questionAmt);
                }
                else
                {
                    int correct = 0;
                    foreach (MyExcelCollection m in question)
                    {
                        if (m.compareAns())
                            correct++;

                    }
                    s.setUsersScore(correct * 100 / questionAmt);
                }

                
            }
            s.Show();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if(questionAmt!=0)
            {
                label5.Text = Convert.ToString(questionAmt);
            }
            else if (enhanceMode)
            {
                if (enhanceQuestion != null)
                    label5.Text = Convert.ToString(enhanceQuestion.Length);
            }
            else
            {
                if (question != null)
                    label5.Text = Convert.ToString(question.Length);
            }
            label3.Text = Convert.ToString(examIndex+1);
        }
        public void setExamIndex(int examIndex)
        {
            this.examIndex = examIndex;
        }
        public void setQuestionAmt(int questionAmt)
        {
            this.questionAmt = questionAmt;
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            foreach(MyExcelCollection m in question)
            {
                m.setEnhance(checkBox3.Checked);
            }
        }

        public void Show()
        {
            btnPrevious.Enabled = false;
            btnNext.Enabled = true;
            this.Visible = true;
        }
    }
}