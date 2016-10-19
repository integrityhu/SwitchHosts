namespace SwitchHostsForm
{
    partial class SetupForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        private NetworkManagement networkManager = null;

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
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageHosts = new System.Windows.Forms.TabPage();
            this.label4 = new System.Windows.Forms.Label();
            this.btNew = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnSaveNSwitch = new System.Windows.Forms.Button();
            this.cbxVersions = new System.Windows.Forms.ComboBox();
            this.rcbHostsConfig = new System.Windows.Forms.RichTextBox();
            this.tabPageFont = new System.Windows.Forms.TabPage();
            this.numRightMargin = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.btnChangeColor = new System.Windows.Forms.Button();
            this.edColor = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnChange = new System.Windows.Forms.Button();
            this.edFont = new System.Windows.Forms.TextBox();
            this.tabDNS = new System.Windows.Forms.TabPage();
            this.label8 = new System.Windows.Forms.Label();
            this.btnDNSDelete = new System.Windows.Forms.Button();
            this.btnSaveDNS = new System.Windows.Forms.Button();
            this.cbxDNSNames = new System.Windows.Forms.ComboBox();
            this.edDNS2 = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cbxNIC = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.edDNS1 = new System.Windows.Forms.TextBox();
            this.fontDialog1 = new System.Windows.Forms.FontDialog();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.tssLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.tabControl1.SuspendLayout();
            this.tabPageHosts.SuspendLayout();
            this.tabPageFont.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numRightMargin)).BeginInit();
            this.tabDNS.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Location = new System.Drawing.Point(304, 294);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(385, 294);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPageHosts);
            this.tabControl1.Controls.Add(this.tabPageFont);
            this.tabControl1.Controls.Add(this.tabDNS);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(462, 276);
            this.tabControl1.TabIndex = 3;
            // 
            // tabPageHosts
            // 
            this.tabPageHosts.BackColor = System.Drawing.SystemColors.Menu;
            this.tabPageHosts.Controls.Add(this.label4);
            this.tabPageHosts.Controls.Add(this.btNew);
            this.tabPageHosts.Controls.Add(this.btnDelete);
            this.tabPageHosts.Controls.Add(this.btnSaveNSwitch);
            this.tabPageHosts.Controls.Add(this.cbxVersions);
            this.tabPageHosts.Controls.Add(this.rcbHostsConfig);
            this.tabPageHosts.Location = new System.Drawing.Point(4, 22);
            this.tabPageHosts.Name = "tabPageHosts";
            this.tabPageHosts.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageHosts.Size = new System.Drawing.Size(454, 250);
            this.tabPageHosts.TabIndex = 0;
            this.tabPageHosts.Text = "Hosts";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 6);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(74, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Version name:";
            // 
            // btNew
            // 
            this.btNew.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btNew.Location = new System.Drawing.Point(189, 219);
            this.btNew.Name = "btNew";
            this.btNew.Size = new System.Drawing.Size(75, 23);
            this.btNew.TabIndex = 6;
            this.btNew.Text = "New";
            this.btNew.UseVisualStyleBackColor = true;
            this.btNew.Click += new System.EventHandler(this.btNew_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDelete.Location = new System.Drawing.Point(270, 219);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 23);
            this.btnDelete.TabIndex = 4;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnSaveNSwitch
            // 
            this.btnSaveNSwitch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSaveNSwitch.Location = new System.Drawing.Point(351, 219);
            this.btnSaveNSwitch.Name = "btnSaveNSwitch";
            this.btnSaveNSwitch.Size = new System.Drawing.Size(93, 23);
            this.btnSaveNSwitch.TabIndex = 5;
            this.btnSaveNSwitch.Text = "Save\'n switch";
            this.btnSaveNSwitch.UseVisualStyleBackColor = true;
            this.btnSaveNSwitch.Click += new System.EventHandler(this.btnSaveNSwitch_Click);
            // 
            // cbxVersions
            // 
            this.cbxVersions.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbxVersions.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxVersions.FormattingEnabled = true;
            this.cbxVersions.Location = new System.Drawing.Point(83, 3);
            this.cbxVersions.Name = "cbxVersions";
            this.cbxVersions.Size = new System.Drawing.Size(361, 21);
            this.cbxVersions.TabIndex = 2;
            this.cbxVersions.SelectedIndexChanged += new System.EventHandler(this.cbxVersions_SelectedIndexChanged);
            // 
            // rcbHostsConfig
            // 
            this.rcbHostsConfig.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rcbHostsConfig.Location = new System.Drawing.Point(3, 26);
            this.rcbHostsConfig.Name = "rcbHostsConfig";
            this.rcbHostsConfig.Size = new System.Drawing.Size(445, 188);
            this.rcbHostsConfig.TabIndex = 1;
            this.rcbHostsConfig.Text = "";
            // 
            // tabPageFont
            // 
            this.tabPageFont.Controls.Add(this.numRightMargin);
            this.tabPageFont.Controls.Add(this.label3);
            this.tabPageFont.Controls.Add(this.btnChangeColor);
            this.tabPageFont.Controls.Add(this.edColor);
            this.tabPageFont.Controls.Add(this.label2);
            this.tabPageFont.Controls.Add(this.label1);
            this.tabPageFont.Controls.Add(this.btnChange);
            this.tabPageFont.Controls.Add(this.edFont);
            this.tabPageFont.Location = new System.Drawing.Point(4, 22);
            this.tabPageFont.Name = "tabPageFont";
            this.tabPageFont.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageFont.Size = new System.Drawing.Size(454, 250);
            this.tabPageFont.TabIndex = 1;
            this.tabPageFont.Text = "Visual configuration";
            this.tabPageFont.UseVisualStyleBackColor = true;
            // 
            // numRightMargin
            // 
            this.numRightMargin.Location = new System.Drawing.Point(30, 132);
            this.numRightMargin.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numRightMargin.Name = "numRightMargin";
            this.numRightMargin.Size = new System.Drawing.Size(72, 20);
            this.numRightMargin.TabIndex = 8;
            this.numRightMargin.Value = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.numRightMargin.ValueChanged += new System.EventHandler(this.numRightMargin_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(27, 116);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(66, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Right margin";
            // 
            // btnChangeColor
            // 
            this.btnChangeColor.Location = new System.Drawing.Point(170, 77);
            this.btnChangeColor.Name = "btnChangeColor";
            this.btnChangeColor.Size = new System.Drawing.Size(75, 23);
            this.btnChangeColor.TabIndex = 5;
            this.btnChangeColor.Text = "Change";
            this.btnChangeColor.UseVisualStyleBackColor = true;
            this.btnChangeColor.Click += new System.EventHandler(this.btnChangeColor_Click);
            // 
            // edColor
            // 
            this.edColor.Location = new System.Drawing.Point(30, 80);
            this.edColor.Name = "edColor";
            this.edColor.Size = new System.Drawing.Size(134, 20);
            this.edColor.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(27, 64);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Font color";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(27, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Font and format";
            // 
            // btnChange
            // 
            this.btnChange.Location = new System.Drawing.Point(267, 37);
            this.btnChange.Name = "btnChange";
            this.btnChange.Size = new System.Drawing.Size(75, 23);
            this.btnChange.TabIndex = 1;
            this.btnChange.Text = "Change";
            this.btnChange.UseVisualStyleBackColor = true;
            this.btnChange.Click += new System.EventHandler(this.btnChange_Click);
            // 
            // edFont
            // 
            this.edFont.Location = new System.Drawing.Point(30, 37);
            this.edFont.Name = "edFont";
            this.edFont.Size = new System.Drawing.Size(231, 20);
            this.edFont.TabIndex = 0;
            // 
            // tabDNS
            // 
            this.tabDNS.Controls.Add(this.label8);
            this.tabDNS.Controls.Add(this.btnDNSDelete);
            this.tabDNS.Controls.Add(this.btnSaveDNS);
            this.tabDNS.Controls.Add(this.cbxDNSNames);
            this.tabDNS.Controls.Add(this.edDNS2);
            this.tabDNS.Controls.Add(this.label7);
            this.tabDNS.Controls.Add(this.label5);
            this.tabDNS.Controls.Add(this.cbxNIC);
            this.tabDNS.Controls.Add(this.label6);
            this.tabDNS.Controls.Add(this.button1);
            this.tabDNS.Controls.Add(this.edDNS1);
            this.tabDNS.Location = new System.Drawing.Point(4, 22);
            this.tabDNS.Name = "tabDNS";
            this.tabDNS.Size = new System.Drawing.Size(454, 250);
            this.tabDNS.TabIndex = 2;
            this.tabDNS.Text = "DNS";
            this.tabDNS.UseVisualStyleBackColor = true;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(32, 26);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(69, 13);
            this.label8.TabIndex = 11;
            this.label8.Text = "Config name:";
            // 
            // btnDNSDelete
            // 
            this.btnDNSDelete.Location = new System.Drawing.Point(107, 106);
            this.btnDNSDelete.Name = "btnDNSDelete";
            this.btnDNSDelete.Size = new System.Drawing.Size(75, 23);
            this.btnDNSDelete.TabIndex = 3;
            this.btnDNSDelete.Text = "Delete";
            this.btnDNSDelete.UseVisualStyleBackColor = true;
            this.btnDNSDelete.Click += new System.EventHandler(this.btnDNSDelete_Click);
            // 
            // btnSaveDNS
            // 
            this.btnSaveDNS.Location = new System.Drawing.Point(188, 106);
            this.btnSaveDNS.Name = "btnSaveDNS";
            this.btnSaveDNS.Size = new System.Drawing.Size(75, 23);
            this.btnSaveDNS.TabIndex = 4;
            this.btnSaveDNS.Text = "Save";
            this.btnSaveDNS.UseVisualStyleBackColor = true;
            this.btnSaveDNS.Click += new System.EventHandler(this.btnSaveDNS_Click);
            // 
            // cbxDNSNames
            // 
            this.cbxDNSNames.FormattingEnabled = true;
            this.cbxDNSNames.Location = new System.Drawing.Point(107, 23);
            this.cbxDNSNames.Name = "cbxDNSNames";
            this.cbxDNSNames.Size = new System.Drawing.Size(157, 21);
            this.cbxDNSNames.TabIndex = 0;
            this.cbxDNSNames.SelectedIndexChanged += new System.EventHandler(this.cbxDNSNames_SelectedIndexChanged);
            // 
            // edDNS2
            // 
            this.edDNS2.Location = new System.Drawing.Point(107, 74);
            this.edDNS2.Name = "edDNS2";
            this.edDNS2.Size = new System.Drawing.Size(104, 20);
            this.edDNS2.TabIndex = 2;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(26, 77);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(74, 13);
            this.label7.TabIndex = 6;
            this.label7.Text = "DNS server 2:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(67, 148);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(33, 13);
            this.label5.TabIndex = 5;
            this.label5.Text = "NICs:";
            // 
            // cbxNIC
            // 
            this.cbxNIC.FormattingEnabled = true;
            this.cbxNIC.Location = new System.Drawing.Point(107, 145);
            this.cbxNIC.Name = "cbxNIC";
            this.cbxNIC.Size = new System.Drawing.Size(157, 21);
            this.cbxNIC.TabIndex = 5;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(26, 53);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(74, 13);
            this.label6.TabIndex = 3;
            this.label6.Text = "DNS server 1:";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(189, 172);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 6;
            this.button1.Text = "Change";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // edDNS1
            // 
            this.edDNS1.Location = new System.Drawing.Point(107, 50);
            this.edDNS1.Name = "edDNS1";
            this.edDNS1.Size = new System.Drawing.Size(105, 20);
            this.edDNS1.TabIndex = 1;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.tssLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 323);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(483, 22);
            this.statusStrip1.TabIndex = 4;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(41, 17);
            this.toolStripStatusLabel1.Text = "status:";
            // 
            // tssLabel
            // 
            this.tssLabel.Name = "tssLabel";
            this.tssLabel.Size = new System.Drawing.Size(0, 17);
            // 
            // SetupForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(483, 345);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Name = "SetupForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Setup";
            this.tabControl1.ResumeLayout(false);
            this.tabPageHosts.ResumeLayout(false);
            this.tabPageHosts.PerformLayout();
            this.tabPageFont.ResumeLayout(false);
            this.tabPageFont.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numRightMargin)).EndInit();
            this.tabDNS.ResumeLayout(false);
            this.tabDNS.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPageHosts;
        public System.Windows.Forms.RichTextBox rcbHostsConfig;
        private System.Windows.Forms.TabPage tabPageFont;
        private System.Windows.Forms.Button btnChange;
        public System.Windows.Forms.TextBox edFont;
        public System.Windows.Forms.FontDialog fontDialog1;
        private System.Windows.Forms.Button btnChangeColor;
        public System.Windows.Forms.TextBox edColor;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.ColorDialog colorDialog1;
        public System.Windows.Forms.NumericUpDown numRightMargin;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbxVersions;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnSaveNSwitch;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel tssLabel;
        private System.Windows.Forms.Button btNew;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TabPage tabDNS;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox edDNS1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cbxNIC;
        private System.Windows.Forms.Button btnSaveDNS;
        private System.Windows.Forms.ComboBox cbxDNSNames;
        private System.Windows.Forms.TextBox edDNS2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnDNSDelete;
        private System.Windows.Forms.Label label8;
    }
}