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
            private void btn_ErlangInstall_Click(object sender, RoutedEventArgs e)
            {
                
                InstallUnit Erlang = new InstallUnit("start otp_win64_24.3.4.exe /S");//установка Erlang через класс InstallUnit
                Erlang.CmdInstall();

            }

        private void next3_btn_Click(object sender, RoutedEventArgs e)
        {
            Erl.Content = new RabbitMQ();
        }
    }
}
