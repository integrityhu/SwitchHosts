using System;
using System.Linq;
using System.Windows.Forms;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Diagnostics;
using System.IO;
using System.Configuration;
using System.Net;
using System.Net.Sockets;
using System.Net.NetworkInformation;

namespace SwitchHostsForm
{

    public partial class MainForm : Form
    {
        protected override CreateParams CreateParams
        { //http://stackoverflow.com/questions/357076/best-way-to-hide-a-window-from-the-alt-tab-program-switcher
            get
            {
                var Params = base.CreateParams;
                Params.ExStyle |= 0x80;
                return Params;
            }
        }

        //http://www.codeproject.com/Articles/11758/Conditional-Install-of-Desktop-and-Quick-Launch-Sh
        delegate void UpdateHostsCallback();
        delegate void SetTextCallback(string text, Control ctrl);

        BackgroundWorker bgw = new BackgroundWorker();

        #region BottomMost
        [DllImport("user32.dll")]
        static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

        static readonly IntPtr HWND_BOTTOM = new IntPtr(1);
        const UInt32 SWP_NOSIZE = 0x0001;
        const UInt32 SWP_NOMOVE = 0x0002;
        const UInt32 SWP_NOACTIVATE = 0x0010;
        #endregion



        public TimeSpan UpTime
        {
            get
            {
                using (var uptime = new PerformanceCounter("System", "System Up Time"))
                {
                    uptime.NextValue();       //Call this an extra time before reading its value
                    return TimeSpan.FromSeconds(uptime.NextValue());
                }
            }
        }


        public MainForm()
        {
            this.InitializeComponent();
            this.Visible = false;
            SetWindowPos(Handle, HWND_BOTTOM, 0, 0, 0, 0, SWP_NOMOVE | SWP_NOSIZE | SWP_NOACTIVATE); // Set Form1 as BottomMost

            AssignLabels();

            bgw.DoWork += new DoWorkEventHandler(BgwDoWork);
            bgw.RunWorkerAsync();

        }

        // What does the person want shown on the desktop?
        private void AssignLabels()
        {
            // Hard coded for demo
            lblUpTime = new Label();
            lblUpTime.Name = "Up Time";
            lblUpTime.AutoSize = true;
            this.Controls.Add(lblUpTime);

            lblUsername = new Label();
            lblUsername.Name = "Username";
            lblUsername.AutoSize = true;
            this.Controls.Add(lblUsername);

            lblDNS = new Label();
            lblDNS.Name = "DNSServers";
            lblDNS.AutoSize = true;
            this.Controls.Add(lblDNS);

            lblHosts = new Label();
            lblHosts.Name = "Hosts";
            lblHosts.AutoSize = true;
            this.Controls.Add(lblHosts);

            reloadFromConfig();
        }

        public void reloadFromConfig()
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(Application.ExecutablePath);
            this.fontName = config.AppSettings.Settings["fontName"].Value;
            this.fontSize = config.AppSettings.Settings["fontSize"].Value;
            String fColor = config.AppSettings.Settings["foreColor"].Value;
            string rightMargin = config.AppSettings.Settings["rightMargin"].Value;
            this.rightMargin = Int16.Parse(rightMargin);

            decimal iFontSize = Decimal.Parse(this.fontSize);
            this.lblHosts.Font = new Font(this.fontName, (int)iFontSize);
            Color color;
            try
            {
                color = System.Drawing.ColorTranslator.FromHtml(fColor);
            }
            catch (Exception)
            {
                color = Color.FromName(fColor);
            }

            this.lblHosts.ForeColor = color;
            this.lblUsername.ForeColor = color;
            this.lblUpTime.ForeColor = color;
            this.lblDNS.ForeColor = color;

            this.lblUpTime.Font = this.lblHosts.Font;
            this.lblUsername.Font = this.lblHosts.Font;
            this.lblDNS.Font = this.lblHosts.Font;

            lblUpTime.Top = this.Top + 10;
            lblUsername.Top = lblUpTime.Top + lblUpTime.Height + 25;

            reloadDNS();
            reloadHosts();
        }

        // How long has the computer been running?
        private string GetUpTime()
        {
            string days = UpTime.Days.ToString("N0"); // formatted like this 100,000 with no decimal places
            string hours = UpTime.Hours.ToString("00"); // Padded with 2 zeros so 2 hours will look like this 02
            string minutes = UpTime.Minutes.ToString("00");
            string secs = UpTime.Seconds.ToString("00");

            string res = "";
            if (UpTime.Days == 0) res = hours + ":" + minutes + ":" + secs; // If there are no days show this
            if (UpTime.Days == 1) res = days + " Day " + hours + ":" + minutes + ":" + secs; // If there is only 1 day show this
            if (String.IsNullOrEmpty(res)) res = days + " Days " + hours + ":" + minutes + ":" + secs; // In all other cases show this
            return res;
        }

        // Run the backgroundworker that updates the background information display
        void BgwDoWork(object sender, DoWorkEventArgs e)
        {



        }

        // Only required for cross-thread operations
        private void SetText(string text, Control ctrl)
        {
            // InvokeRequired required compares the thread ID of the 
            // calling thread to the thread ID of the creating thread. 
            // If these threads are different, it returns true. 
            if (ctrl.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetText);
                this.Invoke(d, new object[] { text, ctrl });
            }
            else
            {
                ctrl.Text = text;
            }
        }

        private void TimerGuiTick(object sender, EventArgs e)
        {
            SetWindowPos(Handle, HWND_BOTTOM, 0, 0, 0, 0, SWP_NOMOVE | SWP_NOSIZE | SWP_NOACTIVATE); // Set Form1 as BottomMost
            lblUpTime.Left = this.Right - rightMargin;
            lblUpTime.Text = "Up Time: " + GetUpTime();

            lblUsername.Left = this.Right - rightMargin;
            lblUsername.Text = "Username: " + Environment.UserName;

            Application.DoEvents();
        }

        //https://msdn.microsoft.com/en-us/library/system.net.networkinformation.networkinterface.name(v=vs.110).aspx
        public void reloadDNS()
        {
            String dnsServersStr = "DNS Servers :\r\n";

            NetworkInterface[] adapters = NetworkInterface.GetAllNetworkInterfaces();
            foreach (NetworkInterface adapter in adapters)
            {
                if (adapter.Supports(NetworkInterfaceComponent.IPv4))
                {
                    IPInterfaceProperties adapterProperties = adapter.GetIPProperties();
                    IPAddressCollection dnsServers = adapterProperties.DnsAddresses;
                    dnsServersStr += String.Format("{0} :\r\n", adapter.Name);
                    if (dnsServers.Count > 0)
                    {
                        foreach (IPAddress dns in dnsServers)
                        {
                            if (dns.AddressFamily.ToString().Equals(ProtocolFamily.InterNetwork.ToString()))
                                dnsServersStr += String.Format("{0}\r\n", dns.ToString());
                        }
                    }
                }
            }
            lblDNS.Left = this.Right - rightMargin;
            lblDNS.Text = dnsServersStr;
            lblDNS.Top = lblUsername.Top + lblUsername.Height + 25;
        }

        public void reloadHosts()
        {
            String hostsContent = SwitchHosts.Program.getSystemHostsContent();
            StringReader contentReader = new StringReader(hostsContent);
            String line = contentReader.ReadLine();
            StringWriter contentWriter = new StringWriter();
            while (line != null)
            {
                int idx = line.IndexOf("#");
                if (idx > -1)
                {
                    line = line.Substring(0, idx);
                }
                if (!String.IsNullOrEmpty(line.Trim()))
                {
                    contentWriter.WriteLine(line.Trim());
                }
                line = contentReader.ReadLine();
            }
            lblHosts.Top = lblDNS.Top + lblDNS.Height + 40;
            lblHosts.Left = this.Right - rightMargin;
            lblHosts.Text = contentWriter.ToString();
        }

        internal void switchConfig(string text)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(Application.ExecutablePath);
            string hostsFile = Environment.GetEnvironmentVariable("SystemRoot") + "/system32/drivers/etc/hosts";
            confMenuItems.ForEach(delegate (ToolStripMenuItem i) { i.Checked = false; });
            if (System.IO.File.Exists("./versions/" + text))
            {
                System.IO.File.Copy("./versions/" + text, hostsFile, true);
            }
            if (config.AppSettings.Settings["currentHostsFile"] != null)
            {
                config.AppSettings.Settings["currentHostsFile"].Value = text;
            }
            else
            {
                config.AppSettings.Settings.Add("currentHostsFile", text);
            }
            config.Save(ConfigurationSaveMode.Modified);
            System.Diagnostics.EventLog appLog = new System.Diagnostics.EventLog();
            appLog.Source = "SwitchHost";
            appLog.WriteEntry("new hosts config:" + text);
        }
    }
}