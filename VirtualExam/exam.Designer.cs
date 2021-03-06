﻿namespace VirtualExam
{
    partial class ExamForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.lblQuestion = new System.Windows.Forms.Label();
            this.radioButton4 = new System.Windows.Forms.RadioButton();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.btnCheckAns = new System.Windows.Forms.Button();
            this.btnTurnIn = new System.Windows.Forms.Button();
            this.btnAns = new System.Windows.Forms.Button();
            this.btnPrevious = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.lbTimeCounter = new System.Windows.Forms.Label();
            this.lblCountdown = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.btnSaveStat = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.rdbFontSmall = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rdbFontBig = new System.Windows.Forms.RadioButton();
            this.rdbFontMid = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblQuestion
            // 
            this.lblQuestion.AutoEllipsis = true;
            this.lblQuestion.Location = new System.Drawing.Point(12, 50);
            this.lblQuestion.Name = "lblQuestion";
            this.lblQuestion.Size = new System.Drawing.Size(750, 200);
            this.lblQuestion.TabIndex = 3;
            this.lblQuestion.Text = "Question";
            // 
            // radioButton4
            // 
            this.radioButton4.Location = new System.Drawing.Point(12, 353);
            this.radioButton4.Name = "radioButton4";
            this.radioButton4.Size = new System.Drawing.Size(850, 16);
            this.radioButton4.TabIndex = 10;
            this.radioButton4.TabStop = true;
            this.radioButton4.Text = "answerD";
            this.radioButton4.UseVisualStyleBackColor = true;
            // 
            // radioButton3
            // 
            this.radioButton3.Location = new System.Drawing.Point(12, 331);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(850, 16);
            this.radioButton3.TabIndex = 9;
            this.radioButton3.TabStop = true;
            this.radioButton3.Text = "answerC";
            this.radioButton3.UseVisualStyleBackColor = true;
            // 
            // radioButton2
            // 
            this.radioButton2.Location = new System.Drawing.Point(12, 309);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(850, 16);
            this.radioButton2.TabIndex = 8;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "answerB";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // radioButton1
            // 
            this.radioButton1.ForeColor = System.Drawing.Color.Black;
            this.radioButton1.Location = new System.Drawing.Point(12, 287);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(850, 16);
            this.radioButton1.TabIndex = 7;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "answerA";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // btnCheckAns
            // 
            this.btnCheckAns.Location = new System.Drawing.Point(245, 375);
            this.btnCheckAns.Name = "btnCheckAns";
            this.btnCheckAns.Size = new System.Drawing.Size(75, 23);
            this.btnCheckAns.TabIndex = 29;
            this.btnCheckAns.Text = "檢查";
            this.btnCheckAns.UseVisualStyleBackColor = true;
            this.btnCheckAns.Visible = false;
            this.btnCheckAns.Click += new System.EventHandler(this.checkAns_Click);
            // 
            // btnTurnIn
            // 
            this.btnTurnIn.Location = new System.Drawing.Point(390, 375);
            this.btnTurnIn.Name = "btnTurnIn";
            this.btnTurnIn.Size = new System.Drawing.Size(75, 23);
            this.btnTurnIn.TabIndex = 28;
            this.btnTurnIn.Text = "交卷";
            this.btnTurnIn.UseVisualStyleBackColor = true;
            this.btnTurnIn.Click += new System.EventHandler(this.turnIn_Click);
            // 
            // btnAns
            // 
            this.btnAns.Location = new System.Drawing.Point(164, 376);
            this.btnAns.Name = "btnAns";
            this.btnAns.Size = new System.Drawing.Size(75, 23);
            this.btnAns.TabIndex = 27;
            this.btnAns.Text = "顯示答案";
            this.btnAns.UseVisualStyleBackColor = true;
            this.btnAns.Click += new System.EventHandler(this.btnAns_Click);
            // 
            // btnPrevious
            // 
            this.btnPrevious.Enabled = false;
            this.btnPrevious.Location = new System.Drawing.Point(2, 375);
            this.btnPrevious.Name = "btnPrevious";
            this.btnPrevious.Size = new System.Drawing.Size(75, 23);
            this.btnPrevious.TabIndex = 26;
            this.btnPrevious.Text = "上一題";
            this.btnPrevious.UseVisualStyleBackColor = true;
            this.btnPrevious.Click += new System.EventHandler(this.btnPrevious_Click);
            // 
            // btnNext
            // 
            this.btnNext.Location = new System.Drawing.Point(83, 376);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(75, 23);
            this.btnNext.TabIndex = 25;
            this.btnNext.Text = "下一題";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(783, 17);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(48, 16);
            this.checkBox1.TabIndex = 30;
            this.checkBox1.Text = "檢查";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.Visible = false;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // lbTimeCounter
            // 
            this.lbTimeCounter.AutoSize = true;
            this.lbTimeCounter.Location = new System.Drawing.Point(718, 21);
            this.lbTimeCounter.Name = "lbTimeCounter";
            this.lbTimeCounter.Size = new System.Drawing.Size(47, 12);
            this.lbTimeCounter.TabIndex = 32;
            this.lbTimeCounter.Text = "00:00:00";
            // 
            // lblCountdown
            // 
            this.lblCountdown.AutoSize = true;
            this.lblCountdown.Location = new System.Drawing.Point(617, 21);
            this.lblCountdown.Name = "lblCountdown";
            this.lblCountdown.Size = new System.Drawing.Size(89, 12);
            this.lblCountdown.TabIndex = 31;
            this.lblCountdown.Text = "剩餘作答時間：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(82, 9);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(11, 12);
            this.label5.TabIndex = 36;
            this.label5.Text = "1";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(76, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(8, 12);
            this.label4.TabIndex = 35;
            this.label4.Text = "/";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(48, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(31, 16);
            this.label3.TabIndex = 34;
            this.label3.Text = "1";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 33;
            this.label2.Text = "題目：";
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(783, 37);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(84, 16);
            this.checkBox2.TabIndex = 37;
            this.checkBox2.Text = "標記為不熟";
            this.checkBox2.UseVisualStyleBackColor = true;
            this.checkBox2.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);
            // 
            // btnSaveStat
            // 
            this.btnSaveStat.Location = new System.Drawing.Point(471, 375);
            this.btnSaveStat.Name = "btnSaveStat";
            this.btnSaveStat.Size = new System.Drawing.Size(89, 23);
            this.btnSaveStat.TabIndex = 38;
            this.btnSaveStat.Text = "儲存練習狀態";
            this.btnSaveStat.UseVisualStyleBackColor = true;
            this.btnSaveStat.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(326, 381);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 12);
            this.label1.TabIndex = 39;
            this.label1.Text = "<-- 未實做";
            this.label1.Visible = false;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // checkBox3
            // 
            this.checkBox3.AutoSize = true;
            this.checkBox3.Location = new System.Drawing.Point(783, 57);
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.Size = new System.Drawing.Size(72, 16);
            this.checkBox3.TabIndex = 40;
            this.checkBox3.Text = "標記全部";
            this.checkBox3.UseVisualStyleBackColor = true;
            this.checkBox3.Visible = false;
            this.checkBox3.CheckedChanged += new System.EventHandler(this.checkBox3_CheckedChanged);
            // 
            // rdbFontSmall
            // 
            this.rdbFontSmall.AutoSize = true;
            this.rdbFontSmall.Checked = true;
            this.rdbFontSmall.Location = new System.Drawing.Point(6, 22);
            this.rdbFontSmall.Name = "rdbFontSmall";
            this.rdbFontSmall.Size = new System.Drawing.Size(35, 16);
            this.rdbFontSmall.TabIndex = 41;
            this.rdbFontSmall.TabStop = true;
            this.rdbFontSmall.Text = "小";
            this.rdbFontSmall.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rdbFontBig);
            this.groupBox1.Controls.Add(this.rdbFontMid);
            this.groupBox1.Controls.Add(this.rdbFontSmall);
            this.groupBox1.Location = new System.Drawing.Point(800, 80);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(51, 88);
            this.groupBox1.TabIndex = 42;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "字體";
            // 
            // rdbFontBig
            // 
            this.rdbFontBig.AutoSize = true;
            this.rdbFontBig.Location = new System.Drawing.Point(6, 66);
            this.rdbFontBig.Name = "rdbFontBig";
            this.rdbFontBig.Size = new System.Drawing.Size(35, 16);
            this.rdbFontBig.TabIndex = 43;
            this.rdbFontBig.TabStop = true;
            this.rdbFontBig.Text = "大";
            this.rdbFontBig.UseVisualStyleBackColor = true;
            // 
            // rdbFontMid
            // 
            this.rdbFontMid.AutoSize = true;
            this.rdbFontMid.Location = new System.Drawing.Point(6, 44);
            this.rdbFontMid.Name = "rdbFontMid";
            this.rdbFontMid.Size = new System.Drawing.Size(35, 16);
            this.rdbFontMid.TabIndex = 42;
            this.rdbFontMid.TabStop = true;
            this.rdbFontMid.Text = "中";
            this.rdbFontMid.UseVisualStyleBackColor = true;
            // 
            // ExamForm
            // 
            this.AccessibleName = "";
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(884, 411);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.checkBox3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnSaveStat);
            this.Controls.Add(this.checkBox2);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lbTimeCounter);
            this.Controls.Add(this.lblCountdown);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.btnCheckAns);
            this.Controls.Add(this.btnTurnIn);
            this.Controls.Add(this.btnAns);
            this.Controls.Add(this.btnPrevious);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.radioButton4);
            this.Controls.Add(this.radioButton3);
            this.Controls.Add(this.radioButton2);
            this.Controls.Add(this.radioButton1);
            this.Controls.Add(this.lblQuestion);
            this.Name = "ExamForm";
            this.Text = "exam";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ExamForm_FormClosing);
            this.Load += new System.EventHandler(this.ExamForm_Load);
            this.Shown += new System.EventHandler(this.ExamForm_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ExamForm_KeyDown);
            this.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.ExamForm_PreviewKeyDown);
            this.Resize += new System.EventHandler(this.ExamForm_Resize);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCheckAns;
        private System.Windows.Forms.Button btnTurnIn;
        private System.Windows.Forms.Button btnAns;
        private System.Windows.Forms.CheckBox checkBox1;
        public System.Windows.Forms.Label lbTimeCounter;
        private System.Windows.Forms.Label lblCountdown;
        public System.Windows.Forms.Label lblQuestion;
        public System.Windows.Forms.RadioButton radioButton4;
        public System.Windows.Forms.RadioButton radioButton3;
        public System.Windows.Forms.RadioButton radioButton2;
        public System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.Button btnSaveStat;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.Label label5;
        public System.Windows.Forms.Label label3;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.CheckBox checkBox3;
        public System.Windows.Forms.Button btnPrevious;
        public System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.RadioButton rdbFontSmall;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rdbFontBig;
        private System.Windows.Forms.RadioButton rdbFontMid;
    }
}