using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Installer
{
    /// <summary>
    /// Interaction logic for IsRabbitHere.xaml
    /// </summary>
    public partial class IsRabbitHere : Page
    {
        public IsRabbitHere()
        {
            InitializeComponent();
        }

        private void btn_Have_Rabbit_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void btn_No_Rabbit_Click(object sender, RoutedEventArgs e)
        {
            Redis1.Content = new RabbitMQ();
        }
    }
}
