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
        SqlConnection myconn = new SqlConnection("Data Source=.;Initial Catalog=地质信息及建筑设计建议;Integrated Security=True");
        public Form3()
        {
            InitializeComponent();
        }


        // 此处实现搜索框关键词提示功能

        private void Form3_Load(object sender, EventArgs e)
        {
            string str0 = "此抗震设防烈度及建筑设计建议依据gb50011-2022，仅给出初步设计建议，适用于多遇地震。多高层钢筋混凝土房屋、多层砌体房屋、底部框架砌体房屋、多高层钢结构房屋给出相应适用的最大高度，" +
            "单层工业厂房、大跨屋盖建筑给出结构布置一般规定。其它类型结构如土、木、石结构房屋、地下建筑等及详细设计建议，请参考gb50011-2022抗震设计规范";
            MessageBox.Show(str0);
            string str1 = "select * from kangzhen";
            BindComboBox(str1);
        }
        private void BindComboBox(string str)
        {
            //listOnit.Clear();
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
            foreach (var item in listOnit)
            {
                    //符合，插入ListNew
                if (listNew.Contains(item) == false) listNew.Add(item);
            }
            foreach (var item in listOnit_2)
            {
                    //符合，插入ListNew
                if (listNew_2.Contains(item) == false) listNew_2.Add(item);
            }
            foreach (var item in listOnit_3)
            {
                    //符合，插入ListNew
                if (listNew_2.Contains(item) == false) listNew_3.Add(item);
            }
            this.comboBox1.Items.AddRange(listNew.ToArray());
            this.comboBox2.Items.AddRange(listNew_2.ToArray());
            this.comboBox3.Items.AddRange(listNew_3.ToArray());
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
                    if(listNew.Contains(item) == false) listNew.Add(item);
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
            /*
            foreach (var item in listOnit_2)
            {
                if (item.Contains(this.comboBox2.Text))
                {
                    //符合，插入ListNew
                    if (listNew_2.Contains(item) == false) listNew_2.Add(item);
                }
            }*/
            for (int i = 0; i < listOnit_2.Count(); i++)
            {
                if (listOnit_2[i].Contains(this.comboBox2.Text))
                {
                    if (comboBox1.Text.ToString() == listOnit[i].ToString())
                    {
                        if (listNew_2.Contains(listOnit_2[i]) == false) listNew_2.Add(listOnit_2[i]);
                    }
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
            /*
            foreach (var item in listOnit_3)
            {
                if (item.Contains(this.comboBox3.Text))
                {
                    //符合，插入ListNew
                    if (listNew_3.Contains(item) == false) listNew_3.Add(item);
                }
            }*/
            for (int i = 0; i < listOnit_3.Count(); i++)
            {
                if (listOnit_3[i].Contains(this.comboBox3.Text)) {
                    if (comboBox2.Text.ToString() == listOnit_2[i].ToString())
                    {
                        if (listNew_3.Contains(listOnit_3[i]) == false) listNew_3.Add(listOnit_3[i]);
                    }
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

        private void button6_Click(object sender, EventArgs e)
        {
            SqlDataAdapter myadapter = new SqlDataAdapter("select ptzgd240, ptzcs240,dkzgd240,dkzcs240,dkzgd190,dkzcs190,xqkgd190,xqkcs190 from dibukjqiti where liedu ='" + textBox1.Text.Trim() + "'", myconn);
            DataSet myset = new DataSet();
            myadapter.Fill(myset, "查找");
            string s1 = "（一）房屋层数和总高度限值（m）\n";
            string s2 = "1.普通砖：\n  最小抗震墙厚度：240mm  高度 " + myset.Tables["查找"].Rows[0][0].ToString() + "层数：" + myset.Tables["查找"].Rows[0][1].ToString();
            string s3 = "\n2.多孔砖：\n  最小抗震墙厚度：240mm  高度 " + myset.Tables["查找"].Rows[0][2].ToString() + "层数：" + myset.Tables["查找"].Rows[0][3].ToString();
            string s4 = "\n3.多孔砖：\n  最小抗震墙厚度：190mm  高度 " + myset.Tables["查找"].Rows[0][4].ToString() + "层数：" + myset.Tables["查找"].Rows[0][5].ToString();
            string s5 = "\n4.小砌块：\n  最小抗震墙厚度：190mm  高度 " + myset.Tables["查找"].Rows[0][6].ToString() + "层数：" + myset.Tables["查找"].Rows[0][7].ToString();
            richTextBox1.Text = s1 + s2 + s3 + s4 + s5;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            SqlDataAdapter myadapter = new SqlDataAdapter("select kj,kjzx, kjpx, tt, maxgaokuanbi from dgcgangjiegou where liedu ='" + textBox1.Text.Trim() + "'", myconn);
            DataSet myset = new DataSet();
            myadapter.Fill(myset, "查找");
            string s1 = "（一）现浇钢筋混凝土房屋适用的最大高度（m）\n";
            string s2 = "框架：" + myset.Tables["查找"].Rows[0][0].ToString() + "\n框架-中心支撑：" + myset.Tables["查找"].Rows[0][1].ToString();
            string s3 = "\n框架-偏心支撑（延性墙板）：" + myset.Tables["查找"].Rows[0][2].ToString() + "\n筒体（框筒，筒中筒，桁架桶，束桶）和巨型框架：" + myset.Tables["查找"].Rows[0][3].ToString();
            string s4 = "\n（二）最大高宽比";
            string s5 = "\n" + myset.Tables["查找"].Rows[0][4].ToString();
            richTextBox1.Text = s1 + s2 + s3 + s4 + s5;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = "（一）单层钢筋混凝土柱厂房结构布置一般规定"+ 
                            "\n装配式单层钢筋混凝土柱厂房布置应符合下列要求：" +
                            "\n1 多跨厂房宜等高和等长，高低跨厂房不宜采用一端开口的结构布置。" +
                            "\n2 厂房的贴建房屋和构筑物，不宜布置在厂房角部和紧邻防震缝处。" +
                            "\n3 厂房体型复杂或有贴建的房屋和构筑物时，宜设防震缝；在厂房纵横跨交接处、大柱网厂房或不设柱间支撑的厂房，防震缝宽度可采用100mm～150mm，其他情况可采用50mm～90mm。" +
                            "\n4 两个主厂房之间的过渡跨至少应有一侧采用防震缝与主厂房脱开。" +
                            "\n5 厂房内上起重机的铁梯不应靠近防震缝设置；多跨厂房各跨上起重机的铁梯不宜设置在同一横向轴线附近。" +
                            "\n6 厂房内的工作平台、刚性工作间宜与厂房主体结构脱开。" +
                            "\n7 厂房的同一结构单元内，不应采用不同的结构形式；厂房端部应设屋架，不应采用山墙承重；厂房单元内不应采用横墙和排架混合承重。" +
                            "\n8 厂房柱距宜相等，各柱列的侧移刚度宜均匀，当有抽柱时，应采取抗震加强措施。"+
                            "\n\n（二）单层钢结构厂房结构布置一般规定" + 
                "\n钢柱、钢屋架或钢屋面梁承重的单层厂房的结构体系应符合下列要求（单层的轻型钢结构厂房的抗震设计，应符合专门的规定）：" +
                "\n1 厂房的横向抗侧力体系，可采用刚接框架、铰接框架、门式刚架或其他结构体系。厂房的纵向抗侧力体系，8、9度应采用柱间支撑；6、7度宜采用柱间支撑，也可采用刚接框架。"+
                "\n2 厂房内设有桥式起重机时，起重机梁系统的构件与厂房框架柱的连接应能可靠地传递纵向水平地震作用。"+
                "\n3 屋盖应设置完整的屋盖支撑系统。屋盖横梁与柱顶铰接时，宜采用螺栓连接。" +
                "\n\n（三）单层砖柱厂房结构布置一般规定" + 
                "\n1 厂房两端均应设置砖承重山墙。"+
                "\n2 与柱等高并相连的纵横内隔墙宜采用砖抗震墙。"+
                "\n3 防震缝设置应符合下列规定："+
                "\n  1)轻型屋盖厂房，可不设防震缝；"+
                "\n  2)钢筋混凝土屋盖厂房与贴建的建(构)筑物间宜设防震缝，防震缝的宽度可采用50mm～70mm，防震缝处应设置双柱或双墙。"+
                "\n4 天窗不应通至厂房单元的端开间，天窗不应采用端砖壁承重。";
        }

        private void button9_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = "适用于采用拱、平面桁架、立体桁架、网架、网壳、张弦梁、弦支穹顶等基本形式及其组合而成的大跨度钢屋盖建筑。" + 
                "\n\n（一）屋盖及其支承结构的选型和布置要求" +
                "\n1 应能将屋盖的地震作用有效地传递到下部支承结构。"+
                "\n2 应具有合理的刚度和承载力分布，屋盖及其支承的布置宜均匀对称。"+
                "\n3 宜优先采用两个水平方向刚度均衡的空间传力体系。"+
                "\n4 结构布置宜避免因局部削弱或突变形成薄弱部位，产生过大的内力、变形集中。对于可能出现的薄弱部位，应采取措施提高其抗震能力。"+
                "\n5 宜采用轻型屋面系统。"+
                "\n6 下部支承结构应合理布置，避免使屋盖产生过大的地震扭转效应。"+
                "\n\n（二）屋盖体系结构布置规定" +
                "\n1 单向传力体系的结构布置，应符合下列规定：" +
                "\n  1)主结构(桁架、拱、张弦梁)间应设置可靠的支撑，保证垂直于主结构方向的水平地震作用的有效传递；"+
                "\n  2)当桁架支座采用下弦节点支承时，应在支座间设置纵向桁架或采取其他可靠措施，防止桁架在支座处发生平面外扭转。"+
                "\n2 空间传力体系的结构布置，应符合下列规定："+
                "\n1)平面形状为矩形且三边支承一边开口的结构，其开口边应加强，保证足够的刚度。"+
                "\n2)两向正交正放网架、双向张弦梁，应沿周边支座设置封闭的水平支撑。"+
                "\n3)单层网壳应采用刚接节点。"+
                "\n（注：单向传力体系指平面拱、单向平面桁架、单向立体桁架、单向张弦梁等结构形式；空间传力体系指网架、网壳、双向立体桁架、双向张弦梁和弦支穹顶等结构形式。）";
        }
    }
}
