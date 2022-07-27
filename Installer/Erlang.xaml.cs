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
    /// Interaction logic for Erlang.xaml
    /// </summary>
    public partial class Erlang : Page
    {
        public Erlang()
        {
            InitializeComponent();
        }
        /*
        private void cmd(string path, string arg)//установка через командую строку
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
            private void btn_ErlangInstall_Click(object sender, RoutedEventArgs e)
        {
            //cmd(System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "start otp_win64_24.3.4.exe");
            InstallUnit Erlang = new InstallUnit("start otp_win64_24.3.4.exe");//установка Erlang через класс InstallUnit
            Erlang.CmdInstall();

        }

        private void next3_btn_Click(object sender, RoutedEventArgs e)
        {
            Erl.Content = new RabbitMQ();
        }
    }
}
