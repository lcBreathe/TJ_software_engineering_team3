using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace DatabaseProject
{
    public partial class Form6 : Form
    {
        public Form6()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            saveFileDialog.Title = "Save Text File";
            saveFileDialog.DefaultExt = "txt";
            saveFileDialog.AddExtension = true;
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                // 获取用户选择的文件路径
                string filePath = saveFileDialog.FileName;
                try
                {
                    // 将TextBox中的内容写入文件
                    File.WriteAllText(filePath, textBox1.Text);
                    MessageBox.Show("文件保存成功!", "提示");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("保存文件时出错!", "错误提示");
                }
            }

        }

        private void Form6_Load(object sender, EventArgs e)
        {
            textBox1.Text = class1.content;
        }
    }
}
