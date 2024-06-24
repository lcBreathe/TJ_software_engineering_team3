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
    public partial class Form7 : Form
    {
        SqlConnection myconn = new SqlConnection("Data Source=.;Initial Catalog=地质信息及建筑设计建议;Integrated Security=True");
        public Form7()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string s = "update 反馈信息 set pass = 1 where info = '" + richTextBox1.Text.Trim() + "'";
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
                    MessageBox.Show("更新成功");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SqlDataAdapter myadapter = new SqlDataAdapter("select info from 反馈信息 where pass ='0'", myconn);
            DataSet myset = new DataSet();
            myadapter.Fill(myset, "查找");
            string a = myset.Tables["查找"].Rows[0][0].ToString();
            richTextBox1.Text = a;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string s = "delete 反馈信息 where info = '" + richTextBox1.Text.Trim() + "'";                                                     // 删除语句
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
                    MessageBox.Show("更新成功");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2();
            f2.Show();
            this.Hide();
        }
    }
}
