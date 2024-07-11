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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }
        SqlConnection myconn = new SqlConnection("Data source=ZHANGX;Initial catalog=Seismic_information_platform;Integrated Security=True");
        private void Form3_Load(object sender, EventArgs e)
        {
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox2.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox3.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox4.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox5.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox6.DropDownStyle = ComboBoxStyle.DropDownList;
            int nowyear=DateTime.Now.Year;
            for (int indexer = nowyear; indexer > 1948; indexer--)
            {
                comboBox1.Items.Add(indexer.ToString());
            }
            for (int indexer = 1; indexer < 13; indexer++)
            {
                comboBox2.Items.Add(indexer.ToString());
            }
            for (int indexer = 1; indexer < 25; indexer++)
            {
                comboBox6.Items.Add(indexer.ToString());
            }
            for (int indexer = 1; indexer < 61; indexer++)
            {
                comboBox4.Items.Add(indexer.ToString());
                comboBox5.Items.Add(indexer.ToString());
            }
        }



        private void textBox7_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox3.Items.Clear();
            if (Convert.ToInt16(comboBox2.SelectedItem) == 2)
            {
                if (Convert.ToInt16(comboBox1.SelectedItem) % 100 == 0)
                {
                    if (Convert.ToInt16(comboBox1.SelectedItem) % 400 == 0)
                    {
                        for (int indexer = 1; indexer < 30; indexer++)
                        {
                            comboBox3.Items.Add(indexer.ToString());
                        }
                    }
                    else
                    {
                        for (int indexer = 1; indexer < 29; indexer++)
                        {
                            comboBox3.Items.Add(indexer.ToString());
                        }
                    }
                }
                else
                {
                    if (Convert.ToInt16(comboBox1.SelectedItem) % 4 == 0)
                    {
                        for (int indexer = 1; indexer < 30; indexer++)
                        {
                            comboBox3.Items.Add(indexer.ToString());
                        }
                    }
                    else
                    {
                        for (int indexer = 1; indexer < 29; indexer++)
                        {
                            comboBox3.Items.Add(indexer.ToString());
                        }
                    }
                }
            }
            else {
                if (Convert.ToInt16(comboBox2.SelectedItem) == 4|| Convert.ToInt16(comboBox2.SelectedItem) == 6|| Convert.ToInt16(comboBox2.SelectedItem) == 9|| Convert.ToInt16(comboBox2.SelectedItem) == 11)
                {
                    for (int indexer = 1; indexer < 31; indexer++)
                    {
                        comboBox3.Items.Add(indexer.ToString());
                    }
                }
                else
                {
                    for (int indexer = 1; indexer < 32; indexer++)
                    {
                        comboBox3.Items.Add(indexer.ToString());
                    }
                }
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox3.Items.Clear();
        }

        private void comboBox6_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (comboBox1.SelectedItem == null || comboBox2.SelectedItem == null || comboBox3.SelectedItem == null || comboBox4.SelectedItem == null || comboBox5.SelectedItem == null || comboBox6.SelectedItem == null ||textBox1.Text=="" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "" || textBox5.Text == "" || textBox6.Text == "") {
                MessageBox.Show("数据不全或数据不合理1！", "错误提示");
            }
            else
            {
                double m, d, sn, we;
                m = Convert.ToDouble(textBox1.Text);
                d = Convert.ToDouble(textBox4.Text);
                sn = Convert.ToDouble(textBox2.Text);
                we = Convert.ToDouble(textBox3.Text);
                int year = Convert.ToInt16(comboBox1.SelectedItem.ToString());
                int month = Convert.ToInt16(comboBox2.SelectedItem.ToString());
                int date = Convert.ToInt16(comboBox3.SelectedItem.ToString());
                int hour = Convert.ToInt16(comboBox6.SelectedItem.ToString());
                int min = Convert.ToInt16(comboBox5.SelectedItem.ToString());
                int sec = Convert.ToInt16(comboBox4.SelectedItem.ToString());
                DateTime mydatetime = new DateTime(year, month, date, hour, min, sec);
                if (mydatetime > DateTime.Now)
                {
                    MessageBox.Show("数据不全或数据不合理2！", "错误提示");
                }
                else
                {
                    if (m >= 5 && m <= 10 && d > 0 && sn > 0 && sn < 90 && we < 180 && we > 0)
                    {
                        string mystr1 = "SELECT COUNT(*) FROM geography WHERE city='" + textBox6.Text + "'";  //查询城市信息
                        SqlDataAdapter myadapter1 = new SqlDataAdapter(mystr1, myconn);
                        DataSet mydataset1 = new DataSet();
                        myadapter1.Fill(mydataset1, "t1");
                        if (mydataset1.Tables["t1"].Rows[0].ItemArray[0].ToString() == "0")
                        {
                            MessageBox.Show("数据不全或数据不合理3！", "错误提示");
                        }
                        else
                        {
                            string caotion = "是否确定添加位于 " + textBox6.Text +"  "+ textBox7.Text+" 的地震：\r\n描述：" + textBox5.Text + "  " +
                            "\r\n发震时间：" + mydatetime.ToString() + "  " +
                            "\r\n震级：" + m.ToString() + "  " + "震源深度：" + d.ToString() + "  km" +
                            "\r\n震中位置：" + we.ToString() + "°E  " + sn + "°N  ";
                            DialogResult result = MessageBox.Show(caotion, "提示", MessageBoxButtons.YesNo);
                            if (result == DialogResult.Yes)
                            {
                                string connectionString = "Data source=ZHANGX;Initial catalog=Seismic_information_platform;Integrated Security=True";
                                string insertQuery = "INSERT INTO history (city, magnitude, happentime, depth, we, sn, describe,province) VALUES (@1, @2, @3, @4, @5, @6, @7,@8)";
                                using (SqlConnection connection = new SqlConnection(connectionString))
                                {
                                    SqlCommand command = new SqlCommand(insertQuery, connection);
                                    command.Parameters.AddWithValue("@1", textBox6.Text);
                                    command.Parameters.AddWithValue("@2", m.ToString());
                                    command.Parameters.AddWithValue("@3", mydatetime.ToString());
                                    command.Parameters.AddWithValue("@4", d.ToString());
                                    command.Parameters.AddWithValue("@5", we.ToString());
                                    command.Parameters.AddWithValue("@6", sn.ToString());
                                    command.Parameters.AddWithValue("@7", textBox5.Text);
                                    command.Parameters.AddWithValue("@8", textBox7.Text);
                                    connection.Open();
                                    int rowsAffected = command.ExecuteNonQuery();
                                }
                                MessageBox.Show("已成功插入新记录！", "提示");
                                this.Close();
                            }
                        }
                    }

                    else
                    {
                        MessageBox.Show("数据不全或数据不合理4！", "错误提示");
                    }
                }
            }
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        
    }
}
