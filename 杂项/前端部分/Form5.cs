using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Data.SqlClient;
using System.Drawing.Imaging;

namespace 软件工程课程大作业
{
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string strConn = "Data Source=.;Initial Catalog=地质信息及建筑设计建议;Integrated Security=True";
                SqlConnection connection = new SqlConnection(strConn);
                string sql = "insert into Images (blobdata) values (@blobdata)";
                SqlCommand command = new SqlCommand(sql, connection);
                //图片路径
                OpenFileDialog openFi = new OpenFileDialog();
                openFi.Filter = "图像文件(JPeg, Gif, Bmp, etc.)|*.jpg;*.jpeg;*.gif;*.bmp;*.tif; *.tiff; *.png| JPeg 图像文件(*.jpg;*.jpeg)"
                    + "|*.jpg;*.jpeg |GIF 图像文件(*.gif)|*.gif |BMP图像文件(*.bmp)|*.bmp|Tiff图像文件(*.tif;*.tiff)|*.tif;*.tiff|Png图像文件(*.png)"
                    + "| *.png |所有文件(*.*)|*.*";
                string picturePath = "";
                if (openFi.ShowDialog() == DialogResult.OK) {
                    picturePath = System.IO.Path.GetFullPath(openFi.FileName);
                }
                
                  //注意，这里需要指定保存图片的绝对路径和图片 
                                                     //文件的名称，每次必须更换图片的名称，这里很为不便
                //创建FileStream对象
                FileStream fs = new FileStream(picturePath, FileMode.Open, FileAccess.Read);
                //声明Byte数组
                Byte[] mybyte = new byte[fs.Length];
                //读取数据
                fs.Read(mybyte,0,mybyte.Length);
                fs.Close();
                //转换成二进制数据，并保存到数据库
                SqlParameter prm = new SqlParameter("@blobdata",SqlDbType.VarBinary,mybyte.Length,ParameterDirection.Input,false,0,0,null,DataRowVersion.Current,mybyte);
                command.Parameters.Add(prm);
                //打开数据库连接
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            /*
            //FolderBrowserDialog fd = new FolderBrowserDialog();
            //if (fd.ShowDialog() == DialogResult.OK)
            //{
                //textBox1.Text = "I am OK" +  fd.SelectedPath;
            //}//选择显示图片操作
            OpenFileDialog openFi = new OpenFileDialog();
            openFi.Filter = "图像文件(JPeg, Gif, Bmp, etc.)|*.jpg;*.jpeg;*.gif;*.bmp;*.tif; *.tiff; *.png| JPeg 图像文件(*.jpg;*.jpeg)"
                + "|*.jpg;*.jpeg |GIF 图像文件(*.gif)|*.gif |BMP图像文件(*.bmp)|*.bmp|Tiff图像文件(*.tif;*.tiff)|*.tif;*.tiff|Png图像文件(*.png)"
                + "| *.png |所有文件(*.*)|*.*";
            if (openFi.ShowDialog() == DialogResult.OK){
                MessageBox.Show(System.IO.Path.GetFullPath(openFi.FileName));
            }*/
             
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                //创建数据库连接字符串
                string strConn = "Data Source=.;Initial Catalog=地质信息及建筑设计建议;Integrated Security=True";
                //创建SqlConnection对象
                SqlConnection connection = new SqlConnection(strConn);
                //打开数据库连接
                connection.Open();
                //创建SQL语句
                string sql = "select blodid,blobdata from Images order by blodid";
                //创建SqlCommand对象
                SqlCommand command = new SqlCommand(sql, connection);
                //创建DataAdapter对象
                SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                //创建DataSet对象
                DataSet dataSet = new DataSet();
                dataAdapter.Fill(dataSet, "BLOBTest");
                int c = dataSet.Tables["BLOBTest"].Rows.Count;
                if (c > 0)
                {
                    Byte[] mybyte = new byte[0];
                    mybyte = (Byte[])(dataSet.Tables["BLOBTest"].Rows[c - 1]["BLOBData"]);
                    MemoryStream ms = new MemoryStream(mybyte);
                    pictureBox1.Image = Image.FromStream(ms);
                }
                connection.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2();
            f2.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form6 f6 = new Form6();
            f6.Show();
            this.Hide();
        }
    }
}
