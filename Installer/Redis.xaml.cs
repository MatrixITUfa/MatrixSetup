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
        
        private void btn_RedisInstall_Click(object sender, RoutedEventArgs e)
        {
            InstallUnit Redis = new InstallUnit("cd Redis\nredis-server --service-install\nredis-server --service-start");//установка Redis через класс InstallUnit
            Redis.CmdInstall();

        }

        private void next2_btn_Click(object sender, RoutedEventArgs e)
        {
            Redis1.Content = new IsRabbitHere();
            
        }
    }
}
