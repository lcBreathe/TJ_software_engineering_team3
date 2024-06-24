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
    public partial class Feature4map2 : Form
    {
        private string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
        public Feature4map2()
        {
            InitializeComponent();
        }

        private void Feature4map2_Load(object sender, EventArgs e)
        {
            string path = Path.Combine(baseDirectory, @"..\..\..\所选时期内各省最大震级统计图.html");
            chromiumWebBrowser1.Load(path);
        }
    }
}
