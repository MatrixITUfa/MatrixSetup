using System.Windows;
using System.Diagnostics;


namespace Installer
{
    /// <summary>
    /// Interaction logic for RabbitMQStartPage.xaml
    /// </summary>
    public partial class RabbitMQStartPage : Window
    {
        public RabbitMQStartPage()
        {
            InitializeComponent();
        }
        
        private void btn_RabbitInstall_Click(object sender, RoutedEventArgs e)
        {
            InstallScenario.DefaultInstall();
            MessageBox.Show("Установка RabbitMQ завершена.");
            Process.Start("explorer.exe", @"http://localhost:15672");
            Installer.App.Current.Shutdown();
        }
    }
}
