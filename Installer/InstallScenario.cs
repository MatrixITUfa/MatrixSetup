using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Win32;
using System.Linq;

namespace Installer
{
    public static class InstallScenario
    {
        private static InstallUnit erlangInstall = new InstallUnit("start otp_win64_24.3.4.exe /S", "Установка Erlang");//установка Erlang через класс InstallUnit
        private static InstallUnit rabbitInstall = new InstallUnit("start rabbitmq-server-3.10.1.exe /S", "Установка RabbitMQ");//установка Rabbit через класс InstallUnit
        private static InstallUnit rabbitServiceRemove = new InstallUnit(@"pushd C:\Program Files\RabbitMQ Server\rabbitmq_server-3.10.1\sbin" + "\new" + "rabbitmq-service remove", "Переустановка сервиса RabbitMQ");
        private static InstallUnit rabbitServiceInstall = new InstallUnit(@"pushd C:\Program Files\RabbitMQ Server\rabbitmq_server-3.10.1\sbin" + "\new" + "rabbitmq-service install", "Переустановка сервиса RabbitMQ");
        private static InstallUnit stopRabbitService = new InstallUnit("", "net", "stop RabbitMQ", false, "Перезапуск RabbitMQ");
        private static InstallUnit startRabbitService = new InstallUnit("", "net", "start RabbitMQ", false, "Перезапуск RabbitMQ");
        private static InstallUnit rabbitPluginsEnable = new InstallUnit(@"pushd C:\Program Files\RabbitMQ Server\rabbitmq_server-3.10.1\sbin" + "\new" + "rabbitmq-plugins enable rabbitmq_management", "Запуска плагина RabbitMQ");
        private static InstallUnit java = new InstallUnit("jre - 8u321 - windows - x64.exe INSTALLCFG =\"%cd%\\config.cfg\"", "Установка Java");//установка Java через класс InstallUnit
        private static InstallUnit neo4j = new InstallUnit("cd neo4j - community - 3.2.1\ncd bin\nneo4j install - service\nneo4j start", "Установка Neo4j");//установка Neo4j через класс InstallUnit

        public static List<string> MustInstalled = new List<string>()
        {
            "erlang",
            "rabbitmq",
          //"redis",
          //"java",
          //"neo4j"
        };
        public static void AddInstall(string install) 
        {
            MustInstalled.Add(install);
        }
        public static void RemoveInstall(string install) 
        {
            MustInstalled.Remove(install);
        }
        public static void DefaultInstall()
        {
            InstallSelected(MustInstalled);
            

            if(Check().Count != MustInstalled.Count) Reinstall();
        }
        public static List<string> Check()
        {
            List<string> Installs = new List<string>();
            List<string> DetectedInstalls = new List<string>();
            
            List<string> keys = new List<string>()
            {
                @"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall",
                @"SOFTWARE\WOW6432Node\Microsoft\Windows\CurrentVersion\Uninstall"
            };

            // The RegistryView.Registry64 forces the application to open the registry as x64 even if the application is compiled as x86 
            FindInstalls(RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64), keys, Installs);
            FindInstalls(RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry64), keys, Installs);

            Installs = Installs.Where(s => !string.IsNullOrWhiteSpace(s)).Distinct().ToList();
            Installs.Sort(); // The list of ALL installed applications

            foreach (string selectInstall in MustInstalled)
            {
                foreach (string install in Installs)
                {
                    if (install.ToLower().IndexOf(selectInstall) != -1)
                    {
                        DetectedInstalls.Add(selectInstall);
                    }
                }
            }

            return DetectedInstalls;

            void FindInstalls(RegistryKey regKey, List<string> keys, List<string> installed)
            {
                foreach (string key in keys)
                {
                    using (RegistryKey rk = regKey.OpenSubKey(key))
                    {
                        if (rk == null)
                        {
                            continue;
                        }
                        foreach (string skName in rk.GetSubKeyNames())
                        {
                            using (RegistryKey sk = rk.OpenSubKey(skName))
                            {
                                try
                                {
                                    installed.Add(Convert.ToString(sk.GetValue("DisplayName")));
                                }
                                catch (Exception ex)
                                { }
                            }
                        }
                    }
                }
            }
        }
        public static void InstallSelected(List<string> mustSelectInstalled)
        {
            foreach(string select in mustSelectInstalled)
            {
                switch (select)
                {
                    case "erlang":
                        erlangInstall.CmdRun();
                        Environment.SetEnvironmentVariable("ERLANG_HOME", @"C:\Program Files\erl-24.3.4");//установка переменной среды ERLANG_HOME //сделать, чтобы автоматически бралось имя erl
                        break;
                    case "rabbitmq":
                        rabbitInstall.CmdRun();
                        rabbitServiceRemove.CmdRun();
                        rabbitServiceInstall.CmdRun();
                        stopRabbitService.CmdRun();
                        startRabbitService.CmdRun();
                        rabbitPluginsEnable.CmdRun();
                        rabbitServiceRemove.CmdRun();
                        rabbitServiceInstall.CmdRun();
                        stopRabbitService.CmdRun();
                        startRabbitService.CmdRun();
                        break;
                    case "java":
                        java.CmdRun();
                        break;
                    case "neo4j":
                        neo4j.CmdRun();
                        break;
                }
            }
            Task wait = new Task(() =>
            {
                Thread.Sleep(5000);
            });
            wait.Start();
            wait.Wait();
        }
        public static void Reinstall()
        {
            List<string> detectedInstalls;
            detectedInstalls = Check();
            List<string> toReinstall;
            toReinstall = (System.Collections.Generic.List<string>)MustInstalled.Except(detectedInstalls);
            InstallSelected(toReinstall);
            if (Check().Count != MustInstalled.Count) Reinstall();
        }
    }
}
