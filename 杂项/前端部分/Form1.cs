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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        static string mystr = "Data Source=.;Initial Catalog=地质信息及建筑设计建议;Integrated Security=True";
        SqlConnection myconn = new SqlConnection(mystr);

        private void button1_Click(object sender, EventArgs e)
        {
            用户信息.snum = textBox1.Text.Trim();
            string str = "select access from 个人信息 where snum='" + textBox1.Text.Trim() + "'";
            SqlDataAdapter myadapter = new SqlDataAdapter(str, myconn);
            DataSet myset = new DataSet();
            myadapter.Fill(myset, "key");
            if (myset.Tables["key"].Rows[0].ItemArray[0].ToString() == textBox2.Text.Trim())
            {
                Form2 f1 = new Form2();
                f1.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("用户名或密码错误，请重新输入！");
                textBox2.Clear();
            }
        }
    }
}
