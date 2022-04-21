﻿using System;
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
            cmdJava(System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location));
        }

        private void btn_javainstall_Click(object sender, RoutedEventArgs e)
        {
            Java javainastall = new Java();
            if (txtbox_javapath.Text != String.Empty) javainastall.Path = txtbox_javapath.Text;
            javainastall.InstallJava();
        }

        private void showtest_btn_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location));
        }

        private void cmdJava(string path) 
        {
            Process cmd = new Process();
            cmd.StartInfo.FileName = "cmd.exe";
            cmd.StartInfo.Verb = "runas";
            cmd.StartInfo.RedirectStandardInput = true;
            cmd.StartInfo.RedirectStandardOutput = true;
            cmd.StartInfo.CreateNoWindow = true;
            cmd.StartInfo.UseShellExecute = false;
            cmd.Start();

            cmd.StandardInput.WriteLine("pushd " + path + "\n cd ..\n cd ..\n cd ..\ncd resfiles\njre-8u321-windows-x64.exe INSTALLCFG=\"%cd%\\config.cfg\"");
            cmd.StandardInput.Flush();
            cmd.StandardInput.Close();
            cmd.WaitForExit();
            Console.WriteLine(cmd.StandardOutput.ReadToEnd());
        }

        private void cmdNeo4j(string path)
        {
            Process cmd = new Process();
            cmd.StartInfo.FileName = "cmd.exe";
            cmd.StartInfo.Verb = "runas";
            cmd.StartInfo.RedirectStandardInput = true;
            cmd.StartInfo.RedirectStandardOutput = true;
            cmd.StartInfo.CreateNoWindow = true;
            cmd.StartInfo.UseShellExecute = false;
            cmd.Start();

            cmd.StandardInput.WriteLine("pushd " + path + "\n cd ..\n cd ..\n cd ..\ncd resfiles\ncd neo4j-community-3.2.1\ncd bin\nneo4j install-service\nneo4j start");
            cmd.StandardInput.Flush();
            cmd.StandardInput.Close();
            cmd.WaitForExit();
            Console.WriteLine(cmd.StandardOutput.ReadToEnd());
            
        }

        private void btn_testNeo4j_Click(object sender, RoutedEventArgs e)
        {
            cmdNeo4j(System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location));
        }
    }
}