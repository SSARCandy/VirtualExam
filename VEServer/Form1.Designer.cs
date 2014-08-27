namespace VEServer
{
    partial class Form1
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置 Managed 資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器
        /// 修改這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.lblHostIP = new System.Windows.Forms.Label();
            this.lblHostName = new System.Windows.Forms.Label();
            this.lblclientComputerName = new System.Windows.Forms.Label();
            this.lblClientIpAddress = new System.Windows.Forms.Label();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.lblOnline = new System.Windows.Forms.Label();
            this.lblAcc = new System.Windows.Forms.Label();
            this.listBox2 = new System.Windows.Forms.ListBox();
            this.lblSckIndex = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 61);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "HostStat";
            // 
            // lblHostIP
            // 
            this.lblHostIP.AutoSize = true;
            this.lblHostIP.Location = new System.Drawing.Point(12, 34);
            this.lblHostIP.Name = "lblHostIP";
            this.lblHostIP.Size = new System.Drawing.Size(36, 12);
            this.lblHostIP.TabIndex = 4;
            this.lblHostIP.Text = "HostIP";
            // 
            // lblHostName
            // 
            this.lblHostName.AutoSize = true;
            this.lblHostName.Location = new System.Drawing.Point(12, 9);
            this.lblHostName.Name = "lblHostName";
            this.lblHostName.Size = new System.Drawing.Size(53, 12);
            this.lblHostName.TabIndex = 3;
            this.lblHostName.Text = "HostName";
            // 
            // lblclientComputerName
            // 
            this.lblclientComputerName.AutoSize = true;
            this.lblclientComputerName.Location = new System.Drawing.Point(12, 91);
            this.lblclientComputerName.Name = "lblclientComputerName";
            this.lblclientComputerName.Size = new System.Drawing.Size(33, 12);
            this.lblclientComputerName.TabIndex = 6;
            this.lblclientComputerName.Text = "label2";
            // 
            // lblClientIpAddress
            // 
            this.lblClientIpAddress.AutoSize = true;
            this.lblClientIpAddress.Location = new System.Drawing.Point(12, 118);
            this.lblClientIpAddress.Name = "lblClientIpAddress";
            this.lblClientIpAddress.Size = new System.Drawing.Size(33, 12);
            this.lblClientIpAddress.TabIndex = 7;
            this.lblClientIpAddress.Text = "label3";
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 12;
            this.listBox1.Location = new System.Drawing.Point(579, 9);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(124, 244);
            this.listBox1.TabIndex = 8;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // lblOnline
            // 
            this.lblOnline.AutoSize = true;
            this.lblOnline.Location = new System.Drawing.Point(12, 173);
            this.lblOnline.Name = "lblOnline";
            this.lblOnline.Size = new System.Drawing.Size(53, 12);
            this.lblOnline.TabIndex = 9;
            this.lblOnline.Text = "線上人數";
            // 
            // lblAcc
            // 
            this.lblAcc.AutoSize = true;
            this.lblAcc.Location = new System.Drawing.Point(12, 151);
            this.lblAcc.Name = "lblAcc";
            this.lblAcc.Size = new System.Drawing.Size(53, 12);
            this.lblAcc.TabIndex = 10;
            this.lblAcc.Text = "累積人數";
            // 
            // listBox2
            // 
            this.listBox2.FormattingEnabled = true;
            this.listBox2.ItemHeight = 12;
            this.listBox2.Location = new System.Drawing.Point(96, 9);
            this.listBox2.Name = "listBox2";
            this.listBox2.Size = new System.Drawing.Size(477, 244);
            this.listBox2.TabIndex = 11;
            // 
            // lblSckIndex
            // 
            this.lblSckIndex.AutoSize = true;
            this.lblSckIndex.Location = new System.Drawing.Point(12, 202);
            this.lblSckIndex.Name = "lblSckIndex";
            this.lblSckIndex.Size = new System.Drawing.Size(46, 12);
            this.lblSckIndex.TabIndex = 12;
            this.lblSckIndex.Text = "sckindex";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(706, 261);
            this.Controls.Add(this.lblSckIndex);
            this.Controls.Add(this.listBox2);
            this.Controls.Add(this.lblAcc);
            this.Controls.Add(this.lblOnline);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.lblClientIpAddress);
            this.Controls.Add(this.lblclientComputerName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblHostIP);
            this.Controls.Add(this.lblHostName);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblHostIP;
        private System.Windows.Forms.Label lblHostName;
        private System.Windows.Forms.Label lblclientComputerName;
        private System.Windows.Forms.Label lblClientIpAddress;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label lblOnline;
        private System.Windows.Forms.Label lblAcc;
        private System.Windows.Forms.ListBox listBox2;
        private System.Windows.Forms.Label lblSckIndex;
    }
}

