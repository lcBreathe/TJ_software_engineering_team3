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
    public partial class Delete : Form
    {
        public Delete()
        {
            InitializeComponent();
        }
        static string connectionString = "Data source=ZHANGX;Initial catalog=Seismic_information_platform;Integrated Security=True";
        SqlConnection conn = new SqlConnection(connectionString);
        private void button2_Click(object sender, EventArgs e)
        {
            int year = Convert.ToInt16(comboBox1.SelectedItem.ToString());
            int month = Convert.ToInt16(comboBox2.SelectedItem.ToString());
            int date = Convert.ToInt16(comboBox3.SelectedItem.ToString());
            int hour = Convert.ToInt16(comboBox6.SelectedItem.ToString());
            int min = Convert.ToInt16(comboBox5.SelectedItem.ToString());
            int sec = Convert.ToInt16(comboBox4.SelectedItem.ToString());
            DateTime mydatetime = new DateTime(year, month, date, hour, min, sec);
            conn.Open();
            {
                string s = "delete from history where happentime = '" + mydatetime + "'";
                SqlCommand mycmd = new SqlCommand(s, conn);
                mycmd.ExecuteNonQuery();
            }
            conn.Close();
        }
    }
}
