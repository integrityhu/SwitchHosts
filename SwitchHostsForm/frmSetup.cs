using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Management;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace SwitchHostsForm
{

    public partial class SetupForm : Form
    {
        string currentHostsFile;
        List<DNSInfo> dnsXML;

        private static List<DNSInfo> getDNSList(string fileName)
        {
            FileStream fstream = new FileStream(fileName, FileMode.Open);
            XmlSerializer ser = new XmlSerializer(typeof(List<DNSInfo>));
            List<DNSInfo> result = (List<DNSInfo>)ser.Deserialize(fstream);
            fstream.Close();
            return result;
        }

        private static void save(List<DNSInfo> dnsInfoList, string fileName)
        {
            FileStream fstream = new FileStream(fileName, FileMode.Create);
            XmlSerializer ser = new XmlSerializer(typeof(List<DNSInfo>));
            ser.Serialize(fstream, dnsInfoList);
            fstream.Close();
        }

        public SetupForm()
        {
            InitializeComponent();
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

            if (!System.IO.File.Exists("./versions/hosts.origin"))
            {
                String originHostsFile = Environment.GetEnvironmentVariable("SystemRoot") + "/system32/drivers/etc/hosts";
                System.IO.File.Copy(originHostsFile, "./versions/hosts.origin");
            }

            if (!System.IO.File.Exists("./dnsaddresses.xml"))
            {
                dnsXML = new List<DNSInfo>();
                DNSInfo dnsInfo = new DNSInfo();
                dnsInfo.Name = "GoogleDNS";
                dnsInfo.DNS1 = "8.8.4.4"; //dns1
                dnsInfo.DNS2 = "8.8.8.8"; //dns2
                dnsXML.Add(dnsInfo);
                dnsInfo = new DNSInfo();
                dnsInfo.Name = "OpenDNS";
                dnsInfo.DNS1 = "208.67.222.222";
                dnsInfo.DNS2 = "208.67.220.220";
                dnsXML.Add(dnsInfo);
                save(dnsXML, "./dnsaddresses.xml");
            }
            else
            {
                dnsXML = getDNSList("./dnsaddresses.xml");
            }

            foreach (DNSInfo item in dnsXML)
            {
                cbxDNSNames.Items.Add(item.Name);
            }


            cbxVersions.Items.AddRange(files);
            if (config.AppSettings.Settings["currentHostsFile"] != null)
            {
                cbxVersions.SelectedItem = config.AppSettings.Settings["currentHostsFile"].Value;
                this.rcbHostsConfig.Text = SwitchHosts.Program.getFileContent("./versions/" + config.AppSettings.Settings["currentHostsFile"].Value);
            }
            else
            {
                cbxVersions.SelectedItem = "hosts.origin";
                this.rcbHostsConfig.Text = SwitchHosts.Program.getFileContent("./versions/hosts.origin");
            }
            setCurrentHostsFileName();

            networkManager = new NetworkManagement();
            ManagementObjectCollection objMOC = networkManager.getNICs();
            foreach (ManagementObject objMO in objMOC)
            {
                if ((bool)objMO["IPEnabled"])
                {
                    cbxNIC.Items.Add(objMO["Caption"].ToString());
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            Font font = fontDialog1.Font;
            Color color = colorDialog1.Color;
            Configuration config = ConfigurationManager.OpenExeConfiguration(Application.ExecutablePath);
            config.AppSettings.Settings["fontName"].Value = font.Name;
            config.AppSettings.Settings["fontSize"].Value = font.SizeInPoints.ToString();
            if (color.IsNamedColor)
            {
                config.AppSettings.Settings["foreColor"].Value = color.Name;
            }
            else
            {
                config.AppSettings.Settings["foreColor"].Value = System.Drawing.ColorTranslator.ToHtml(color);
            }

            config.AppSettings.Settings["rightMargin"].Value = numRightMargin.Value.ToString();
            List<String> items = new List<String>();
            for (int i = 0; i < cbxVersions.Items.Count; i++)
            {
                items.Add(cbxVersions.Items[i].ToString());
            }
            String oHostsFile = currentHostsFile;
            setCurrentHostsFileName();

            if (config.AppSettings.Settings["hostsFiles"] == null)
            {
                config.AppSettings.Settings.Add("hostsFiles", string.Join(",", items));
            }
            else
            {
                config.AppSettings.Settings["hostsFiles"].Value = string.Join(",", items);
            }

            config.Save(ConfigurationSaveMode.Modified);
            SwitchHosts.Program.saveFileContent("./versions/" + currentHostsFile, this.rcbHostsConfig.Text);
            if (oHostsFile.Equals(currentHostsFile))
            {
                MainForm mainForm = (MainForm)Application.OpenForms[0];
                mainForm.switchConfig(currentHostsFile);
            }
            this.Close();
        }

        private void setCurrentHostsFileName()
        {
            if (cbxVersions.SelectedIndex > -1)
            {
                currentHostsFile = cbxVersions.Items[cbxVersions.SelectedIndex].ToString();
            }
            else if (!String.IsNullOrEmpty(cbxVersions.Text))
            {
                currentHostsFile = cbxVersions.Text;
            }
            else
            {
                currentHostsFile = "hosts.origin";
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            MainForm mainForm = (MainForm)Application.OpenForms[0];
            mainForm.reloadFromConfig();
            this.Close();
        }

        private void btnChange_Click(object sender, EventArgs e)
        {
            DialogResult res = fontDialog1.ShowDialog(this);
            if (res == DialogResult.OK)
            {
                edFont.Text = fontDialog1.Font.FontFamily.Name + "(" + fontDialog1.Font.SizeInPoints + ")";
                MainForm mainForm = (MainForm)Application.OpenForms[0];
                mainForm.lblHosts.Font = fontDialog1.Font;
                mainForm.lblUsername.Font = fontDialog1.Font;
                mainForm.lblUpTime.Font = fontDialog1.Font;
                mainForm.lblDNS.Font = fontDialog1.Font;
                reformatLabels();
            }
        }

        private void btnChangeColor_Click(object sender, EventArgs e)
        {
            DialogResult res = colorDialog1.ShowDialog(this);
            if (res == DialogResult.OK)
            {
                if (colorDialog1.Color.IsNamedColor)
                {
                    edColor.Text = colorDialog1.Color.Name;
                }
                else
                {
                    edColor.Text = System.Drawing.ColorTranslator.ToHtml(colorDialog1.Color);
                }
                MainForm mainForm = (MainForm)Application.OpenForms[0];
                mainForm.lblHosts.ForeColor = colorDialog1.Color;
                mainForm.lblUpTime.ForeColor = colorDialog1.Color;
                mainForm.lblUsername.ForeColor = colorDialog1.Color;
                mainForm.lblDNS.ForeColor = colorDialog1.Color;
            }
        }

        private void numRightMargin_ValueChanged(object sender, EventArgs e)
        {
            reformatLabels();
        }

        private void reformatLabels()
        {
            MainForm mainForm = (MainForm)Application.OpenForms[0];
            mainForm.rightMargin = (int)numRightMargin.Value;
            mainForm.reloadDNS();
            mainForm.reloadHosts();
        }

        private void cbxVersions_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((cbxVersions.SelectedIndex > -1) && (cbxVersions.Items[cbxVersions.SelectedIndex] != null))
            {
                String configFile = cbxVersions.Items[cbxVersions.SelectedIndex].ToString();
                rcbHostsConfig.Text = SwitchHosts.Program.getFileContent("./versions/" + configFile);
                rcbHostsConfig.ReadOnly = configFile.Equals("hosts.origin");
                btnDelete.Enabled = !rcbHostsConfig.ReadOnly;
            }
            else
            {
                rcbHostsConfig.ResetText();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("Delete this version?", "Are you sure", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (res.Equals(DialogResult.Yes))
            {
                String configFile = cbxVersions.Items[cbxVersions.SelectedIndex].ToString();
                if (System.IO.File.Exists("./versions/" + configFile))
                {
                    System.IO.File.Delete("./versions/" + configFile);
                }
                cbxVersions.Items.RemoveAt(cbxVersions.SelectedIndex);
                cbxVersions.SelectedIndex = -1;
                cbxVersions_SelectedIndexChanged(cbxVersions, null);

                Configuration config = ConfigurationManager.OpenExeConfiguration(Application.ExecutablePath);
                String hostsFiles = string.Join(",", cbxVersions.Items.Cast<String>());
                if (config.AppSettings.Settings["hostsFiles"] == null)
                {

                    config.AppSettings.Settings.Add("hostsFiles", hostsFiles);
                }
                else
                {
                    config.AppSettings.Settings["hostsFiles"].Value = string.Join(",", hostsFiles);
                }
                config.Save(ConfigurationSaveMode.Modified);
                tssLabel.Text = configFile + " deleted.";
            }
        }

        private void btnSaveNSwitch_Click(object sender, EventArgs e)
        {
            btnSave_Click(sender, e);
            MainForm mainForm = (MainForm)Application.OpenForms[0];
            setCurrentHostsFileName();
            mainForm.switchConfig(currentHostsFile);
            this.Close();
        }

        private void btNew_Click(object sender, EventArgs e)
        {
            tssLabel.Text = "";
            VersionControl newVersion = new VersionControl();
            newVersion.cbBaseVersions.Items.Add("");
            newVersion.cbBaseVersions.Items.AddRange(cbxVersions.Items.Cast<string>().ToArray());
            DialogResult res = newVersion.ShowDialog(this);
            if (res.Equals(DialogResult.OK))
            {
                if (!String.IsNullOrWhiteSpace(newVersion.txtNewVersionName.Text))
                {
                    if (cbxVersions.Items.Contains(newVersion.txtNewVersionName.Text))
                    {
                        MessageBox.Show(newVersion.txtNewVersionName.Text + " version exists!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        cbxVersions.Items.Add(newVersion.txtNewVersionName.Text);
                        cbxVersions.SelectedIndex = cbxVersions.Items.IndexOf(newVersion.txtNewVersionName.Text);
                        cbxVersions_SelectedIndexChanged(cbxVersions, null);
                        if (!String.IsNullOrWhiteSpace(newVersion.cbBaseVersions.Text))
                        {
                            rcbHostsConfig.Text = SwitchHosts.Program.getFileContent("./versions/" + newVersion.cbBaseVersions.Text);
                            tssLabel.Text = "Modify version content";
                        }
                        else
                        {
                            tssLabel.Text = "Enter new content";
                        }

                    }
                }
                else
                {
                    MessageBox.Show("Version name is empty!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (cbxNIC.SelectedIndex > -1)
            {
                networkManager.setDNS(cbxNIC.SelectedItem.ToString(), edDNS1.Text + (!String.IsNullOrWhiteSpace(edDNS2.Text) ? "," + edDNS2.Text : ""));
                reformatLabels();
            }
            else
            {
                MessageBox.Show("Choose an NIC", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cbxDNSNames_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxDNSNames.SelectedIndex > -1)
            {
                string iDNSName = cbxDNSNames.SelectedItem.ToString();
                try
                {

                    var dnsInfo = dnsXML.Where(x => x.Name.Equals(iDNSName)).First();
                    if (dnsInfo != null)
                    {
                        edDNS1.Text = dnsInfo.DNS1;
                        edDNS2.Text = dnsInfo.DNS2;
                    }
                }
                catch (InvalidOperationException ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                edDNS1.Text = "";
                edDNS2.Text = "";
            }
        }

        private void btnSaveDNS_Click(object sender, EventArgs e)
        {
            if (cbxDNSNames.SelectedIndex > 1)
            {
                string iDNSName = cbxDNSNames.Text;
                dnsXML.Where(x => x.Name.Equals(iDNSName)).First(x =>
                {
                    x.DNS1 = edDNS1.Text;
                    x.DNS2 = edDNS2.Text;
                    return true;
                });
                save(dnsXML, "./dnsaddresses.xml");
            }
            else if (cbxDNSNames.SelectedIndex == -1)
            {
                DNSInfo dnsInfo = new DNSInfo();
                dnsInfo.Name = cbxDNSNames.Text;
                dnsInfo.DNS1 = edDNS1.Text;
                dnsInfo.DNS2 = edDNS2.Text;
                dnsXML.Add(dnsInfo);
                save(dnsXML, "./dnsaddresses.xml");

                cbxDNSNames.Items.Clear();
                foreach (DNSInfo item in dnsXML)
                {
                    cbxDNSNames.Items.Add(item.Name);
                }
            }
        }

        private void btnDNSDelete_Click(object sender, EventArgs e)
        {
            if (cbxDNSNames.SelectedIndex > 1)
            {
                string iDNSName = cbxDNSNames.Text;
                dnsXML.RemoveAll(x => x.Name.Equals(iDNSName));
                cbxDNSNames.Items.Remove(cbxDNSNames.SelectedItem);
                edDNS1.Text = "";
                edDNS2.Text = "";
                save(dnsXML, "./dnsaddresses.xml");
            }
        }
    }
}
