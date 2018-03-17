namespace GearBoxGUI
{
    partial class FormGearBox
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.buttonHttpdStart = new System.Windows.Forms.Button();
            this.buttonHttpdStop = new System.Windows.Forms.Button();
            this.backgroundWorkerHttpd = new System.ComponentModel.BackgroundWorker();
            this.SuspendLayout();
            // 
            // buttonHttpdStart
            // 
            this.buttonHttpdStart.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonHttpdStart.Location = new System.Drawing.Point(12, 12);
            this.buttonHttpdStart.Name = "buttonHttpdStart";
            this.buttonHttpdStart.Size = new System.Drawing.Size(100, 30);
            this.buttonHttpdStart.TabIndex = 0;
            this.buttonHttpdStart.Text = "Start";
            this.buttonHttpdStart.UseVisualStyleBackColor = true;
            this.buttonHttpdStart.Click += new System.EventHandler(this.buttonHttpdStart_Click);
            // 
            // buttonHttpdStop
            // 
            this.buttonHttpdStop.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonHttpdStop.Location = new System.Drawing.Point(118, 12);
            this.buttonHttpdStop.Name = "buttonHttpdStop";
            this.buttonHttpdStop.Size = new System.Drawing.Size(100, 30);
            this.buttonHttpdStop.TabIndex = 1;
            this.buttonHttpdStop.Text = "Stop";
            this.buttonHttpdStop.UseVisualStyleBackColor = true;
            this.buttonHttpdStop.Click += new System.EventHandler(this.buttonHttpdStop_Click);
            // 
            // backgroundWorkerHttpd
            // 
            this.backgroundWorkerHttpd.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorkerHttpd_DoWork);
            this.backgroundWorkerHttpd.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorkerHttpd_RunWorkerCompleted);
            // 
            // FormGearBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(230, 55);
            this.Controls.Add(this.buttonHttpdStop);
            this.Controls.Add(this.buttonHttpdStart);
            this.MaximizeBox = false;
            this.Name = "FormGearBox";
            this.Text = "GearBox";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonHttpdStart;
        private System.Windows.Forms.Button buttonHttpdStop;
        private System.ComponentModel.BackgroundWorker backgroundWorkerHttpd;
    }
}

