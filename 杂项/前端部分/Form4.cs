using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace 软件工程课程大作业
{
    public partial class Form4 : Form
    {
        SqlConnection myconn = new SqlConnection("Data Source=.;Initial Catalog=地质信息及建筑设计建议;Integrated Security=True");
        
        public Form4()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form3 f3 = new Form3();
            f3.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string s = "insert into 反馈信息 values('" + richTextBox1.Text.Trim() + "', '0')";
            SqlCommand mycmd = new SqlCommand(s, myconn);
            if (MessageBox.Show("Confirm upload?", "Confirm Message", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                try
                {
                    MessageBox.Show(s);
                    myconn.Open();
                    mycmd.ExecuteNonQuery();
                    myconn.Close();
                    richTextBox1.Text = "";
                    MessageBox.Show("上传成功");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
        }
    }
}
