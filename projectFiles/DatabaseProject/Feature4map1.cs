using CefSharp.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DatabaseProject
{
    public partial class Feature4map1 : Form
    {
        private string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
        public Feature4map1()
        {
            InitializeComponent();
        }

        private void Feature4map1_Load(object sender, EventArgs e)
        {
            string path = Path.Combine(baseDirectory, @"..\..\..\所选时期内各省破坏性地震发生次数统计图.html");
            chromiumWebBrowser1.Load(path);
        }
    }
}
