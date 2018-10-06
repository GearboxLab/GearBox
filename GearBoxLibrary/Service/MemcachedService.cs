using System.Diagnostics;
using System.IO;
using System.Threading;

namespace GearBox.Service
{
    public class MemcachedService : AbstractService
    {
        private string _gearboxRoot;
        private Config.Memcached _config;

        private string _binDirectory;
        private string _binFilePath;

        public MemcachedService(string gearboxRoot, Config.Memcached config)
        {
            _gearboxRoot = gearboxRoot;
            _config = config;

            _binDirectory = _gearboxRoot + "\\opt\\memcached\\" + _config.FolderName;
            _binFilePath = _binDirectory + "\\memcached.exe";

            PidFilePath = _binDirectory + "\\memcached.pid";
        }

        public override void Start()
        {
            Spawn(new ThreadStart(DoStart));
        }

        public override void Stop()
        {
            Spawn(new ThreadStart(DoStop));
        }

        private void DoStart()
        {
            Process process = new Process();

            process.StartInfo.FileName = _binFilePath;
            process.StartInfo.Arguments = 
                  "-l " + _config.Ip
                + " -p " + _config.Port.ToString()
                + " -m " + _config.Size.ToString()
            ;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.ErrorDialog = false;
            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;

            process.Start();

            WritePid(process.Id);

            process.WaitForExit();
        }

        private void DoStop()
        {
            if (!File.Exists(PidFilePath))
            {
                return;
            }

            try
            {
                int pid = GetStartedPid(PidFilePath);

                if (pid > 0)
                {
                    Process process = Process.GetProcessById(pid);

                    if (!process.WaitForExit(1000))
                    {
                        process.Kill();

                        Thread.Sleep(1000);

                        File.Delete(PidFilePath);
                    }
                }
            }
            catch
            {
            }
        }

        private void WritePid(int pid)
        {
            File.WriteAllText(PidFilePath, pid.ToString());
        }
    }
}
