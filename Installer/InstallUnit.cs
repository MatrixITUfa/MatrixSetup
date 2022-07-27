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
    public class InstallUnit
    {
        private string path;
        private string arg;
        InstallUnit() { }
        public InstallUnit(string arg1)//конструктор с аргументом для командной строки, путь где находятся файлы это одна директория с установщиком
        {
            Path = System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            Arg = arg1;
        }
        public InstallUnit(string path1,string arg1)//конструктор с путем, где находятся файлы и аргументом для командной строки
        {
            Path = path1;
            Arg = arg1;
        }
        public string Path//путь к директории, в которой хранятся установочные файлы
        {
            get 
            {
                return path;
            }
            set
            {
                path = value;   // устанавливаем новое значение свойства
            }
        }
        public string Arg//строка с аргументами для командной строки
        {
            get 
            {
                return arg;
            }
            set
            {
                arg = value;   // устанавливаем новое значение свойства
            }

        }
        

        public virtual void CmdInstall()//установка через командую строку
        {
            Process cmd = new Process();
            cmd.StartInfo.FileName = "cmd.exe";
            cmd.StartInfo.Verb = "runas";//права администратора
            cmd.StartInfo.RedirectStandardInput = true;
            cmd.StartInfo.RedirectStandardOutput = true;
            cmd.StartInfo.CreateNoWindow = true;//выполнение без открытия окна
            cmd.StartInfo.UseShellExecute = false;
            cmd.Start();

            cmd.StandardInput.WriteLine("pushd " + path + "\n cd ..\n cd ..\n cd ..\ncd resfiles\n" + arg);//переход в директорию resfiles и выполенение аргумента для командной строки
            cmd.StandardInput.Flush();
            cmd.StandardInput.Close();
            cmd.WaitForExit();
            Console.WriteLine(cmd.StandardOutput.ReadToEnd());
        }
    }
    class JavaInstallUnit : InstallUnit //версия класса InstallUnit для установки Java
    {
        private string javapath;
        public string JavaPath// дополнительное свойство, если пользователь задаст свой путь установки Java
        {
            set
            {
                javapath = value;   // устанавливаем новое значение свойства
            }
            get 
            {
                if (javapath == null) return "0";//в случае если пользователь не задал свой путь для установки, то возвращает 0 и устанавливает в папку по умолчанию
                else return javapath;
            }

        }

        public JavaInstallUnit(string arg1) :base(arg1) { }
        public JavaInstallUnit(string arg1,string javapath1) : base(arg1) 
        {
            JavaPath = javapath1;
        }
        public override void CmdInstall() 
        {
            Process cmd = new Process();
            cmd.StartInfo.FileName = "cmd.exe";
            cmd.StartInfo.Verb = "runas";
            cmd.StartInfo.RedirectStandardInput = true;
            cmd.StartInfo.RedirectStandardOutput = true;
            cmd.StartInfo.CreateNoWindow = true;
            cmd.StartInfo.UseShellExecute = false;
            cmd.Start();

            cmd.StandardInput.WriteLine("pushd " + Path + "\n cd ..\n cd ..\n cd ..\ncd resfiles\n" + Arg);
            cmd.StandardInput.Flush();
            cmd.StandardInput.Close();
            cmd.WaitForExit();
            Console.WriteLine(cmd.StandardOutput.ReadToEnd());
            if (JavaPath != "0") JavaConfigUpdate();//если не 0, то значит пользователь установил свой путь для установки Java
        }
        private void JavaConfigUpdate() 
        {
            string newpath = (System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location).Remove(24)+ "\\resfiles");
            File.WriteAllText(newpath, String.Empty);
            StreamWriter javaconfig = new StreamWriter(newpath);
            javaconfig.WriteLine("INSTALLDIR="+JavaPath+"\nINSTALL_SILENT = Enable\nREBOOT = Disable");//задание нового установочного пути
            javaconfig.Close();
        }
    }
}
