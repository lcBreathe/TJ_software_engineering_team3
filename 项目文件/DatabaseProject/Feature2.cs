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
using System.IO;

namespace DatabaseProject
{
    public partial class Feature2 : Form
    {
        public Feature2()
        {
            InitializeComponent();
        }
        SqlConnection myconn = new SqlConnection("Data source=ZHANGX;Initial catalog=Seismic_information_platform;Integrated Security=True");
        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox2.DropDownStyle = ComboBoxStyle.DropDownList;
            string mystr1 = "SELECT DISTINCT province FROM geography WHERE province IS NOT NULL";  //查询所有省份名称
            string mystr2 = "SELECT COUNT(DISTINCT province) AS TotalUniqueRows FROM geography WHERE province IS NOT NULL";  //获取省份总数
            SqlDataAdapter myadapter1 = new SqlDataAdapter(mystr1, myconn);
            SqlDataAdapter myadapter2 = new SqlDataAdapter(mystr2, myconn);
            DataSet mydataset1 = new DataSet();
            myadapter1.Fill(mydataset1, "t1");
            myadapter2.Fill(mydataset1, "t2");
            int j = Convert.ToInt32(mydataset1.Tables["t2"].Rows[0].ItemArray[0]);
            for (int i = 0; i < j; i++) {
                comboBox1.Items.Add(mydataset1.Tables["t1"].Rows[i].ItemArray[0].ToString());
            }
            
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox2.Items.Clear();
            textBox1.Text = "";
            textBox2.Text = "";
            label5.Text = "经度：";
            label6.Text = "纬度：";
            label1.Text = "设防烈度：";
            label2.Text = "设计基本地震加速度值：";
            string mystr1 = "SELECT city FROM geography WHERE province='"+comboBox1.SelectedItem+"'";  //查询省内所有城市名称
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

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            string mystr1 = "SELECT dense,ac FROM design WHERE city='" + comboBox2.SelectedItem + "'";  //查询设防烈度和设计加速度
            string mystr2 = "SELECT we,sn FROM position WHERE city='" + comboBox2.SelectedItem + "'";  //查询城市经纬度
            SqlDataAdapter myadapter1 = new SqlDataAdapter(mystr1, myconn);
            SqlDataAdapter myadapter2 = new SqlDataAdapter(mystr2, myconn);
            DataSet mydataset1 = new DataSet();
            myadapter1.Fill(mydataset1, "t1"); 
            myadapter2.Fill(mydataset1, "t2");
            label1.Text = "设防烈度："+ mydataset1.Tables["t1"].Rows[0].ItemArray[0].ToString();
            label2.Text="设计基本地震加速度值："+ mydataset1.Tables["t1"].Rows[0].ItemArray[1].ToString()+"g";
            label5.Text = "经度：" + mydataset1.Tables["t2"].Rows[0].ItemArray[0].ToString()+"°E";
            label6.Text = "纬度：" + mydataset1.Tables["t2"].Rows[0].ItemArray[1].ToString()+"°N";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            string mystr1 = "SELECT * FROM history ";  //查询每次地震的经纬度等信息
            string mystr2 = "SELECT COUNT(*) FROM history"; //查询地震记录总条数
            string mystr3 = "SELECT we,sn FROM position WHERE city='" + comboBox2.SelectedItem + "'";  //查询城市经纬度
            string mystr4 = label1.Text;
            int j,k,l;
            int indexdef;
            double we, sn, we0, sn0,d,m;
            SqlDataAdapter myadapter1 = new SqlDataAdapter(mystr1, myconn);
            SqlDataAdapter myadapter2 = new SqlDataAdapter(mystr2, myconn);
            SqlDataAdapter myadapter3 = new SqlDataAdapter(mystr3, myconn);
            DataSet mydataset1 = new DataSet();
            myadapter1.Fill(mydataset1, "t1");
            myadapter2.Fill(mydataset1, "t2");
            myadapter3.Fill(mydataset1, "t3");
            k = 0;
            l = 0;
            if (comboBox2.SelectedItem != null && comboBox1.SelectedItem != null)
            {
                char characterAtIndex = mystr4[5];
                indexdef = Convert.ToInt16(characterAtIndex.ToString());
                j = Convert.ToInt32(mydataset1.Tables["t2"].Rows[0].ItemArray[0]);
                we0 = Convert.ToDouble(mydataset1.Tables["t3"].Rows[0].ItemArray[0]);
                sn0 = Convert.ToDouble(mydataset1.Tables["t3"].Rows[0].ItemArray[1]);
                for (int i = 0; i < j; i++)
                {
                    we = Convert.ToDouble(mydataset1.Tables["t1"].Rows[i].ItemArray[4]);
                    sn = Convert.ToDouble(mydataset1.Tables["t1"].Rows[i].ItemArray[5]);
                    d = Convert.ToDouble(mydataset1.Tables["t1"].Rows[i].ItemArray[3]);
                    m = Convert.ToDouble(mydataset1.Tables["t1"].Rows[i].ItemArray[1]);
                    //计算震中距
                    double s = 6378.137 * Math.Acos(Math.Sin(sn * Math.PI / 180) * Math.Sin(sn0 * Math.PI / 180) + Math.Cos(sn0 * Math.PI / 180) * Math.Cos(sn * Math.PI / 180) * (Math.Cos((we - we0) * Math.PI / 180)));
                    //计算估计烈度
                    int it = (int)Math.Round((3 * m - 3) / 2 - 3 * Math.Log10(s / d + 1));
                    int it0 = (int)Math.Round((3 * m - 3) / 2);
                    if (d > 70)
                    {
                        it = 5;
                        it0 = 5;
                    }
                    if (it > 5 && s < 400)
                    {
                        textBox1.Text = textBox1.Text +
                            "地震描述：" + mydataset1.Tables["t1"].Rows[i].ItemArray[6].ToString() + "  " +
                            "\r\n发震时间：" + mydataset1.Tables["t1"].Rows[i].ItemArray[2].ToString() + "  " +
                            "\r\n震级：" + mydataset1.Tables["t1"].Rows[i].ItemArray[1].ToString() + "  " + "震源深度：" + mydataset1.Tables["t1"].Rows[i].ItemArray[3].ToString() + "  km" +
                            "\r\n震中位置：" + mydataset1.Tables["t1"].Rows[i].ItemArray[4].ToString() + "°E  " + mydataset1.Tables["t1"].Rows[i].ItemArray[5].ToString() + "°N  " +
                            "\r\n震中距：" + s.ToString() + "  km" +
                            "\r\n该地烈度：" + it.ToString() + "  " + "震中烈度：" + it0.ToString() + "\r\n\r\n";
                        k = k + 1;
                        if (it > indexdef)
                        {
                            l = l + 1;
                        }
                    }
                }
                textBox1.Text = "该地城区共受过" + k.ToString() + "次破坏性地震影响，其中" + l.ToString() + "次该地城区烈度超过设防烈度，具体情况如下：\r\n\r\n" + textBox1.Text;
            }
            else {
                MessageBox.Show("请先指定省份和城市！", "提示");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox2.Text = "";
            string mystr1 = "SELECT * FROM history WHERE city='" + comboBox2.SelectedItem + "'";  //查询每次地震的经纬度等信息
            string mystr2 = "SELECT COUNT(*) FROM history WHERE city='" + comboBox2.SelectedItem + "'"; //查询地震记录总条数
            string mystr3 = label1.Text;
            int j,k,l;
            int indexdef;
            SqlDataAdapter myadapter1 = new SqlDataAdapter(mystr1, myconn);
            SqlDataAdapter myadapter2 = new SqlDataAdapter(mystr2, myconn);
            DataSet mydataset1 = new DataSet();
            myadapter1.Fill(mydataset1, "t1");
            myadapter2.Fill(mydataset1, "t2");
            j = Convert.ToInt32(mydataset1.Tables["t2"].Rows[0].ItemArray[0]);
            k = 0;
            l = 0;
            if (comboBox2.SelectedItem != null && comboBox1.SelectedItem != null)
            {
                char characterAtIndex = mystr3[5];
                indexdef = Convert.ToInt16(characterAtIndex.ToString());
                for (int i = 0; i < j; i++)
                {
                    //计算估计烈度
                    double m = Convert.ToDouble(mydataset1.Tables["t1"].Rows[i].ItemArray[1]);
                    double d = Convert.ToDouble(mydataset1.Tables["t1"].Rows[i].ItemArray[3]);
                    int it0;
                    if (d > 70)
                    {
                        it0 = 5;
                    }
                    else
                    {
                        it0 = (int)Math.Round((3 * m - 3) / 2);
                    }
                    k = k + 1;
                    if (it0 > indexdef) {
                        l = l + 1;
                    }
                    textBox2.Text = textBox2.Text +
                        "地震描述：" + mydataset1.Tables["t1"].Rows[i].ItemArray[6].ToString() + "  " +
                        "\r\n发震时间：" + mydataset1.Tables["t1"].Rows[i].ItemArray[2].ToString() + "  " +
                        "\r\n震级：" + mydataset1.Tables["t1"].Rows[i].ItemArray[1].ToString() + "  " + "震源深度：" + mydataset1.Tables["t1"].Rows[i].ItemArray[3].ToString() + "  km" +
                        "\r\n震中位置：" + mydataset1.Tables["t1"].Rows[i].ItemArray[4].ToString() + "°E  " + mydataset1.Tables["t1"].Rows[i].ItemArray[5].ToString() + "°N  " +
                        "\r\n震中烈度：" + it0.ToString() + "\r\n\r\n";
                }
                textBox2.Text = "该地境内共发生过" +k.ToString()+"次破坏性地震，其中"+l.ToString()+"次震中烈度超过该地城区设防烈度，具体情况如下：\r\n\r\n"+ textBox2.Text;
            }
            else {
                MessageBox.Show("请先指定省份和城市！", "提示");
            }
        }


        private void button5_Click(object sender, EventArgs e)
        {
            
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            class1.content = class1.content + "该地城区历史上的破坏性地震情况如下：\r\n" + textBox1.Text;
            class1.content = class1.content + "震中位于该地境内的破坏性地震情况如下：\r\n" + textBox2.Text;


            Form6 form6 = new Form6();
            form6.Show();
        }
    }
}
