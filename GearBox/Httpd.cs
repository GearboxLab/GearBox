using System;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace GearBox
{
    public class Httpd
    {
        const string HTTPD_VERSION = "httpd-2.4.32-Win64-VC15";

        public void Start()
        {
            Thread thread = new Thread(new ThreadStart(StartHttpd))
            {
                IsBackground = true
            };

            thread.Start();

            Thread.Sleep(1000);
        }

        public void Stop()
        {
            int pid = GetHttpdPID();

            if (pid <= 0)
            {
                return;
            }

            Process process;

            try
            {
                process = Process.GetProcessById(pid);

                if (!process.WaitForExit(1000))
                {
                    process.Kill();
                }
            }
            catch (ArgumentException e)
            {
                return;
            }
        }

        public bool IsStarted()
        {
            int pid = GetHttpdPID();

            foreach (Process process in Process.GetProcesses())
            {
                if (process.ProcessName == "httpd" && (pid > 0 && process.Id == pid))
                {
                    return true;
                }
            }

            return false;
        }

        private void StartHttpd()
        {
            string gearboxRoot = GetGearBoxRoot();
            string serverRoot = gearboxRoot + "\\opt\\" + HTTPD_VERSION;
            string documentRoot = gearboxRoot + "\\var\\www";
            string confRoot = gearboxRoot + "\\etc\\" + HTTPD_VERSION;
            string logRoot = gearboxRoot + "\\var\\log\\httpd";

            Process process = new Process();
            
            process.StartInfo.EnvironmentVariables.Add("GEARBOX_ROOT", gearboxRoot.Replace("\\", "/"));
            process.StartInfo.EnvironmentVariables.Add("HTTPD_VERSION", HTTPD_VERSION);
            process.StartInfo.EnvironmentVariables.Add("SERVER_ROOT", serverRoot);
            process.StartInfo.EnvironmentVariables.Add("DEFAULT_DOCUMENT_ROOT", documentRoot);
            process.StartInfo.EnvironmentVariables.Add("CONF_ROOT", confRoot);
            process.StartInfo.EnvironmentVariables.Add("LOG_ROOT", logRoot);
            process.StartInfo.FileName = serverRoot + "\\bin\\httpd.exe";
            process.StartInfo.Arguments = "-f \"" + gearboxRoot + "\\opt\\gearbox\\httpd.conf\"";
            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.CreateNoWindow = true;
            process.EnableRaisingEvents = true;
            
            process.Start();

            process.WaitForExit();
        }

        private string GetGearBoxRoot()
        {
            return Directory.GetCurrentDirectory();
        }

        private string GetHttpdServerRoot()
        {
            return GetGearBoxRoot() + "\\opt\\" + HTTPD_VERSION;
        }

        private int GetHttpdPID()
        {
            string pidFilePath = GetHttpdServerRoot() + "\\logs\\httpd.pid";

            if (!File.Exists(pidFilePath))
            {
                return 0;
            }
            else
            {
                string[] lines = File.ReadAllLines(pidFilePath);

                if (lines.Length > 0)
                {
                    return Int32.Parse(lines[0]);
                }
            }

            return 0;
        }
    }
}
