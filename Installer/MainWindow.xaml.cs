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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            Main.Content = new Redis();
        }

        private void selectfile_btn_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog folderDialog = new System.Windows.Forms.FolderBrowserDialog();
            folderDialog.ShowNewFolderButton = false;
            folderDialog.SelectedPath = System.AppDomain.CurrentDomain.BaseDirectory;
            System.Windows.Forms.DialogResult result = folderDialog.ShowDialog();
            
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                txtbox_javapath.Text = folderDialog.SelectedPath;
            }
        }

        private void btn_test_Click(object sender, RoutedEventArgs e)
        {
            InstallUnit Java = new InstallUnit("jre - 8u321 - windows - x64.exe INSTALLCFG =\"%cd%\\config.cfg\"");//установка Java через класс InstallUnit
            Java.CmdInstall();
        }

        

        

        private void btn_testNeo4j_Click(object sender, RoutedEventArgs e)
        {
            InstallUnit Neo4j = new InstallUnit("cd neo4j - community - 3.2.1\ncd bin\nneo4j install - service\nneo4j start");//установка Neo4j через класс InstallUnit
            Neo4j.CmdInstall();
        }

        private void btn_location_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(Directory.GetCurrentDirectory());
        }
    }
}