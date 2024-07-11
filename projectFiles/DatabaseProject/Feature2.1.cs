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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        SqlConnection myconn = new SqlConnection("Data source=ZHANGX;Initial catalog=Seismic_information_platform;Integrated Security=True");
        private void Form2_Load(object sender, EventArgs e)
        {
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox2.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox3.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox4.DropDownStyle = ComboBoxStyle.DropDownList;
            string mystr1 = "SELECT DISTINCT province FROM geography WHERE province IS NOT NULL";  //查询所有省份名称
            string mystr2 = "SELECT COUNT(DISTINCT province) AS TotalUniqueRows FROM geography WHERE province IS NOT NULL";  //获取省份总数
            SqlDataAdapter myadapter1 = new SqlDataAdapter(mystr1, myconn);
            SqlDataAdapter myadapter2 = new SqlDataAdapter(mystr2, myconn);
            DataSet mydataset1 = new DataSet();
            myadapter1.Fill(mydataset1, "t1");
            myadapter2.Fill(mydataset1, "t2");
            int j = Convert.ToInt32(mydataset1.Tables["t2"].Rows[0].ItemArray[0]);
            for (int i = 0; i < j; i++)
            {
                comboBox1.Items.Add(mydataset1.Tables["t1"].Rows[i].ItemArray[0].ToString());
            }
            
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox2.Items.Clear();
            comboBox3.Items.Clear();
            comboBox4.Items.Clear();
            string mystr1 = "SELECT city FROM geography WHERE province='" + comboBox1.SelectedItem + "'";  //查询省内所有城市名称
            string mystr2 = "SELECT COUNT(city) AS TotalRows FROM geography WHERE province='" + comboBox1.SelectedItem + "'";  //查询省内城市总数
            SqlDataAdapter myadapter1 = new SqlDataAdapter(mystr1, myconn);
            SqlDataAdapter myadapter2 = new SqlDataAdapter(mystr2, myconn);
            DataSet mydataset1 = new DataSet();
            myadapter1.Fill(mydataset1, "t1");
            myadapter2.Fill(mydataset1, "t2");
            int j = Convert.ToInt32(mydataset1.Tables["t2"].Rows[0].ItemArray[0]);
            for (int i = 0; i < j; i++)
            {
                comboBox2.Items.Add(mydataset1.Tables["t1"].Rows[i].ItemArray[0].ToString());
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem != null && comboBox2.SelectedItem != null && comboBox3.SelectedItem != null && comboBox4.SelectedItem != null)
            {
                string mystr1 = "UPDATE design SET dense=@mydense,ac=@myac WHERE city=@mycity";  //更新design数据库
                string connectionString = "Data source=ZHANGX;Initial catalog=Seismic_information_platform;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                        connection.Open();
                        using (SqlCommand command = new SqlCommand(mystr1, connection))
                        {
                            command.Parameters.AddWithValue("@mydense", comboBox3.SelectedItem.ToString());
                            command.Parameters.AddWithValue("@myac", comboBox4.SelectedItem.ToString());
                            command.Parameters.AddWithValue("@mycity", comboBox2.SelectedItem.ToString());
                        int rowsAffected = command.ExecuteNonQuery();
                    }

                }
                MessageBox.Show("数据更新成功！", "提示");
                this.Close();
            }
            else
            {
                MessageBox.Show("请完善全部更新数据！","错误提示");
            }
            
        }
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox3.Items.Clear();
            comboBox4.Items.Clear();
            for (int i = 6; i < 10; i++)
            {
                comboBox3.Items.Add(i.ToString());
            }
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox4.Items.Clear();
            if (comboBox3.SelectedItem.ToString()=="6") {
                comboBox4.Items.Add(0.05.ToString());
            }
            if (comboBox3.SelectedItem.ToString() == "7")
            {
                comboBox4.Items.Add(0.1.ToString());
                comboBox4.Items.Add(0.15.ToString());
            }
            if (comboBox3.SelectedItem.ToString() == "8")
            {
                comboBox4.Items.Add(0.2.ToString());
                comboBox4.Items.Add(0.3.ToString());
            }
            if (comboBox3.SelectedItem.ToString() == "9")
            {
                comboBox4.Items.Add(0.4.ToString());
            }
        }
    }
}
