using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.IO;

namespace DatabaseProject
{
    public partial class Feature1 : Form
    {
        private string connectionString = "Data source=ZHANGX;Initial catalog=Seismic_information_platform;Integrated Security=True";
        private Stopwatch stopwatch;
        private System.Windows.Forms.Timer timer;
        public Feature1()
        {
            InitializeComponent();

            // 初始化计时器
            stopwatch = new Stopwatch();
            timer = new System.Windows.Forms.Timer();
            timer.Interval = 1000; // 1秒钟更新一次
            timer.Tick += Timer_Tick;
        }

        private void Feature1_Load(object sender, EventArgs e)
        {
            LoadDataFromDatabase();
        }

        private void LoadDataFromDatabase()
        {
            string query = "SELECT * FROM history ORDER BY [happentime] DESC"; // 查询语句

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();

                adapter.Fill(dataTable);

                // 修改列头名称
                dataTable.Columns["city"].ColumnName = "发震城市";
                dataTable.Columns["magnitude"].ColumnName = "震级（M）";
                dataTable.Columns["happentime"].ColumnName = "发震时间（UTC+8）";
                dataTable.Columns["depth"].ColumnName = "深度（千米）";
                dataTable.Columns["we"].ColumnName = "维度（°）";
                dataTable.Columns["sn"].ColumnName = "经度（°）";
                dataTable.Columns["province"].ColumnName = "所在省份";
                dataTable.Columns["describe"].ColumnName = "具体位置";

                dataGridView1.DataSource = dataTable; // 将查询结果绑定到DataGridView
            }
        }

        private async void button1_Click(object sender, EventArgs e)
        {

            string batFilePath = @"..\..\..\Seismic_information_processing.bat";

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

                    // 开始计时
                    stopwatch.Start();
                    timer.Start();

                    // 启动进程
                    process.Start();

                    // 异步等待进程结束
                    await Task.Run(() => process.WaitForExit());

                    // 停止计时
                    stopwatch.Stop();
                    timer.Stop();

                    // 显示运行结束的MessageBox
                    textBox1.Text = ("更新结束!已生成“近一年全球地震情况汇总.csv”文件");
                    MessageBox.Show("更新结束!已生成“近一年全球地震情况汇总.csv”文件");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Batch file not found!");
            }
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            if (stopwatch.IsRunning)
            {
                TimeSpan elapsedTime = stopwatch.Elapsed;
                textBox1.Text = ($"更新中，累计时间：{elapsedTime.Hours:00}:{elapsedTime.Minutes:00}:{elapsedTime.Seconds:00}");
            }
        }



        private void button2_Click(object sender, EventArgs e)
        {
            string filePath = @"..\..\..\近一年全球地震情况汇总.csv";
            ImportCSV(filePath);
        }
        private void ImportCSV(string filePath)
        {
            string tableName = "history";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (StreamReader reader = new StreamReader(filePath))
                    {
                        // Skip the first line (headers)
                        reader.ReadLine();

                        while (!reader.EndOfStream)
                        {
                            string line = reader.ReadLine();
                            string[] data = line.Split(',');

                            if (data.Length < 8)
                            {
                                continue;
                            }

                            string city = data[7];
                            decimal magnitude = decimal.Parse(data[1]);
                            DateTime happentime = DateTime.Parse(data[0]);
                            decimal depth = decimal.Parse(data[4]);
                            decimal we = decimal.Parse(data[3]);
                            decimal sn = decimal.Parse(data[2]);
                            string describe = data[5];
                            string province = data[6];

                            // Check if the data already exists in the database
                            if (!CheckDuplicateData(connection, tableName, city, happentime, we, sn) && CityExists(connection, city))
                            {
                                try
                                {
                                    using (SqlCommand insertCommand = new SqlCommand(
                                        $"INSERT INTO {tableName} ([city], [magnitude], [happentime], [depth], [we], [sn], [describe], [province]) " +
                                        $"VALUES (@City, @Magnitude, @Happentime, @Depth, @We, @Sn, @Describe, @Province)", connection))
                                    {
                                        insertCommand.Parameters.AddWithValue("@City", city);
                                        insertCommand.Parameters.AddWithValue("@Magnitude", magnitude);
                                        insertCommand.Parameters.AddWithValue("@Happentime", happentime);
                                        insertCommand.Parameters.AddWithValue("@Depth", depth);
                                        insertCommand.Parameters.AddWithValue("@We", we);
                                        insertCommand.Parameters.AddWithValue("@Sn", sn);
                                        insertCommand.Parameters.AddWithValue("@Describe", describe);
                                        insertCommand.Parameters.AddWithValue("@Province", province);

                                        insertCommand.ExecuteNonQuery();
                                    }
                                }
                                catch (SqlException ex)
                                {
                                    if (ex.Number == 547 || ex.Number == 2627 || ex.Number == 2601)
                                    {
                                        // Skip import for conflicting or duplicate data
                                        continue;
                                    }
                                    else
                                    {
                                        throw; // Re-throw other exceptions
                                    }
                                }
                            }
                        }
                        MessageBox.Show("导入完成");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error importing CSV data: {ex.Message}");
            }
        }

        private bool CheckDuplicateData(SqlConnection connection, string tableName, string city, DateTime happentime, decimal we, decimal sn)
        {
            using (SqlCommand command = new SqlCommand(
                $"SELECT COUNT(*) FROM {tableName} WHERE [city] = @City AND [happentime] = @Happentime AND [we] = @We AND [sn] = @Sn", connection))
            {
                command.Parameters.AddWithValue("@City", city);
                command.Parameters.AddWithValue("@Happentime", happentime);
                command.Parameters.AddWithValue("@We", we);
                command.Parameters.AddWithValue("@Sn", sn);

                int count = (int)command.ExecuteScalar();
                return count > 0;
            }
        }

        private bool CityExists(SqlConnection connection, string city)
        {
            using (SqlCommand command = new SqlCommand(
                $"SELECT COUNT(*) FROM geography WHERE [city] = @City", connection))
            {
                command.Parameters.AddWithValue("@City", city);
                int count = (int)command.ExecuteScalar();
                return count > 0;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string query = "SELECT * FROM history ORDER BY [happentime] DESC"; // 查询语句

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();

                adapter.Fill(dataTable);

                // 修改列头名称
                dataTable.Columns["city"].ColumnName = "发震城市";
                dataTable.Columns["province"].ColumnName = "所在省份";
                dataTable.Columns["magnitude"].ColumnName = "震级（M）";
                dataTable.Columns["happentime"].ColumnName = "发震时间（UTC+8）";
                dataTable.Columns["depth"].ColumnName = "深度（千米）";
                dataTable.Columns["we"].ColumnName = "维度（°）";
                dataTable.Columns["sn"].ColumnName = "经度（°）";
                dataTable.Columns["describe"].ColumnName = "具体位置";

                dataGridView1.DataSource = dataTable; // 将查询结果绑定到DataGridView
            }
        }

    }
}
