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

namespace DatabaseProject
{
    public partial class Administrator : Form
    {
        public Administrator()
        {
            InitializeComponent();
        }
        const string connectionString = "Data source=ZHANGX;Initial catalog=Seismic_information_platform;Integrated Security=True";
        SqlConnection conn = new SqlConnection(connectionString);
        private void button5_Click(object sender, EventArgs e)
        {
            control ct = new control();
            ct.Show();
            this.Hide();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            SqlDataAdapter myadapter = new SqlDataAdapter("select info from 反馈信息 where pass ='1'", conn);
            DataSet myset = new DataSet();
            myadapter.Fill(myset, "查找");
            string a = myset.Tables["查找"].Rows[0][0].ToString();
            richTextBox1.Text = a;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form3 f3 = new Form3();
            f3.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Delete dl = new Delete();
            dl.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Modify md = new Modify();
            md.Show();
            this.Hide();
        }
    }
}
