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
    //public delegate void ProgressBarHandler(string taskName);
    
    public class InstallUnit
    {
        private string path;//путь из коготорого запускаются установочные файлы // для запуска из cmd
        private string arg;//часть того, что вводится в командную строку после ее открытия // для запуска из cmd
        private string filename;//имя запускаемого файла, может быть cmd или установочный exe
        private string startarg;//аргументы при запуске файла 
        private bool write;//true - вводит нужные текстовые данные(обычно команды) в запущенной программе(обычно в cmd), false - не вводит
        private string writearg;//все текстовые данные, которые вводятся 
        public string taskName;//имя процесса для отображения пользователю
        //public static ProgressBarHandler Notify;
        public InstallUnit()
        {
            Path =  Directory.GetCurrentDirectory();
        }

        public InstallUnit(string Arg, string taskName) : this()//конструктор с аргументом для командной строки, путь где находятся файлы это одна директория с установщиком
        {
            //this.Notify = Notify;
            this.taskName = taskName;
            this.Arg = Arg;
            filename = "cmd.exe";
            startarg = "";
            write = true;
            writearg = "chcp 1251 pushd " + this.Path + "\newcd resfiles\new" + this.Arg;
        }
        public InstallUnit(string Arg,string filename, string startarg, string taskName) : this(Arg, taskName) // startarg это аргументы запуска
        {
            this.filename = filename;
            this.startarg = startarg;
            write = true;
            writearg = "chcp 1251 pushd " + this.Path + "\newcd resfiles\new" + this.Arg;//was writearg = "pushd " + Path + "\n cd ..\n cd ..\n cd ..\ncd resfiles\n" + Arg;
        }
        
        public InstallUnit(string Arg, string filename, string startarg, bool write, string taskName) : this(Arg, filename, startarg, taskName)
        {
            this.write = write;//при write == false теряют смысл в существовании поля writearg, Path, Arg 
            writearg = "chcp 1251 pushd " + this.Path + "\newcd resfiles\new" + this.Arg;//was writearg = "pushd " + Path + "\n cd ..\n cd ..\n cd ..\ncd resfiles\n" + Arg;
        }
        public InstallUnit(string Arg, string filename, string startarg, bool write,string writearg, string taskName) : this(Arg, filename, startarg, write, taskName)
        {
            this.writearg = writearg;
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
     
        public virtual void CmdRun()//установка через командую строку
        {
            //Notify.Invoke(taskName);
            
            //вызов события // в событие передается имя задачи и то, насколько заполняется прогрессбар
            Process cmd = new Process();
            cmd.StartInfo.FileName = filename;//изменяемое// обычно это "cmd.exe"
            cmd.StartInfo.Verb = "runas";//права администратора
            cmd.StartInfo.Arguments = startarg;//изменяемое// обычно "" еще /K
            cmd.StartInfo.RedirectStandardInput = true;
            cmd.StartInfo.RedirectStandardOutput = true;
            cmd.StartInfo.CreateNoWindow = true;//выполнение без открытия окна // обычно true
            cmd.StartInfo.UseShellExecute = false;
            cmd.Start();
            
            if (write == true)
            {
                string[] writeargSplit;
                writeargSplit = writearg.Split("\new");
                foreach(string arg in writeargSplit) 
                {
                    cmd.StandardInput.WriteLine(arg);//ввод комманд в командную строку или другую программу
                }
                
                cmd.StandardInput.Flush();
                cmd.StandardInput.Close();
                cmd.WaitForExit();
                Console.WriteLine(cmd.StandardOutput.ReadToEnd());
            }
            //Process Currentprocess = Process.GetCurrentProcess();//отладочный вывод
            //Debug.WriteLine(Currentprocess.ToString() + "нужный процесс");//отладочный вывод
            
            
        }
    }
}