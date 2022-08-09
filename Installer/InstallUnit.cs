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
        private string path;//путь из коготорого запускаются установочные файлы // для запуска из cmd
        private string arg;//часть того, что вводится в командную строку после ее открытия // для запуска из cmd
        private string filename;//имя запускаемого файла, может быть cmd или установочный exe
        private string startarg;//аргументы при запуске файла 
        private bool write;//true - вводит нужные текстовые данные(обычно команды) в запущенной программе(обычно в cmd), false - не вводит
        private string writearg;//все текстовые данные, которые вводятся 
        
        public InstallUnit()
        {
            Path =  Directory.GetCurrentDirectory();
        }
        public InstallUnit(string arg1) : this()//конструктор с аргументом для командной строки, путь где находятся файлы это одна директория с установщиком
        {
            Arg = arg1;
            filename = "cmd.exe";
            startarg = "";
            write = true;
            writearg = "pushd " + Path + "\ncd resfiles\n" + Arg;
        }
        public InstallUnit(string arg1,string filename1, string startarg1) : this(arg1) // startarg это аргументы запуска
        {
            filename = filename1;
            startarg = startarg1;
            write = true;
            writearg = "pushd " + Path + "\ncd resfiles\n" + Arg;//was writearg = "pushd " + Path + "\n cd ..\n cd ..\n cd ..\ncd resfiles\n" + Arg;
        }
        
        public InstallUnit(string arg1, string filename1, string startarg1, bool write1) : this(arg1, filename1, startarg1)
        {
            write = write1;//при write == false теряют смысл в существовании поля writearg, Path, Arg 
            writearg = "pushd " + Path + "\ncd resfiles\n" + Arg;//was writearg = "pushd " + Path + "\n cd ..\n cd ..\n cd ..\ncd resfiles\n" + Arg;
        }
        public InstallUnit(string arg1, string filename1, string startarg1, bool write1,string writearg1) : this(arg1, filename1, startarg1, write1)
        {
            writearg = writearg1;
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
            cmd.StartInfo.FileName = filename;//изменяемое// обычно это "cmd.exe"
            cmd.StartInfo.Verb = "runas";//права администратора
            cmd.StartInfo.Arguments = startarg;//изменяемое// обычно ""
            cmd.StartInfo.RedirectStandardInput = true;
            cmd.StartInfo.RedirectStandardOutput = true;
            cmd.StartInfo.CreateNoWindow = true;//выполнение без открытия окна // обычно true
            cmd.StartInfo.UseShellExecute = false;
            cmd.Start();
            
            if (write == true)
            {
                cmd.StandardInput.WriteLine(writearg);//переход в директорию resfiles и ввод комманд в командную строку или другую программу
                cmd.StandardInput.Flush();
                cmd.StandardInput.Close();
                cmd.WaitForExit();
                Console.WriteLine(cmd.StandardOutput.ReadToEnd());
            }
            //Process Currentprocess = Process.GetCurrentProcess();//отладочный вывод
            //Debug.WriteLine(Currentprocess.ToString() + "нужный процесс");//отладочный вывод
            
            //cmd.Close();
        }
    }
}