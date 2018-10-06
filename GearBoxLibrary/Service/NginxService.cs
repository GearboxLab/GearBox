using System.Diagnostics;
using System.Threading;

namespace GearBox.Service
{
    public class NginxService : AbstractService
    {
        private string _binDirectory;
        private string _binFilePath;

        public NginxService(string binDirectory)
        {
            _binDirectory = binDirectory;
            _binFilePath = _binDirectory + "\\nginx.exe";

            PidFilePath = _binDirectory + "\\logs\\nginx.pid";
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

            process.StartInfo.FileName = "cmd.exe";
            process.StartInfo.Arguments = "/Q /C start /MIN /B \"nginx\" \"" + _binFilePath + "\" -p \"" + _binDirectory + "\"";
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.ErrorDialog = false;
            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;

            process.Start();

            process.WaitForExit();
        }

        private void DoStop()
        {
            Process process = new Process();

            process.StartInfo.FileName = _binFilePath;
            process.StartInfo.Arguments = "-s quit -p \"" + _binDirectory + "\"";
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.ErrorDialog = false;
            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;

            process.Start();

            process.WaitForExit();
        }
    }
}
