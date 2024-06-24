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
    public partial class loginform : Form
    {
        public event Action<string> DataSent;
        public loginform()
        {
            InitializeComponent();
        }
        SqlConnection myconn = new SqlConnection("Data source=ZHANGX;Initial catalog=Seismic_information_platform;Integrated Security=True");
        private void button1_Click(object sender, EventArgs e)
        {
            creat_new_users myform = new creat_new_users(); //进入新用户创建窗体
            myform.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            retrieve_password form5 = new retrieve_password(); //进入找回密码窗体
            form5.Show(); 
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //核对用户名和密码
            string name = textBox1.Text; 
            string password=textBox2.Text;
            string mystr1 = "SELECT password FROM users WHERE name='" + textBox1.Text + "'";
            string mystr2 = "SELECT count(*) FROM users WHERE name='" + textBox1.Text + "'";
            SqlDataAdapter myadapter1 = new SqlDataAdapter(mystr1, myconn);
            SqlDataAdapter myadapter2 = new SqlDataAdapter(mystr2, myconn);
            DataSet mydataset1 = new DataSet();
            myadapter1.Fill(mydataset1, "t1");
            myadapter2.Fill(mydataset1, "t2");
            int i = Convert.ToInt16(mydataset1.Tables["t2"].Rows[0].ItemArray[0].ToString());
            if (i == 0)
            {
                MessageBox.Show("查无此用户！", "提示");
                textBox1.Text = "";
                textBox2.Text = "";
            }
            else
            {
                if (textBox2.Text == mydataset1.Tables["t1"].Rows[0].ItemArray[0].ToString())
                {
                    GlobalData.SharedString = name;
                    control form1 = new control();
                    form1.Show();
                    this.Hide();
                    form1.FormClosed += (s, args) => Application.Exit();
                }
                else {
                    MessageBox.Show("密码错误！", "提示");
                    textBox1.Text = "";
                    textBox2.Text = "";
                }
            }
        }

    }
    public static class GlobalData
    {
        public static string SharedString { get; set; }
    }

}
