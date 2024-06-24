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
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using System.Reflection.Emit;

namespace DatabaseProject
{
    public partial class Feature3 : Form
    {
        private Stopwatch stopwatch;
        private System.Windows.Forms.Timer timer;
        private string targetDirectory = @"..\..\..\videos"; // 指定目标目录
        private string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
        private string batFilePath;
        private string selectedFolderPath;
        private string jsonPath;
        private string selectedFolderName;

        public Feature3()
        {
            InitializeComponent();
            LoadFolderNamesToListBox();
            InitializeTimer();
            PopulateListBox2();
        }

        private void InitializeTimer()
        {
            stopwatch = new Stopwatch();
            timer = new Timer();
            timer.Interval = 1000; // 每秒触发一次
            timer.Tick += Timer_Tick;
        }

        private void LoadFolderNamesToListBox()
        {
            // 清空 ListBox
            listBox1.Items.Clear();

            // 获取目标目录下的所有文件夹
            if (Directory.Exists(targetDirectory))
            {
                string[] directories = Directory.GetDirectories(targetDirectory);
                foreach (string directory in directories)
                {
                    listBox1.Items.Add(Path.GetFileName(directory));
                }
            }
        }
        private void PopulateListBox2()
        {
            // 清空 ListBox2
            listBox2.Items.Clear();

            // 获取目标目录下的所有文件夹
            if (Directory.Exists(targetDirectory))
            {
                string[] directories = Directory.GetDirectories(targetDirectory);
                foreach (string directory in directories)
                {
                    string jsonFilePath = Path.Combine(directory, "transforms.json");
                    if (File.Exists(jsonFilePath))
                    {
                        listBox2.Items.Add(Path.GetFileName(directory));
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Video files (*.mp4;*.avi;*.mkv;*.mov)|*.mp4;*.avi;*.mkv;*.mov";;
            openFileDialog.Multiselect = true; // 允许多选

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string targetDirectory = @"..\..\..\videos"; // 指定目标目录

                // 确保目录存在
                Directory.CreateDirectory(targetDirectory);

                foreach (string videoFilePath in openFileDialog.FileNames)
                {
                    string videoFileName = Path.GetFileNameWithoutExtension(videoFilePath);

                    // 创建不重复的文件夹名称
                    string newFolderPath = GetUniqueFolderPath(targetDirectory, videoFileName);

                    // 创建文件夹
                    Directory.CreateDirectory(newFolderPath);

                    // 复制视频文件到新文件夹中
                    string newFilePath = Path.Combine(newFolderPath, Path.GetFileName(videoFilePath));
                    File.Copy(videoFilePath, newFilePath);

                    MessageBox.Show("视频文件导入成功!", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // 在ListBox中显示文件夹名字
                    string newFolderName = Path.GetFileName(newFolderPath);
                    listBox1.Items.Add(newFolderName);
                }
            }
        }
        private string GetUniqueFolderPath(string targetDirectory, string folderName)
        {
            string newFolderPath = Path.Combine(targetDirectory, folderName);
            int counter = 1;
            while (Directory.Exists(newFolderPath))
            {
                newFolderPath = Path.Combine(targetDirectory, $"{folderName}_{counter}");
                counter++;
            }
            return newFolderPath;
        }


        private async void button2_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {
                selectedFolderName = listBox1.SelectedItem.ToString();
                selectedFolderPath = Path.Combine(targetDirectory, selectedFolderName);
                // 获取选定文件夹中的所有文件
                string[] files = Directory.GetFiles(selectedFolderPath, "*.mp4");
                if (files.Length > 0)
                {
                    string videoFileName = Path.GetFileName(files[0]);
                    string selectedVideoPath = Path.Combine(selectedFolderPath, videoFileName);
                    batFilePath = BatFileCreator(selectedFolderPath, selectedVideoPath);
                    await VideoProcessing();
                }
                else
                {
                    MessageBox.Show ($"所选文件夹: {selectedFolderPath}\n之中不包含视频文件，请重新选择");
                }
            }
            else
            {
                MessageBox.Show("请从列表中选择你所导入的视频");
            }
        }

        private async Task VideoProcessing()
        {
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
                    JsonProcessing();
                    // 显示运行结束的MessageBox
                    MessageBox.Show("视频处理完成");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("批处理文件错误，请重新处理视频");
            }
        }

        private void JsonProcessing()
        {
            //处理json文件
            jsonPath = Path.Combine(selectedFolderPath, "transforms.json");
            try
            {
                // 读取并解析JSON文件
                string jsonContent = File.ReadAllText(jsonPath);
                JObject jsonObj = JObject.Parse(jsonContent);

                // 遍历JSON对象并修改file_path
                UpdateFilePaths(jsonObj);

                // 保存修改后的JSON
                File.WriteAllText(jsonPath, jsonObj.ToString());

                // 将处理的selectedFolderName添加到listBox2中
                listBox2.Items.Add(selectedFolderName);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"处理JSON文件时出错: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private string BatFileCreator(string SelectedFolderPath, string SelectedVideoPath)
        {
            string batSelectedScriptsPath = Path.Combine(baseDirectory, @"..\..\..\Instant-NGP\scripts\colmap2nerf.py");
            string batSelectedFolderPath = Path.Combine(baseDirectory, SelectedFolderPath);
            string batSelectedVideoPath = Path.Combine(baseDirectory, SelectedVideoPath);
            string pathA = batSelectedScriptsPath;
            string pathB = batSelectedVideoPath;
            string pathC = Path.Combine(batSelectedFolderPath, "transforms.json");
            string batContent =
                "@echo off\n" +
                "(\n" +
                "echo y\n" +
                "echo y\n" +
                ") | python \"" + pathA + "\" --video_in \"" + pathB + "\" --run_colmap --out \"" + pathC + "\"\n" +
                "exit";

            string targetBatDirectory = SelectedFolderPath; 

            if (!Directory.Exists(targetBatDirectory))
            {
                Directory.CreateDirectory(targetBatDirectory);
            }

            string batFilePath = Path.Combine(targetBatDirectory, "Video_processing.bat");
            File.WriteAllText(batFilePath, batContent);
            return batFilePath;
        }

        
        private void UpdateFilePaths(JObject jsonObj)
        {
            foreach (var property in jsonObj.Properties())
            {
                if (property.Name == "file_path")
                {
                    string filePath = property.Value.ToString();
                    string updatedPath = UpdatePath(filePath);
                    property.Value = updatedPath;
                }
                else if (property.Value.Type == JTokenType.Object)
                {
                    UpdateFilePaths((JObject)property.Value);
                }
                else if (property.Value.Type == JTokenType.Array)
                {
                    foreach (var item in (JArray)property.Value)
                    {
                        if (item.Type == JTokenType.Object)
                        {
                            UpdateFilePaths((JObject)item);
                        }
                    }
                }
            }
        }

        private string UpdatePath(string filePath)
        {
            // 更新路径逻辑：将"./"替换为""
            string updatedPath = filePath.Replace("./..\\..\\..\\videos\\"+ selectedFolderName+"\\", "");
            return updatedPath;
        }


        private void Timer_Tick(object sender, EventArgs e)
        {
            if (stopwatch.IsRunning)
            {
                TimeSpan elapsedTime = stopwatch.Elapsed;
                textBox1.Text = ($"视频处理中，累计时间：{elapsedTime.Hours:00}:{elapsedTime.Minutes:00}:{elapsedTime.Seconds:00}");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (listBox2.SelectedItem != null)
            {
                string selectedFolderName = listBox2.SelectedItem.ToString();
                string selectedFolderPath = Path.Combine(targetDirectory, selectedFolderName);
                string jsonFilePath = Path.Combine(selectedFolderPath, "transforms.json");

                if (File.Exists(jsonFilePath))
                {
                    Process.Start(@"..\..\..\Instant-NGP\instant-ngp.exe", jsonFilePath);
                }
                else
                {
                    MessageBox.Show($"所选文件夹: {selectedFolderPath}\n之中不包含transforms.json文件，请重新处理视频");
                }
            }
            else
            {
                MessageBox.Show("请从列表中选择已处理的视频");
            }
        }

    }
}
