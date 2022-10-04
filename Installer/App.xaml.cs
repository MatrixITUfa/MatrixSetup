using System.Windows;

namespace Installer
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            RabbitMQStartPage window = new RabbitMQStartPage();
            window.Show();
        }
    }
}
