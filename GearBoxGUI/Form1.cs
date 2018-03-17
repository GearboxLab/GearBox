using System;
using System.ComponentModel;
using System.Windows.Forms;
using GearBox;
using System.IO;

namespace GearBoxGUI
{
    public partial class FormGearBox : Form
    {
        private Httpd httpd;

        public FormGearBox()
        {
            InitializeComponent();

            httpd = new Httpd();

            updateUI(httpd.IsStarted());
        }

        private void updateUI(bool httpdStarted)
        {
            buttonHttpdStart.Enabled = !httpdStarted;
            buttonHttpdStop.Enabled = httpdStarted;
        }

        private void buttonHttpdStart_Click(object sender, EventArgs e)
        {
            if (!httpd.IsStarted())
            {
                backgroundWorkerHttpd.RunWorkerAsync();
            }
        }

        private void buttonHttpdStop_Click(object sender, EventArgs e)
        {
            httpd.Stop();

            updateUI(false);
        }

        private void backgroundWorkerHttpd_DoWork(object sender, DoWorkEventArgs e)
        {
            httpd.Start();
        }

        private void backgroundWorkerHttpd_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            updateUI(true);
        }
    }
}
