using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VirtualExam
{
    public partial class SetExamForm : Form
    {

        public int Time
        {
            set
            {
                tbTime.Text = value.ToString(); //轉換成分鐘
            }
            get
            {
                return (Int32.Parse(tbTime.Text) * 60) ;
            }
        }

        public SetExamForm()
        {
            InitializeComponent();
            this.Time = 15;
        }

        private void btnApply_Click(object sender, EventArgs e)
        {

            ExamForm.enhanceMode = checkBox1.Checked;
            this.Close();
        }
    }
}
