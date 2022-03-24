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
            System.Diagnostics.Process TestProcess = new System.Diagnostics.Process();
            
            TestProcess.StartInfo.Verb = "runas";
            //TestProcess = System.Diagnostics.Process.Start("notepad.exe", "");
            TestProcess = System.Diagnostics.Process.Start("C:\\Users\\user\\source\\repos\\Installer\\Installer\\resfiles\\jre-8u321-windows-x64.exe", "INSTALLCFG=C:\\Users\\user\\source\\repos\\Installer\\Installer\\resfiles\\config.txt");
        }

        private void btn_javainstall_Click(object sender, RoutedEventArgs e)
        {
            Java javainastall = new Java();
            if (txtbox_javapath.Text != String.Empty) javainastall.Path = txtbox_javapath.Text;
            javainastall.InstallJava();
        }
    }
}