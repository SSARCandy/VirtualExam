using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace VirtualExam
{
    public partial class SignIn : Form
    {
        public SignIn()
        {
            InitializeComponent();
           
        }
        void Initializer()
        {
            radioButton1.CheckedChanged += new EventHandler(radioButton_CheckedChanged);
            radioButton2.CheckedChanged += new EventHandler(radioButton_CheckedChanged);
            radioButton3.CheckedChanged += new EventHandler(radioButton_CheckedChanged);
            
        }
        public static string IP;
        void radioButton_CheckedChanged(object sender,EventArgs e)
        {
            SetIp();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SetIp();
            this.Hide();
            MainForm m = new MainForm();
            m.Show();
        }

        void SetIp()
        {
            if (radioButton1.Checked)
                IP = "219.85.200.148";
            else if (radioButton2.Checked)
                IP = "111.251.147.144";
            else
                IP = "127.0.0.1";
        }
    }
}
