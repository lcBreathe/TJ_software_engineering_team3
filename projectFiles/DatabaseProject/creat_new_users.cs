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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Reflection.Emit;

namespace DatabaseProject
{
    public partial class creat_new_users : Form
    {
        public creat_new_users()
        {
            InitializeComponent();
        }
        SqlConnection myconn = new SqlConnection("Data source=ZHANGX;Initial catalog=Seismic_information_platform;Integrated Security=True");
        private void Form4_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string username;
            string password;
            string question;
            string answer;
            string role = "user";
            username = textBox1.Text;
            password = textBox2.Text;
            question= textBox3.Text;
            answer= textBox4.Text;
            int lengthusername=username.Length;
            int lengthpasswoed = password.Length;
            string mystr1 = "select count(*) from users where name ='"+username+"'";
            SqlDataAdapter myadapter1 = new SqlDataAdapter(mystr1, myconn);
            DataSet mydataset = new DataSet();
            myadapter1.Fill(mydataset, "t1");
            if (Convert.ToInt16(mydataset.Tables["t1"].Rows[0].ItemArray[0].ToString()) == 0)
            {
                if (lengthpasswoed <= 20 && lengthusername <= 20 && lengthpasswoed >= 6 && username != "" && question != "" && answer != "")
                {
                    string connectionString = "Data source=ZHANGX;Initial catalog=Seismic_information_platform;Integrated Security=True";
                    string insertQuery = "INSERT INTO users (name, password, question, answer,role) VALUES (@1, @2, @3, @4,user)";
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        SqlCommand command = new SqlCommand(insertQuery, connection);
                        command.Parameters.AddWithValue("@1", username);
                        command.Parameters.AddWithValue("@2", password);
                        command.Parameters.AddWithValue("@3", question);
                        command.Parameters.AddWithValue("@4", answer); 
                        command.Parameters.AddWithValue("user", role);
                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();
                    }
                    MessageBox.Show("已成功创建新用户！", "提示");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("新用户信息不合要求，请返回修改！", "提示");
                    textBox1.Text = "";
                    textBox2.Text = "";
                    textBox3.Text = "";
                    textBox4.Text = "";
                }
            }
            else {
                MessageBox.Show("该用户名已存在，请返回修改！", "提示");
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
            }
        }
    }
}
