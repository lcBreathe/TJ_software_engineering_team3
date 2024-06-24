using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CefSharp;
using CefSharp.WinForms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace DatabaseProject
{
    public partial class Feature4 : Form
    {
        private string connectionString = "Data source=ZHANGX;Initial catalog=Seismic_information_platform;Integrated Security=True";
        public Feature4()
        {
            InitializeComponent();
        }



        private void Feature4_Load(object sender, EventArgs e)
        {
            // 设置 DateTimePicker 控件的格式和初始值
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "yyyy-MM-dd";
            dateTimePicker1.Value = DateTime.Now;

            dateTimePicker2.Format = DateTimePickerFormat.Custom;
            dateTimePicker2.CustomFormat = "yyyy-MM-dd";
            dateTimePicker2.Value = DateTime.Now;
        }
        private  async void button1_Click(object sender, EventArgs e)
        {
            // 获取 DateTimePicker 的值
            DateTime startTime = dateTimePicker1.Value;
            DateTime endTime = dateTimePicker2.Value;
            // 查询数据库并生成CSV文件
            GenerateCsv(startTime, endTime);
            InfoMap();
            MapShow();

        }
        private async void InfoMap()
        {

            string batFilePath = @"..\..\..\Earthquake_mapping.bat";

            if (System.IO.File.Exists(batFilePath))
            {
                try
                {
                    // 创建进程对象
                    Process process = new Process();

                    // 配置进程启动信息
                    ProcessStartInfo startInfo = new ProcessStartInfo
                    {
                        FileName = batFilePath,
                        CreateNoWindow = true,  // 设置不显示CMD窗口
                        UseShellExecute = false // 不使用默认的Shell启动
                    };

                    process.StartInfo = startInfo;

                    // 启动进程
                    process.Start();

                    // 异步等待进程结束
                    await Task.Run(() => process.WaitForExit());

                    // 显示运行结束的MessageBox
                    MessageBox.Show("生成全国地震数据地图成功");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("数据未正确生成，请重新选择时间段生成地图");
            }
        }

        private void MapShow()
        {
            Feature4map1 newForm1 = new Feature4map1(); // 创建新窗体实例
            newForm1.Show(); // 显示新窗体
            Feature4map2 newForm2 = new Feature4map2(); // 创建新窗体实例
            newForm2.Show(); // 显示新窗体
        }
        private void GenerateCsv(DateTime startTime, DateTime endTime)
        {
            string query = "SELECT city,province, magnitude, depth, we, sn, describe, happentime FROM history WHERE happentime BETWEEN @StartTime AND @EndTime";
            DataTable dataTable = new DataTable();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@StartTime", startTime);
                    command.Parameters.AddWithValue("@EndTime", endTime);

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(dataTable);
                }
            }
            // 生成 CSV 文件
            string csvFilePath = @"..\..\..\所选时期地震数据.csv";
            using (StreamWriter writer = new StreamWriter(csvFilePath, false, Encoding.UTF8))
            {
                // 写入列名
                for (int i = 0; i < dataTable.Columns.Count; i++)
                {
                    writer.Write(dataTable.Columns[i]);
                    if (i < dataTable.Columns.Count - 1)
                    {
                        writer.Write(",");
                    }
                }
                writer.WriteLine();

                // 写入行数据
                foreach (DataRow row in dataTable.Rows)
                {
                    for (int i = 0; i < dataTable.Columns.Count; i++)
                    {
                        writer.Write(row[i].ToString());
                        if (i < dataTable.Columns.Count - 1)
                        {
                            writer.Write(",");
                        }
                    }
                    writer.WriteLine();
                }
            }
        }
    }
}
