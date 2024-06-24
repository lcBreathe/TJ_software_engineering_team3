using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace DatabaseProject
{
    public partial class control : Form
    {
        private string username;


        public control()
        {
            InitializeComponent();

        }

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
            Feature4 newForm = new Feature4(); // 创建新窗体实例
            newForm.Show(); // 显示新窗体
        }


        private void button5_Click(object sender, EventArgs e)
        {
            string connectionString = ("Data source=ZHANGX;Initial catalog=Seismic_information_platform;Integrated Security=True");
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT role FROM users WHERE name = @username";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@username", username);

                        object result = command.ExecuteScalar();

                        if (result != null)
                        {
                            string role = result.ToString();

                            if (role.Equals("admin", StringComparison.OrdinalIgnoreCase))
                            {
                                MessageBox.Show( "您具有管理员权限，即将打开管理员操作界面。");
                                Administrator newForm = new Administrator(); // 创建新窗体实例
                                newForm.Show(); // 显示新窗体
                            }
                            else
                            {
                                MessageBox.Show("无权进行此操作。", "权限不足", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        else
                        {
                            MessageBox.Show("未找到用户。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("查询数据库时发生错误: " + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void control_Load(object sender, EventArgs e)
        {
            username = GlobalData.SharedString;
        }
    }
}
