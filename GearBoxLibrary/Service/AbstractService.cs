using System;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace GearBox.Service
{
    public abstract class AbstractService : IService
    {
        protected string PidFilePath { get; set; }

        abstract public void Start();
        abstract public void Stop();
        
        public static void Spawn(ThreadStart start)
        {
            Thread thread = new Thread(start)
            {
                IsBackground = true
            };

            thread.Start();
        }

        public bool IsRunning()
        {
            if (!File.Exists(PidFilePath))
            {
                return false;
            }

            return ProcessExists(GetStartedPid(PidFilePath));
        }

        public void Restart()
        {
            Stop();

            Spawn(new ThreadStart(DoStartAfterStop));
        }

        protected void DoStartAfterStop()
        {
            for (int i = 0; i < 60; ++i)
            {
                if (IsRunning())
                {
                    Thread.Sleep(1000);
                }
                else
                {
                    break;
                }
            }

            if (!IsRunning())
            {
                Start();
            }
        }

        protected bool ProcessExists(int pid)
        {
            if (pid <= 0)
            {
                return false;
            }

            try
            {
                Process process = Process.GetProcessById(pid);

                return true;
            }
            catch
            {
            }

            return false;
        }

        protected int GetStartedPid(string pidFilePath)
        {
            string pidText = "";

            for (int i = 0; i < 10; ++i)
            {
                if (File.Exists(pidFilePath))
                {
                    try
                    {
                        pidText = File.ReadAllText(pidFilePath).Trim();

                        break;
                    }
                    catch
                    {
                    }
                }

                Thread.Sleep(1000);
            }

            if (pidText.Length > 0)
            {
                Int32.TryParse(pidText, out int pid);

                return pid;
            }

            return 0;
        }
    }
}
