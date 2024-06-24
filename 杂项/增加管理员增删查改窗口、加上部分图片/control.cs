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
    public partial class control : Form
    {
        public control()
        {
            InitializeComponent();
        }
        SqlConnection myconn = new SqlConnection("Data source=ZHANGX;Initial catalog=Seismic_information_platform;Integrated Security=True");
        private void button1_Click(object sender, EventArgs e)
        {
            Feature1 newForm = new Feature1(); // 创建新窗体实例
            newForm.Show(); // 显示新窗体
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Feature2 newForm = new Feature2(); // 创建新窗体实例
            newForm.Show(); // 显示新窗体
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Feature3 newForm = new Feature3(); // 创建新窗体实例
            newForm.Show(); // 显示新窗体
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (User_Info.snum[0] != 'Z')
            {
                MessageBox.Show("你无权访问该项");
            }
            else
            {
                Administrator ad = new Administrator();
                ad.Show();
                this.Hide();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {

        }
    }
}
