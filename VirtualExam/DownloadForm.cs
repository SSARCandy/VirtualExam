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
    public partial class DownloadForm : Form
    {
        public DownloadForm()
        {
            InitializeComponent();
            //嘗試開發線上更新treeview
            /*TreeNode[] myNodeRoot = new TreeNode[2];
            TreeNode[] myNode = new TreeNode[2];
            TreeNode[] myNode2 = new TreeNode[2];
            myNode[0] = new TreeNode();
            myNode[1] = new TreeNode();
            myNode2[0] = new TreeNode();
            myNode2[1] = new TreeNode();
            myNodeRoot[0] = new TreeNode();
            myNodeRoot[1] = new TreeNode();

            myNode2[0].Text = "證券商業務員";
            
            myNode2[1].Text = "經濟大會考";
            myNode[0].Nodes.Add(myNode2[0]);
            myNode[1].Nodes.Add(myNode2[1]);

            myNode[0].Text = "證基會";
            myNode[1].Text = "商學院";
            myNodeRoot[0].Nodes.Add(myNode[0]);
            myNodeRoot[1].Nodes.Add(myNode[1]);

            myNodeRoot[0].Text = "金融證照";
            myNodeRoot[1].Text = "政治大學";
            treeView1.Nodes.Clear();
            treeView1.Nodes.AddRange(myNodeRoot);*/


        }
        public static string dlExam;
        public string examName;
        private void DownloadForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (MainForm.connected)
            {
                if (treeView1.SelectedNode.Text == "證券商業務員")
                {
                    dlExam = "\\" + treeView1.SelectedNode.FullPath;
                    examName = treeView1.SelectedNode.Text;
                    MainForm.askDownload = true;
                    lblStat.Text = "狀態：下載中...";
                }
                else
                {
                    MessageBox.Show("請點選題庫");
                }
            }
            else
            {
                MessageBox.Show("未連線");
            }
        }


        public string getExamName()
        {
            return this.examName;
        }

        private void treeView1_Click(object sender, EventArgs e)
        {
        }

        private void treeView1_MouseClick(object sender, MouseEventArgs e)
        {

            
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (treeView1.SelectedNode.Text == "證券商業務員")
            {
                label1.Text = "題庫：證券商業務員";
                label2.Text = "提供者：官方提供";
            }
        }

    }
}
