namespace NtripShare.Frm
{
    partial class FrmAccount
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmAccount));
            this.checkBoxFloatCoord = new System.Windows.Forms.CheckBox();
            this.label50 = new System.Windows.Forms.Label();
            this.tbLon = new System.Windows.Forms.TextBox();
            this.label51 = new System.Windows.Forms.Label();
            this.tbLat = new System.Windows.Forms.TextBox();
            this.btGetMountPoints = new System.Windows.Forms.Button();
            this.cbMountPoints = new System.Windows.Forms.ComboBox();
            this.label49 = new System.Windows.Forms.Label();
            this.label47 = new System.Windows.Forms.Label();
            this.tbPassword = new System.Windows.Forms.TextBox();
            this.label48 = new System.Windows.Forms.Label();
            this.tbUsername = new System.Windows.Forms.TextBox();
            this.label46 = new System.Windows.Forms.Label();
            this.tbPort = new System.Windows.Forms.TextBox();
            this.label45 = new System.Windows.Forms.Label();
            this.tbIP = new System.Windows.Forms.TextBox();
            this.panelEx1 = new DevComponents.DotNetBar.PanelEx();
            this.checkBoxCool = new System.Windows.Forms.CheckBox();
            this.numericUpDownBuffer = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.buttonMap = new System.Windows.Forms.Button();
            this.comboBoxDataType = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonX2 = new DevComponents.DotNetBar.ButtonX();
            this.buttonX1 = new DevComponents.DotNetBar.ButtonX();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.checkBoxAddToPool = new System.Windows.Forms.CheckBox();
            this.panelEx1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownBuffer)).BeginInit();
            this.SuspendLayout();
            // 
            // checkBoxFloatCoord
            // 
            this.checkBoxFloatCoord.AutoSize = true;
            this.checkBoxFloatCoord.Location = new System.Drawing.Point(203, 273);
            this.checkBoxFloatCoord.Name = "checkBoxFloatCoord";
            this.checkBoxFloatCoord.Size = new System.Drawing.Size(60, 16);
            this.checkBoxFloatCoord.TabIndex = 35;
            this.checkBoxFloatCoord.Text = "固定站";
            this.checkBoxFloatCoord.UseVisualStyleBackColor = true;
            this.checkBoxFloatCoord.CheckedChanged += new System.EventHandler(this.checkBoxFloatCoord_CheckedChanged);
            // 
            // label50
            // 
            this.label50.AutoSize = true;
            this.label50.Location = new System.Drawing.Point(45, 316);
            this.label50.Name = "label50";
            this.label50.Size = new System.Drawing.Size(119, 12);
            this.label50.TabIndex = 32;
            this.label50.Text = "经度（DDD.MMSSSSS）";
            // 
            // tbLon
            // 
            this.tbLon.Location = new System.Drawing.Point(45, 334);
            this.tbLon.Name = "tbLon";
            this.tbLon.Size = new System.Drawing.Size(110, 21);
            this.tbLon.TabIndex = 31;
            // 
            // label51
            // 
            this.label51.AutoSize = true;
            this.label51.Location = new System.Drawing.Point(44, 375);
            this.label51.Name = "label51";
            this.label51.Size = new System.Drawing.Size(113, 12);
            this.label51.TabIndex = 30;
            this.label51.Text = "纬度（DD.MMSSSSS）";
            // 
            // tbLat
            // 
            this.tbLat.Location = new System.Drawing.Point(44, 393);
            this.tbLat.Name = "tbLat";
            this.tbLat.Size = new System.Drawing.Size(111, 21);
            this.tbLat.TabIndex = 29;
            // 
            // btGetMountPoints
            // 
            this.btGetMountPoints.Location = new System.Drawing.Point(279, 211);
            this.btGetMountPoints.Name = "btGetMountPoints";
            this.btGetMountPoints.Size = new System.Drawing.Size(46, 23);
            this.btGetMountPoints.TabIndex = 28;
            this.btGetMountPoints.Text = "获取";
            this.btGetMountPoints.UseVisualStyleBackColor = true;
            this.btGetMountPoints.Click += new System.EventHandler(this.btGetMountPoints_Click);
            // 
            // cbMountPoints
            // 
            this.cbMountPoints.FormattingEnabled = true;
            this.cbMountPoints.Location = new System.Drawing.Point(47, 213);
            this.cbMountPoints.Name = "cbMountPoints";
            this.cbMountPoints.Size = new System.Drawing.Size(226, 20);
            this.cbMountPoints.TabIndex = 27;
            // 
            // label49
            // 
            this.label49.AutoSize = true;
            this.label49.Location = new System.Drawing.Point(45, 198);
            this.label49.Name = "label49";
            this.label49.Size = new System.Drawing.Size(41, 12);
            this.label49.TabIndex = 26;
            this.label49.Text = "接入点";
            // 
            // label47
            // 
            this.label47.AutoSize = true;
            this.label47.Location = new System.Drawing.Point(203, 145);
            this.label47.Name = "label47";
            this.label47.Size = new System.Drawing.Size(29, 12);
            this.label47.TabIndex = 25;
            this.label47.Text = "密码";
            // 
            // tbPassword
            // 
            this.tbPassword.Location = new System.Drawing.Point(205, 160);
            this.tbPassword.Name = "tbPassword";
            this.tbPassword.Size = new System.Drawing.Size(120, 21);
            this.tbPassword.TabIndex = 24;
            // 
            // label48
            // 
            this.label48.AutoSize = true;
            this.label48.Location = new System.Drawing.Point(45, 145);
            this.label48.Name = "label48";
            this.label48.Size = new System.Drawing.Size(41, 12);
            this.label48.TabIndex = 23;
            this.label48.Text = "用户名";
            // 
            // tbUsername
            // 
            this.tbUsername.Location = new System.Drawing.Point(47, 160);
            this.tbUsername.Name = "tbUsername";
            this.tbUsername.Size = new System.Drawing.Size(120, 21);
            this.tbUsername.TabIndex = 22;
            // 
            // label46
            // 
            this.label46.AutoSize = true;
            this.label46.Location = new System.Drawing.Point(203, 92);
            this.label46.Name = "label46";
            this.label46.Size = new System.Drawing.Size(29, 12);
            this.label46.TabIndex = 21;
            this.label46.Text = "端口";
            // 
            // tbPort
            // 
            this.tbPort.Location = new System.Drawing.Point(205, 107);
            this.tbPort.Name = "tbPort";
            this.tbPort.Size = new System.Drawing.Size(120, 21);
            this.tbPort.TabIndex = 20;
            // 
            // label45
            // 
            this.label45.AutoSize = true;
            this.label45.Location = new System.Drawing.Point(45, 92);
            this.label45.Name = "label45";
            this.label45.Size = new System.Drawing.Size(17, 12);
            this.label45.TabIndex = 19;
            this.label45.Text = "IP";
            // 
            // tbIP
            // 
            this.tbIP.Location = new System.Drawing.Point(47, 107);
            this.tbIP.Name = "tbIP";
            this.tbIP.Size = new System.Drawing.Size(120, 21);
            this.tbIP.TabIndex = 18;
            // 
            // panelEx1
            // 
            this.panelEx1.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx1.Controls.Add(this.checkBoxCool);
            this.panelEx1.Controls.Add(this.numericUpDownBuffer);
            this.panelEx1.Controls.Add(this.label4);
            this.panelEx1.Controls.Add(this.label3);
            this.panelEx1.Controls.Add(this.buttonMap);
            this.panelEx1.Controls.Add(this.comboBoxDataType);
            this.panelEx1.Controls.Add(this.label2);
            this.panelEx1.Controls.Add(this.buttonX2);
            this.panelEx1.Controls.Add(this.buttonX1);
            this.panelEx1.Controls.Add(this.textBoxName);
            this.panelEx1.Controls.Add(this.label1);
            this.panelEx1.Controls.Add(this.checkBoxAddToPool);
            this.panelEx1.Controls.Add(this.tbIP);
            this.panelEx1.Controls.Add(this.checkBoxFloatCoord);
            this.panelEx1.Controls.Add(this.label45);
            this.panelEx1.Controls.Add(this.tbPort);
            this.panelEx1.Controls.Add(this.label46);
            this.panelEx1.Controls.Add(this.label50);
            this.panelEx1.Controls.Add(this.tbUsername);
            this.panelEx1.Controls.Add(this.tbLon);
            this.panelEx1.Controls.Add(this.label48);
            this.panelEx1.Controls.Add(this.label51);
            this.panelEx1.Controls.Add(this.tbPassword);
            this.panelEx1.Controls.Add(this.tbLat);
            this.panelEx1.Controls.Add(this.label47);
            this.panelEx1.Controls.Add(this.btGetMountPoints);
            this.panelEx1.Controls.Add(this.label49);
            this.panelEx1.Controls.Add(this.cbMountPoints);
            this.panelEx1.DisabledBackColor = System.Drawing.Color.Empty;
            this.panelEx1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx1.Location = new System.Drawing.Point(0, 0);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(364, 510);
            this.panelEx1.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx1.Style.GradientAngle = 90;
            this.panelEx1.TabIndex = 36;
            this.panelEx1.Click += new System.EventHandler(this.panelEx1_Click);
            // 
            // checkBoxCool
            // 
            this.checkBoxCool.AutoSize = true;
            this.checkBoxCool.Location = new System.Drawing.Point(203, 398);
            this.checkBoxCool.Name = "checkBoxCool";
            this.checkBoxCool.Size = new System.Drawing.Size(96, 16);
            this.checkBoxCool.TabIndex = 48;
            this.checkBoxCool.Text = "是否需要冷却";
            this.checkBoxCool.UseVisualStyleBackColor = true;
            // 
            // numericUpDownBuffer
            // 
            this.numericUpDownBuffer.Location = new System.Drawing.Point(203, 335);
            this.numericUpDownBuffer.Maximum = new decimal(new int[] {
            800000,
            0,
            0,
            0});
            this.numericUpDownBuffer.Name = "numericUpDownBuffer";
            this.numericUpDownBuffer.Size = new System.Drawing.Size(120, 21);
            this.numericUpDownBuffer.TabIndex = 47;
            this.numericUpDownBuffer.Value = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(329, 337);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(17, 12);
            this.label4.TabIndex = 46;
            this.label4.Text = "米";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(201, 319);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 45;
            this.label3.Text = "覆盖范围";
            // 
            // buttonMap
            // 
            this.buttonMap.Location = new System.Drawing.Point(159, 361);
            this.buttonMap.Name = "buttonMap";
            this.buttonMap.Size = new System.Drawing.Size(37, 23);
            this.buttonMap.TabIndex = 43;
            this.buttonMap.Text = "地图";
            this.buttonMap.UseVisualStyleBackColor = true;
            this.buttonMap.Click += new System.EventHandler(this.buttonMap_Click);
            // 
            // comboBoxDataType
            // 
            this.comboBoxDataType.FormattingEnabled = true;
            this.comboBoxDataType.Items.AddRange(new object[] {
            "RTCM32",
            "RTCM30"});
            this.comboBoxDataType.Location = new System.Drawing.Point(45, 270);
            this.comboBoxDataType.Name = "comboBoxDataType";
            this.comboBoxDataType.Size = new System.Drawing.Size(120, 20);
            this.comboBoxDataType.TabIndex = 42;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(45, 255);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 41;
            this.label2.Text = "数据类型";
            // 
            // buttonX2
            // 
            this.buttonX2.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX2.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonX2.Location = new System.Drawing.Point(213, 455);
            this.buttonX2.Name = "buttonX2";
            this.buttonX2.Size = new System.Drawing.Size(75, 23);
            this.buttonX2.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonX2.TabIndex = 40;
            this.buttonX2.Text = "取消";
            this.buttonX2.Click += new System.EventHandler(this.buttonX2_Click);
            // 
            // buttonX1
            // 
            this.buttonX1.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX1.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonX1.Location = new System.Drawing.Point(82, 455);
            this.buttonX1.Name = "buttonX1";
            this.buttonX1.Size = new System.Drawing.Size(75, 23);
            this.buttonX1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonX1.TabIndex = 39;
            this.buttonX1.Text = "确定";
            this.buttonX1.Click += new System.EventHandler(this.buttonX1_Click);
            // 
            // textBoxName
            // 
            this.textBoxName.Location = new System.Drawing.Point(49, 48);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(118, 21);
            this.textBoxName.TabIndex = 37;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(47, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 38;
            this.label1.Text = "名称：";
            // 
            // checkBoxAddToPool
            // 
            this.checkBoxAddToPool.AutoSize = true;
            this.checkBoxAddToPool.Location = new System.Drawing.Point(207, 50);
            this.checkBoxAddToPool.Name = "checkBoxAddToPool";
            this.checkBoxAddToPool.Size = new System.Drawing.Size(108, 16);
            this.checkBoxAddToPool.TabIndex = 36;
            this.checkBoxAddToPool.Text = "创建单独接入点";
            this.checkBoxAddToPool.UseVisualStyleBackColor = true;
            this.checkBoxAddToPool.CheckedChanged += new System.EventHandler(this.checkBoxAddToPool_CheckedChanged);
            // 
            // FrmAccount
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(364, 510);
            this.Controls.Add(this.panelEx1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmAccount";
            this.Text = "账号管理";
            this.Load += new System.EventHandler(this.FrmAccount_Load);
            this.panelEx1.ResumeLayout(false);
            this.panelEx1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownBuffer)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckBox checkBoxFloatCoord;
        private System.Windows.Forms.Label label50;
        private System.Windows.Forms.TextBox tbLon;
        private System.Windows.Forms.Label label51;
        private System.Windows.Forms.TextBox tbLat;
        private System.Windows.Forms.Button btGetMountPoints;
        private System.Windows.Forms.ComboBox cbMountPoints;
        private System.Windows.Forms.Label label49;
        private System.Windows.Forms.Label label47;
        private System.Windows.Forms.TextBox tbPassword;
        private System.Windows.Forms.Label label48;
        private System.Windows.Forms.TextBox tbUsername;
        private System.Windows.Forms.Label label46;
        private System.Windows.Forms.TextBox tbPort;
        private System.Windows.Forms.Label label45;
        private System.Windows.Forms.TextBox tbIP;
        private DevComponents.DotNetBar.PanelEx panelEx1;
        private System.Windows.Forms.CheckBox checkBoxAddToPool;
        private DevComponents.DotNetBar.ButtonX buttonX1;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.Label label1;
        private DevComponents.DotNetBar.ButtonX buttonX2;
        private System.Windows.Forms.ComboBox comboBoxDataType;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttonMap;
        private System.Windows.Forms.NumericUpDown numericUpDownBuffer;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox checkBoxCool;
    }
}