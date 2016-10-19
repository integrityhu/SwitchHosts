using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.IO;
using System.Windows.Forms;

namespace SwitchHostsForm
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private FileSystemWatcher watcher;
        private List<ToolStripMenuItem> confMenuItems;
        public Label lblUpTime;
        public Label lblUsername;
        public Label lblHosts;
        public Label lblDNS;
        public int rightMargin;
        public string fontName;
        public string fontSize;


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

        private void OnContextMenuOpening(object sender, CancelEventArgs e)
        {
            this.contextMenuStrip1.Items.Clear();

            confMenuItems = new List<ToolStripMenuItem>();
            Configuration config = ConfigurationManager.OpenExeConfiguration(Application.ExecutablePath);
            string[] files;
            var hostsFiles = config.AppSettings.Settings["hostsFiles"];
            if ((hostsFiles == null) || string.IsNullOrEmpty(hostsFiles.Value))
            {
                files = new[] { "hosts.origin" };
            }
            else
            {
                String configs = config.AppSettings.Settings["hostsFiles"].Value;
                files = configs.Split(new[] { ',' });
            }
            int idx = 0;
            foreach (String fileName in files)
            {
                ToolStripMenuItem item = new ToolStripMenuItem();
                item.Size = new System.Drawing.Size(116, 22);
                item.Text = fileName;
                item.Name = "switchToolStripMenuItem_" + idx;
                item.Click += this.switchButton_Click;
                if ((config.AppSettings.Settings["currentHostsFile"] == null) && (fileName=="hosts.origin"))
                {
                    item.Checked = true;
                }
                else if (config.AppSettings.Settings["currentHostsFile"] != null && config.AppSettings.Settings["currentHostsFile"].Value.Equals(fileName))
                {
                    item.Checked = true;
                }
                confMenuItems.Add(item);
                idx++;
            }

            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();

            this.contextMenuStrip1.Items.AddRange(confMenuItems.ToArray());
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator2,
            this.setupToolStripMenuItem,
            this.toolStripSeparator1,
            this.exitToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(117, 54);

            // 
            // setupToolStripMenuItem
            // 
            this.setupToolStripMenuItem.Name = "setupToolStripMenuItem";
            this.setupToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.setupToolStripMenuItem.Text = "Setup";
            this.setupToolStripMenuItem.Click += this.ConfigButton_Click;
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(113, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += this.exitButton_Click;

        }

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.notificationIcon = new System.Windows.Forms.NotifyIcon(this.components);

            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);

            this.contextMenuStrip1.Opening += new CancelEventHandler(OnContextMenuOpening);

            this.timerGUI = new System.Windows.Forms.Timer(this.components);

            this.SuspendLayout();

            // 
            // notificationIcon
            // 
            this.notificationIcon.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.notificationIcon.ContextMenuStrip = this.contextMenuStrip1;

            this.notificationIcon.Icon = Properties.Resources.imageres_81;
            this.notificationIcon.Text = "SwitchHosts";
            this.notificationIcon.Visible = true;

            // 
            // timerGUI
            // 
            this.timerGUI.Enabled = true;
            this.timerGUI.Interval = 1000;
            this.timerGUI.Tick += new System.EventHandler(this.TimerGuiTick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(700, 479);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = Properties.Resources.imageres_81;
            this.Name = "Switch Hosts";
            this.ShowIcon = false;
            this.Hide();
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SwitchHosts";
            this.TransparencyKey = System.Drawing.Color.Black;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

            watcher = new FileSystemWatcher();
            watcher.Path = Environment.GetEnvironmentVariable("SystemRoot") + "/system32/drivers/etc/";
            watcher.NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.FileName;
            watcher.Filter = "hosts";
            watcher.Changed += this.OnChanged;
            watcher.EnableRaisingEvents = true;
        }

        public void OnChanged(object source, FileSystemEventArgs e)
        {
            UpdateHostsCallback d = new UpdateHostsCallback(this.reloadHosts);
            this.Invoke(d);
        }

        public void exitButton_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        public void ConfigButton_Click(object sender, EventArgs e)
        {
            SetupForm frmSetup = new SetupForm();
            frmSetup.fontDialog1.Font = this.lblHosts.Font;
            frmSetup.edFont.Text = this.lblHosts.Font.Name + "(" + this.lblHosts.Font.SizeInPoints + ")";
            if (this.lblHosts.ForeColor.IsNamedColor)
            {
                frmSetup.edColor.Text = this.lblHosts.ForeColor.Name;
            }
            else
            {
                frmSetup.edColor.Text = System.Drawing.ColorTranslator.ToHtml(this.lblHosts.ForeColor);
            }
            frmSetup.colorDialog1.Color = this.lblHosts.ForeColor;
            frmSetup.numRightMargin.Value = this.rightMargin;
            frmSetup.BringToFront();
            DialogResult res = frmSetup.ShowDialog(null);
        }

        public void switchButton_Click(object sender, EventArgs e)
        {            
            switchConfig(((ToolStripMenuItem)sender).Text);
        }
        #endregion

        private System.Windows.Forms.NotifyIcon notificationIcon;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem setupToolStripMenuItem;
        private System.Windows.Forms.Timer timerGUI;
    }
}

