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
    public partial class Score : Form
    {
        public Score()
        {
            InitializeComponent();
        }
        private double[] usersScore = new double[1];
        private double[] stdScore = new double[1];

        public void setUsersScore(int  usersScore)
        {
            this.usersScore[0] = Convert.ToDouble(usersScore);
        }

        private void Score_Load(object sender, EventArgs e)
        {
            stdScore[0] = 60;
            chart1.Series[0].Points.Add(usersScore);
            chart1.Series[1].Points.Add(stdScore);
        }
        
        
    }
}
