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
    public partial class Modify : Form
    {
        public Modify()
        {
            InitializeComponent();
        }
        static string connectionString = "Data source=ZHANGX;Initial catalog=Seismic_information_platform;Integrated Security=True";
        SqlConnection conn = new SqlConnection(connectionString);
        private void button1_Click(object sender, EventArgs e)
        {
            conn.Open();
            {
                if (textBox2.Text.Trim() != "")
                {
                    string s1 = "update design set dense = '" + textBox2.Text.Trim() + "' where city = '" + textBox1.Text.Trim() + "'";
                    SqlCommand mycmd = new SqlCommand(s1, conn);
                    mycmd.ExecuteNonQuery();
                }
                if (textBox3.Text.Trim() != "")
                {
                    string s2 = "update design set ac = '" + textBox3.Text.Trim() + "' where city = '" + textBox1.Text.Trim() + "'";
                    SqlCommand mycmd = new SqlCommand(s2, conn);
                    mycmd.ExecuteNonQuery();
                }
            }
            conn.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            conn.Open();
            {
                if (textBox6.Text.Trim() != "")
                {
                    string s1 = "update history set depth = '" + textBox6.Text.Trim() + "' where city = '" + textBox4.Text.Trim() + "' and happentime = '"+ textBox5.Text.Trim() +"'";
                    SqlCommand mycmd = new SqlCommand(s1, conn);
                    mycmd.ExecuteNonQuery();
                }
                if (textBox7.Text.Trim() != "")
                {
                    string s2 = "update history set describe = '" + textBox7.Text.Trim() + "' where city = '" + textBox4.Text.Trim() + "' and happentime = '" + textBox5.Text.Trim() + "'";
                    SqlCommand mycmd = new SqlCommand(s2, conn);
                    mycmd.ExecuteNonQuery();
                }
            }
            conn.Close();
        }
    }
}
