using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
using System.IO;
using System.Diagnostics;
using System.Reflection;

namespace Installer
{
    /// <summary>
    /// Interaction logic for Redis.xaml
    /// </summary>
    public partial class Redis : Page
    {
        public Redis()
        {
            InitializeComponent();
        }
        /*private void cmd(string path, string arg)//установка через командую строку
        {
            Process cmd = new Process();
            cmd.StartInfo.FileName = "cmd.exe";
            cmd.StartInfo.Verb = "runas";
            cmd.StartInfo.RedirectStandardInput = true;
            cmd.StartInfo.RedirectStandardOutput = true;
            cmd.StartInfo.CreateNoWindow = true;
            cmd.StartInfo.UseShellExecute = false;
            cmd.Start();

            cmd.StandardInput.WriteLine("pushd " + path + "\n cd ..\n cd ..\n cd ..\ncd resfiles\n" + arg);
            cmd.StandardInput.Flush();
            cmd.StandardInput.Close();
            cmd.WaitForExit();
            Console.WriteLine(cmd.StandardOutput.ReadToEnd());
        }*/
        private void btn_RedisInstall_Click(object sender, RoutedEventArgs e)
        {
            //cmd(System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "cd Redis\nredis-server --service-install\nredis-server --service-start");
            InstallUnit Redis = new InstallUnit("cd Redis\nredis-server --service-install\nredis-server --service-start");//установка Redis через класс InstallUnit
            Redis.CmdInstall();

        }

        private void next2_btn_Click(object sender, RoutedEventArgs e)
        {
            Redis1.Content = new IsRabbitHere();
            
        }
    }
}
