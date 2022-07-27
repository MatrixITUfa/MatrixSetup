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
    /// Interaction logic for RabbitMQ.xaml
    /// </summary>
    public partial class RabbitMQ : Page
    {
        public RabbitMQ()
        {
            InitializeComponent();
        }

        private void btn_RabbitInstall_Click(object sender, RoutedEventArgs e)
        {
            //cmd(System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "start rabbitmq-server-3.10.1.exe");//старая установка через функцию cmd
            InstallUnit Rabbit = new InstallUnit("start rabbitmq-server-3.10.1.exe");//установка Rabbit через класс InstallUnit
            Rabbit.CmdInstall();
        }
    }
}