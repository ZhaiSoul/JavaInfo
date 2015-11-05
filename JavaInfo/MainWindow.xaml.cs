using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace JavaInfo
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDialog1 = new Microsoft.Win32.OpenFileDialog();
            string JavaFilePath = "";

            openFileDialog1.RestoreDirectory = true;
            openFileDialog1.InitialDirectory = @"c:\";
            openFileDialog1.Filter = "|Java|java.exe";
            openFileDialog1.FileName = "javaw.exe";
            openFileDialog1.Title = "Find Java";
            if (openFileDialog1.ShowDialog() == true)
            {
                JavaFilePath = openFileDialog1.FileName.ToString();
            }
            else
            {
                return;
            }

            Process JavaProcess = new Process();
            JavaProcess.StartInfo.FileName = JavaFilePath;
            if (FileVersionInfo.GetVersionInfo(JavaProcess.StartInfo.FileName).FileDescription == "Java(TM) Platform SE binary")
            {
                JavaProcess.StartInfo.Arguments = "-version";
                JavaProcess.StartInfo.RedirectStandardError = true;
                JavaProcess.StartInfo.RedirectStandardInput = true;
                JavaProcess.StartInfo.RedirectStandardOutput = true;
                JavaProcess.StartInfo.UseShellExecute = false;
                JavaProcess.StartInfo.StandardOutputEncoding = Encoding.UTF8;
                JavaProcess.Start();
                JavaProcess.StandardInput.WriteLine(" ");
                JavaProcess.StandardInput.Flush();
                string result = JavaProcess.StandardError.ReadToEnd();
                try
                {
                    string JavaVersion = result.Substring(14, result.LastIndexOf("\"") - 14);
                    int i = result.LastIndexOf("\"");
                    int ab1 = result.IndexOf("HotSpot(TM) ");
                    int ab2 = result.IndexOf(" VM");
                    string JavaSystem = result.Substring(ab2 - 6, 6);
                    int ab3 = result.IndexOf(JavaSystem);
                    string JavaBit = result.Substring(ab1 + 12, ab3 - ab1 - 12);

                    if (JavaBit == "")
                    {
                        JavaBit = "32-Bit";
                    }
                    //中文提示 Chinese
                    /*
                    if (JavaBit.Equals("64-Bit "))
                    {
                        JavaBit = "64位";
                    }
                    else if (JavaBit == "")
                    {
                        JavaBit = "32位";
                    }
                    else
                    {
                        JavaBit = "未知版本";
                    }

                    if (JavaSystem == "Server")
                    {
                        JavaSystem = "企业版";
                    }
                    else if (JavaSystem == "Client")
                    {
                        JavaSystem = "客户端版";
                    }
                    else
                    {
                        JavaSystem = "OpenJDK";
                    }
                    */
                    int ab4 = JavaVersion.LastIndexOf(".");
                    int javaver = 0;
                    javaver = int.Parse(JavaVersion.Substring(2, JavaVersion.LastIndexOf(".") - 2));
                    lb_JavaVersion.Content = JavaVersion;
                    lb_JavaSystem.Content = JavaSystem;
                    lb_JavaBit.Content = JavaBit;
                    textBox.Text = result;
                }
                catch
                {
                    //textBox.Text = "检测失败！这可能不是一个Java主程序";Chinese 中文
                    textBox.Text = "Error!This is a wrong Java file!";
                }
            }
        }
    }
}
