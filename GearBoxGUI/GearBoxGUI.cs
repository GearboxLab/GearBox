using System;
using System.Windows.Forms;

using System.Diagnostics;
using System.IO;
using System.Threading;

namespace GearBox
{
    public partial class GearBoxGUI : Form
    {
        private static bool _isFormOpened = false;

        private Config.Config _config;
        private Service.ApacheService _apacheService;
        private Service.MemcachedService _memcachedService;

        public const string SERVICE_APACHE = "Apache";
        public const string SERVICE_NGINX = "Nginx";
        public const string SERVICE_MEMCACHED = "Memcached";

        public GearBoxGUI(string[] args) : this()
        {
            if (1 == args.Length && "start" == args[0])
            {
                StartAll();

                for (int i = 0; i < 10 && _apacheService.IsRunning(); ++i)
                {
                    Thread.Sleep(1000);
                }

                Close();
            }
        }

        public GearBoxGUI()
        {
            InitializeComponent();

            string gearboxRoot = GetGearBoxRoot();
            string configFilePath = gearboxRoot + "\\config.json";
            string optDirectory = gearboxRoot + "\\opt";
            string etcDirectory = gearboxRoot + "\\etc";

            _config = Config.Config.Load(configFilePath);
            _apacheService = new Service.ApacheService(gearboxRoot, _config.Apache);
            _memcachedService = new Service.MemcachedService(gearboxRoot, _config.Memcached);
        }

        private void GearBoxGUI_Load(object sender, EventArgs e)
        {
            if (!_isFormOpened)
            {
                _isFormOpened = true;
            }
            else
            {
                Dispose();
            }

            checkBoxStartup.Checked = File.Exists(StartupLinkPath());

            timer1.Start();
        }

        private void GearBoxGUI_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_isFormOpened)
            {
                _isFormOpened = false;
            }

            e.Cancel = true;
            Hide();
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            StartAll();
        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            StopAll();
        }

        private void checkBoxStartup_CheckedChanged(object sender, EventArgs e)
        {
            string linkPath = StartupLinkPath();

            if (!checkBoxStartup.Checked)
            {
                if (File.Exists(linkPath))
                {
                    File.Delete(linkPath);
                }
                
                return;
            }

            string targetPath = Process.GetCurrentProcess().MainModule.FileName;
            IWshRuntimeLibrary.WshShellClass wsh = new IWshRuntimeLibrary.WshShellClass();
            IWshRuntimeLibrary.IWshShortcut shortcut = wsh.CreateShortcut(linkPath) as IWshRuntimeLibrary.IWshShortcut;

            shortcut.TargetPath = targetPath;
            shortcut.Arguments = "start";
            // 1 for default window, 3 for maximize, 7 for minimize
            shortcut.WindowStyle = 1;
            shortcut.Description = "Start GearBox server when Windows starts";
            shortcut.WorkingDirectory = Directory.GetParent(targetPath).ToString();

            shortcut.Save();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            buttonStart.Enabled = !_apacheService.IsRunning();
            buttonStop.Enabled = !buttonStart.Enabled;

            startToolStripMenuItem.Enabled = buttonStart.Enabled;
            stopToolStripMenuItem.Enabled = buttonStop.Enabled;
        }

        private void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            Show();
        }

        private void startToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StartAll();
        }

        private void stopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StopAll();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void StartAll()
        {
            if (!_apacheService.IsRunning())
            {
                _apacheService.Start();
            }

            if (!_memcachedService.IsRunning())
            {
                _memcachedService.Start();
            }
        }

        private void StopAll()
        {
            if (_apacheService.IsRunning())
            {
                _apacheService.Stop();
            }

            if (_memcachedService.IsRunning())
            {
                _memcachedService.Stop();
            }
        }

        private string StartupLinkPath()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.Startup) + "\\GearBox.lnk";
        }

        private string GetGearBoxRoot()
        {
#if DEBUG
            string root = Directory.GetCurrentDirectory();
#else
            string root = Directory.GetParent(Process.GetCurrentProcess().MainModule.FileName).ToString();
#endif
            char[] charsToTrim = { '\\', ' ' };

            return root.TrimEnd(charsToTrim);
        }
    }
}
