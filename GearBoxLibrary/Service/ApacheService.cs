using System.Diagnostics;
using System.IO;
using System.Threading;

namespace GearBox.Service
{
    public class ApacheService : AbstractService
    {
        private string _gearboxRoot;
        private Config.Apache _config;
        private string _binDirectory;
        private string _binFilePath;
        private string _etcDirectory;
        private string _logDirectory;
        private string _configFilePath;

        public ApacheService(string gearboxRoot, Config.Apache config)
        {
            _gearboxRoot = gearboxRoot;
            _config = config;

            _binDirectory = _gearboxRoot + "\\opt\\apache\\" + _config.FolderName;
            _binFilePath = _binDirectory + "\\bin\\httpd.exe";
            _etcDirectory = _gearboxRoot + "\\etc\\apache\\" + _config.FolderName;
            _logDirectory = _gearboxRoot + "\\var\\log\\apache\\" + _config.FolderName;
            _configFilePath = _gearboxRoot + "\\opt\\gearbox\\httpd.conf";

            PidFilePath = _binDirectory + "\\logs\\httpd.pid";
        }

        public override void Start()
        {
            ModifyDefaultConf();

            Spawn(new ThreadStart(DoStart));
        }

        public override void Stop()
        {
            Spawn(new ThreadStart(DoStop));
        }

        private void DoStart()
        {
            Process process = new Process();

            process.StartInfo.EnvironmentVariables.Add("GEARBOX_ROOT", _gearboxRoot.Replace("\\", "/"));
            process.StartInfo.EnvironmentVariables.Add("APACHE_ROOT", _binDirectory.Replace("\\", "/"));
            process.StartInfo.EnvironmentVariables.Add("APACHE_CONF_ROOT", _etcDirectory.Replace("\\", "/"));
            process.StartInfo.EnvironmentVariables.Add("APACHE_LOG_ROOT", _logDirectory.Replace("\\", "/"));

            process.StartInfo.FileName = _binFilePath;
            process.StartInfo.Arguments = "-f \"" + _configFilePath + "\"";
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.ErrorDialog = false;
            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;

            process.Start();

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

        private void ModifyDefaultConf()
        {
            string defaultConfFilePath = _binDirectory + "\\conf\\httpd.conf";
            string contents = File.ReadAllText(defaultConfFilePath);
            string modifiedContents = contents
                .Replace("Define SRVROOT \"c:/Apache24\"", "Define SRVROOT \"${APACHE_ROOT}\"")
                .Replace("${SRVROOT}/htdocs", "${WEB_ROOT}");

            File.WriteAllText(defaultConfFilePath, modifiedContents);
        }
    }
}
