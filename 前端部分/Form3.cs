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
    public partial class Form3 : Form
    {
        string sheng;
        string shi;
        string quxian;
        //初始化绑定默认关键词
        List<string> listOnit = new List<string>();
        List<string> listOnit_2 = new List<string>();
        List<string> listOnit_3 = new List<string>();
        //输入key之后，返回的关键词
        List<string> listNew = new List<string>();
        List<string> listNew_2 = new List<string>();
        List<string> listNew_3 = new List<string>();
        SqlConnection myconn = new SqlConnection("Data Source=.;Initial Catalog=地质信息及抗震设防建议;Integrated Security=True");
        public Form3()
        {
            InitializeComponent();
        }


        // 此处实现搜索框关键词提示功能

        private void Form3_Load(object sender, EventArgs e)
        {
            string str1 = "select * from kangzhen";
            BindComboBox(str1);
        }
        private void BindComboBox(string str)
        {
            listOnit.Clear();
            SqlDataAdapter myadapter = new SqlDataAdapter(str, myconn);
            DataSet myset = new DataSet();
            myadapter.Fill(myset, "alls");
            for (int i = 0; i <= myset.Tables["alls"].Rows.Count - 1; i++)
            {
                listOnit.Add(myset.Tables["alls"].Rows[i].ItemArray[0].ToString());
                listOnit_2.Add(myset.Tables["alls"].Rows[i].ItemArray[1].ToString());
                listOnit_3.Add(myset.Tables["alls"].Rows[i].ItemArray[3].ToString());
            }

            /*
             * 1.注意用Item.Add(obj)或者Item.AddRange(obj)方式添加
             * 2.如果用DataSource绑定，后面再进行绑定是不行的，即便是Add或者Clear也不行
             */
            this.comboBox1.Items.AddRange(listOnit.ToArray());
            this.comboBox2.Items.AddRange(listOnit_2.ToArray());
            this.comboBox3.Items.AddRange(listOnit_3.ToArray());
        }

        private void comboBox1_TextUpdate(object sender, EventArgs e)
        {

            //清空combobox
            this.comboBox1.Items.Clear();
            //清空listNew
            listNew.Clear();
            //遍历全部备查数据
            foreach (var item in listOnit)
            {
                if (item.Contains(this.comboBox1.Text))
                {
                    //符合，插入ListNew
                    listNew.Add(item);
                }
            }
            //combobox添加已经查到的关键词
            this.comboBox1.Items.AddRange(listNew.ToArray());
            //设置光标位置，否则光标位置始终保持在第一列，造成输入关键词的倒序排列
            this.comboBox1.SelectionStart = this.comboBox1.Text.Length;
            //保持鼠标指针原来状态，有时候鼠标指针会被下拉框覆盖，所以要进行一次设置。
            Cursor = Cursors.Default;
            //自动弹出下拉框
            this.comboBox1.DroppedDown = true;
        }

        private void comboBox2_TextUpdate(object sender, EventArgs e)
        {
            this.comboBox2.Items.Clear();
            //清空listNew
            listNew_2.Clear();
            //遍历全部备查数据
            foreach (var item in listOnit_2)
            {
                if (item.Contains(this.comboBox2.Text))
                {
                    //符合，插入ListNew
                    listNew_2.Add(item);
                }
            }
            //combobox添加已经查到的关键词
            this.comboBox2.Items.AddRange(listNew_2.ToArray());
            //设置光标位置，否则光标位置始终保持在第一列，造成输入关键词的倒序排列
            this.comboBox2.SelectionStart = this.comboBox2.Text.Length;
            //保持鼠标指针原来状态，有时候鼠标指针会被下拉框覆盖，所以要进行一次设置。
            Cursor = Cursors.Default;
            //自动弹出下拉框
            this.comboBox2.DroppedDown = true;
        }


        private void comboBox3_TextUpdate(object sender, EventArgs e)
        {
            this.comboBox3.Items.Clear();
            //清空listNew
            listNew_3.Clear();
            //遍历全部备查数据
            foreach (var item in listOnit_3)
            {
                if (item.Contains(this.comboBox3.Text))
                {
                    //符合，插入ListNew
                    listNew_3.Add(item);
                }
            }
            //combobox添加已经查到的关键词
            this.comboBox3.Items.AddRange(listNew_3.ToArray());
            //设置光标位置，否则光标位置始终保持在第一列，造成输入关键词的倒序排列
            this.comboBox3.SelectionStart = this.comboBox3.Text.Length;
            //保持鼠标指针原来状态，有时候鼠标指针会被下拉框覆盖，所以要进行一次设置。
            Cursor = Cursors.Default;
            //自动弹出下拉框
            this.comboBox3.DroppedDown = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form4 f4 = new Form4();
            f4.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2();
            f2.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string str2 = "select liedu from kangzhen where sheng = '"+comboBox1.Text.Trim()+ "' and shi = '" + comboBox2.Text.Trim() + "' and quxian = '"+comboBox3.Text.Trim()+"'";
            SqlDataAdapter myadapter0 = new SqlDataAdapter(str2, myconn);
            DataSet myset0 = new DataSet();
            myadapter0.Fill(myset0, "liedu");
            textBox1.Text = myset0.Tables["liedu"].Rows[0][0].ToString();

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            SqlDataAdapter myadapter = new SqlDataAdapter("select kj,kjkz,kz,bfkz,kjhx,tzt,bzkz from duogaoceng where liedu ='" + textBox1.Text.Trim() + "'", myconn);
            DataSet myset = new DataSet();
            myadapter.Fill(myset, "查找");
            string s1 = "（一）现浇钢筋混凝土房屋适用的最大高度（m）\n";
            string s2 = "框架：" + myset.Tables["查找"].Rows[0][0].ToString() + "\n框架-抗震墙" + myset.Tables["查找"].Rows[0][1].ToString();
            string s3 = "\n抗震墙" + myset.Tables["查找"].Rows[0][2].ToString() + "\n部分框支抗震墙" + myset.Tables["查找"].Rows[0][3].ToString();
            string s4 = "\n框架-核心筒" + myset.Tables["查找"].Rows[0][4].ToString() + "\n筒中筒" + myset.Tables["查找"].Rows[0][5].ToString();
            string s5 = "\n板柱-抗震墙" + myset.Tables["查找"].Rows[0][6].ToString();
            richTextBox1.Text = s1 + s2 + s3 + s4 + s5;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            SqlDataAdapter myadapter = new SqlDataAdapter("select ptzgd240, ptzcs240,dkzgd240,dkzcs240,dkzgd190,dkzcs190,xqkgd190,xqkcs190 from duocengqiti where liedu ='" + textBox1.Text.Trim() + "'", myconn);
            DataSet myset = new DataSet();
            myadapter.Fill(myset, "查找");
            string s1 = "（一）房屋层数和总高度限值（m）\n";
            string s2 = "1.普通砖：\n  最小抗震墙厚度：240mm  高度 " + myset.Tables["查找"].Rows[0][0].ToString() + "层数：" + myset.Tables["查找"].Rows[0][1].ToString();
            string s3 = "\n2.多孔砖：\n  最小抗震墙厚度：240mm  高度 " + myset.Tables["查找"].Rows[0][2].ToString() + "层数：" + myset.Tables["查找"].Rows[0][3].ToString();
            string s4 = "\n3.多孔砖：\n  最小抗震墙厚度：190mm  高度 " + myset.Tables["查找"].Rows[0][4].ToString() + "层数：" + myset.Tables["查找"].Rows[0][5].ToString();
            string s5 = "\n4.小砌块：\n  最小抗震墙厚度：190mm  高度 " + myset.Tables["查找"].Rows[0][6].ToString() + "层数：" + myset.Tables["查找"].Rows[0][7].ToString();
            richTextBox1.Text = s1 + s2 + s3 + s4 + s5;
        }
    }
}
