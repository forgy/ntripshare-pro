namespace NtripShare.Frm
{
    partial class FrmAlertSetting
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmAlertSetting));
            this.panelEx1 = new DevComponents.DotNetBar.PanelEx();
            this.buttonXJSSave = new DevComponents.DotNetBar.ButtonX();
            this.textBoxMailSend = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.textBoxMailPassword = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX30 = new DevComponents.DotNetBar.LabelX();
            this.integerInputSmtpPort = new DevComponents.Editors.IntegerInput();
            this.textBoxSMTPServer = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX28 = new DevComponents.DotNetBar.LabelX();
            this.labelX29 = new DevComponents.DotNetBar.LabelX();
            this.labelX27 = new DevComponents.DotNetBar.LabelX();
            this.textBoxAlarmEmail = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX23 = new DevComponents.DotNetBar.LabelX();
            this.panelEx1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.integerInputSmtpPort)).BeginInit();
            this.SuspendLayout();
            // 
            // panelEx1
            // 
            this.panelEx1.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx1.Controls.Add(this.buttonXJSSave);
            this.panelEx1.Controls.Add(this.textBoxMailSend);
            this.panelEx1.Controls.Add(this.textBoxMailPassword);
            this.panelEx1.Controls.Add(this.labelX30);
            this.panelEx1.Controls.Add(this.integerInputSmtpPort);
            this.panelEx1.Controls.Add(this.textBoxSMTPServer);
            this.panelEx1.Controls.Add(this.labelX28);
            this.panelEx1.Controls.Add(this.labelX29);
            this.panelEx1.Controls.Add(this.labelX27);
            this.panelEx1.Controls.Add(this.textBoxAlarmEmail);
            this.panelEx1.Controls.Add(this.labelX23);
            this.panelEx1.DisabledBackColor = System.Drawing.Color.Empty;
            this.panelEx1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx1.Location = new System.Drawing.Point(0, 0);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(364, 292);
            this.panelEx1.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx1.Style.GradientAngle = 90;
            this.panelEx1.TabIndex = 0;
            // 
            // buttonXJSSave
            // 
            this.buttonXJSSave.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonXJSSave.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonXJSSave.Location = new System.Drawing.Point(118, 234);
            this.buttonXJSSave.Name = "buttonXJSSave";
            this.buttonXJSSave.Size = new System.Drawing.Size(145, 23);
            this.buttonXJSSave.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonXJSSave.TabIndex = 77;
            this.buttonXJSSave.Text = "保存";
            this.buttonXJSSave.Click += new System.EventHandler(this.buttonXJSSave_Click);
            // 
            // textBoxMailSend
            // 
            // 
            // 
            // 
            this.textBoxMailSend.Border.Class = "TextBoxBorder";
            this.textBoxMailSend.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.textBoxMailSend.Location = new System.Drawing.Point(152, 29);
            this.textBoxMailSend.Name = "textBoxMailSend";
            this.textBoxMailSend.PreventEnterBeep = true;
            this.textBoxMailSend.Size = new System.Drawing.Size(150, 21);
            this.textBoxMailSend.TabIndex = 68;
            this.textBoxMailSend.Text = "gnss2020@163.com";
            // 
            // textBoxMailPassword
            // 
            // 
            // 
            // 
            this.textBoxMailPassword.Border.Class = "TextBoxBorder";
            this.textBoxMailPassword.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.textBoxMailPassword.Location = new System.Drawing.Point(152, 65);
            this.textBoxMailPassword.Name = "textBoxMailPassword";
            this.textBoxMailPassword.PreventEnterBeep = true;
            this.textBoxMailPassword.Size = new System.Drawing.Size(150, 21);
            this.textBoxMailPassword.TabIndex = 73;
            this.textBoxMailPassword.Text = "forgy7762892";
            this.textBoxMailPassword.UseSystemPasswordChar = true;
            // 
            // labelX30
            // 
            this.labelX30.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX30.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX30.Location = new System.Drawing.Point(11, 65);
            this.labelX30.Name = "labelX30";
            this.labelX30.Size = new System.Drawing.Size(135, 23);
            this.labelX30.TabIndex = 74;
            this.labelX30.Text = "邮箱密码：";
            this.labelX30.TextAlignment = System.Drawing.StringAlignment.Far;
            // 
            // integerInputSmtpPort
            // 
            // 
            // 
            // 
            this.integerInputSmtpPort.BackgroundStyle.Class = "DateTimeInputBackground";
            this.integerInputSmtpPort.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.integerInputSmtpPort.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
            this.integerInputSmtpPort.InputHorizontalAlignment = DevComponents.Editors.eHorizontalAlignment.Left;
            this.integerInputSmtpPort.Location = new System.Drawing.Point(152, 145);
            this.integerInputSmtpPort.Name = "integerInputSmtpPort";
            this.integerInputSmtpPort.ShowUpDown = true;
            this.integerInputSmtpPort.Size = new System.Drawing.Size(150, 21);
            this.integerInputSmtpPort.TabIndex = 65;
            this.integerInputSmtpPort.Value = 25;
            // 
            // textBoxSMTPServer
            // 
            // 
            // 
            // 
            this.textBoxSMTPServer.Border.Class = "TextBoxBorder";
            this.textBoxSMTPServer.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.textBoxSMTPServer.Location = new System.Drawing.Point(152, 103);
            this.textBoxSMTPServer.Name = "textBoxSMTPServer";
            this.textBoxSMTPServer.PreventEnterBeep = true;
            this.textBoxSMTPServer.Size = new System.Drawing.Size(150, 21);
            this.textBoxSMTPServer.TabIndex = 71;
            this.textBoxSMTPServer.Text = "smtp.163.com";
            // 
            // labelX28
            // 
            this.labelX28.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX28.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX28.Location = new System.Drawing.Point(11, 103);
            this.labelX28.Name = "labelX28";
            this.labelX28.Size = new System.Drawing.Size(135, 23);
            this.labelX28.TabIndex = 72;
            this.labelX28.Text = "SMTP服务器：";
            this.labelX28.TextAlignment = System.Drawing.StringAlignment.Far;
            // 
            // labelX29
            // 
            this.labelX29.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX29.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX29.Location = new System.Drawing.Point(11, 145);
            this.labelX29.Name = "labelX29";
            this.labelX29.Size = new System.Drawing.Size(135, 23);
            this.labelX29.TabIndex = 70;
            this.labelX29.Text = "SMTP端口：";
            this.labelX29.TextAlignment = System.Drawing.StringAlignment.Far;
            // 
            // labelX27
            // 
            this.labelX27.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX27.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX27.Location = new System.Drawing.Point(11, 29);
            this.labelX27.Name = "labelX27";
            this.labelX27.Size = new System.Drawing.Size(135, 23);
            this.labelX27.TabIndex = 69;
            this.labelX27.Text = "报警发送邮箱：";
            this.labelX27.TextAlignment = System.Drawing.StringAlignment.Far;
            // 
            // textBoxAlarmEmail
            // 
            // 
            // 
            // 
            this.textBoxAlarmEmail.Border.Class = "TextBoxBorder";
            this.textBoxAlarmEmail.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.textBoxAlarmEmail.Location = new System.Drawing.Point(152, 187);
            this.textBoxAlarmEmail.Name = "textBoxAlarmEmail";
            this.textBoxAlarmEmail.PreventEnterBeep = true;
            this.textBoxAlarmEmail.Size = new System.Drawing.Size(150, 21);
            this.textBoxAlarmEmail.TabIndex = 66;
            this.textBoxAlarmEmail.Text = "gnss2020@163.com";
            // 
            // labelX23
            // 
            this.labelX23.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX23.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX23.Location = new System.Drawing.Point(11, 187);
            this.labelX23.Name = "labelX23";
            this.labelX23.Size = new System.Drawing.Size(135, 23);
            this.labelX23.TabIndex = 67;
            this.labelX23.Text = "报警接收邮箱：";
            this.labelX23.TextAlignment = System.Drawing.StringAlignment.Far;
            // 
            // FrmAlertSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(364, 292);
            this.Controls.Add(this.panelEx1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FrmAlertSetting";
            this.Text = "报警设置";
            this.Load += new System.EventHandler(this.FrmAlertSetting_Load);
            this.panelEx1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.integerInputSmtpPort)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.PanelEx panelEx1;
        private DevComponents.DotNetBar.Controls.TextBoxX textBoxMailSend;
        private DevComponents.DotNetBar.Controls.TextBoxX textBoxMailPassword;
        private DevComponents.DotNetBar.LabelX labelX30;
        private DevComponents.Editors.IntegerInput integerInputSmtpPort;
        private DevComponents.DotNetBar.Controls.TextBoxX textBoxSMTPServer;
        private DevComponents.DotNetBar.LabelX labelX28;
        private DevComponents.DotNetBar.LabelX labelX29;
        private DevComponents.DotNetBar.LabelX labelX27;
        private DevComponents.DotNetBar.Controls.TextBoxX textBoxAlarmEmail;
        private DevComponents.DotNetBar.LabelX labelX23;
        private DevComponents.DotNetBar.ButtonX buttonXJSSave;
    }
}