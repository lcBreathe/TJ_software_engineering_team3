using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DatabaseProject
{
    public partial class retrieve_password : Form
    {
        public retrieve_password()
        {
            InitializeComponent();
        }
        SqlConnection myconn = new SqlConnection("Data source=ZHANGX;Initial catalog=Seismic_information_platform;Integrated Security=True");
        private void button2_Click(object sender, EventArgs e)
        {
            string mystr1 = "SELECT question FROM users WHERE name='"+textBox1.Text+"'";
            string mystr2 = "SELECT count(*) FROM users WHERE name='" + textBox1.Text + "'";
            SqlDataAdapter myadapter1 = new SqlDataAdapter(mystr1, myconn);
            SqlDataAdapter myadapter2 = new SqlDataAdapter(mystr2, myconn);
            DataSet mydataset1 = new DataSet();
            myadapter1.Fill(mydataset1, "t1");
            myadapter2.Fill(mydataset1, "t2");
            int i = Convert.ToInt16(mydataset1.Tables["t2"].Rows[0].ItemArray[0].ToString());
            if (i==0) {
                MessageBox.Show("查无此用户！", "提示");
                textBox1.Text = "";
                textBox2.Text = "";
            }
            else
            {
                label1.Text = "密保问题：\r\n\r\n" + mydataset1.Tables["t1"].Rows[0].ItemArray[0].ToString();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string mystr1 = "SELECT answer,password FROM users WHERE name='" + textBox1.Text + "'";
            string mystr2 = "SELECT count(*) FROM users WHERE name='" + textBox1.Text + "'";
            SqlDataAdapter myadapter1 = new SqlDataAdapter(mystr1, myconn);
            SqlDataAdapter myadapter2 = new SqlDataAdapter(mystr2, myconn);
            DataSet mydataset1 = new DataSet();
            myadapter1.Fill(mydataset1, "t1");
            myadapter2.Fill(mydataset1, "t2");
            int i = Convert.ToInt16(mydataset1.Tables["t2"].Rows[0].ItemArray[0]);
            if (i == 0)
            {
                MessageBox.Show("查无此用户！", "提示");
                textBox1.Text = "";
                textBox2.Text = "";
            }
            else
            {
                if (mydataset1.Tables["t1"].Rows[0].ItemArray[0].ToString() == textBox2.Text)
                {
                    MessageBox.Show("您的密码为：\r\n" + mydataset1.Tables["t1"].Rows[0].ItemArray[1].ToString(), "提示");
                    this.Close();
                }
                else {
                    MessageBox.Show("答案错误！" , "提示");
                    textBox2.Text = "";
                }
            }
        }
    }
}
