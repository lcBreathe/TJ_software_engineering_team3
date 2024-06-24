using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 软件工程课程大作业
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form3 f3 = new Form3();
            f3.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form5 f5 = new Form5();
            f5.Show();
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            System.Environment.Exit(0);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (用户信息.snum[0] != '1')
            {
                MessageBox.Show("你无权访问该项");
            }
            else
            {
                Form7 f7 = new Form7();
                f7.Show();
                this.Hide();
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (用户信息.snum[0] != '0')
            {
                MessageBox.Show("你无权访问该项");
            }
            else
            {
                Form8 f8 = new Form8();
                f8.Show();
                this.Hide();
            }
        }
    }
}
