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
        //初始化绑定默认关键词
        List<string> listOnit = new List<string>();
        //输入key之后，返回的关键词
        List<string> listNew = new List<string>();
        SqlConnection myconn = new SqlConnection("Data Source=.;Initial Catalog=$$$$$$$$;Integrated Security=True");
        public Form3()
        {
            InitializeComponent();
        }


        // 此处实现搜索框关键词提示功能

        private void Form3_Load(object sender, EventArgs e)
        {
            string str1 = "select $$$$ from $$$$$$";
            BindComboBox(str1);
        }
        private void BindComboBox(string str)
        {
            listOnit.Clear();
            SqlDataAdapter myadapter = new SqlDataAdapter(str, myconn);
            DataSet myset = new DataSet();
            myadapter.Fill(myset, "###");
            for (int i = 0; i <= myset.Tables["###"].Rows.Count - 1; i++)
            {
                listOnit.Add(myset.Tables["###"].Rows[i].ItemArray[0].ToString());
            }

            /*
             * 1.注意用Item.Add(obj)或者Item.AddRange(obj)方式添加
             * 2.如果用DataSource绑定，后面再进行绑定是不行的，即便是Add或者Clear也不行
             */
            this.comboBox1.Items.AddRange(listOnit.ToArray());
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

        
    }
}
