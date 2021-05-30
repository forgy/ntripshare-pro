namespace NtripShare.Frm
{
    partial class FrmTCP
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmTCP));
            this.label48 = new System.Windows.Forms.Label();
            this.tbDes = new System.Windows.Forms.TextBox();
            this.label46 = new System.Windows.Forms.Label();
            this.tbPort = new System.Windows.Forms.TextBox();
            this.panelEx1 = new DevComponents.DotNetBar.PanelEx();
            this.numericUpDownBuffer = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.buttonMap = new System.Windows.Forms.Button();
            this.checkBoxAddToPool = new System.Windows.Forms.CheckBox();
            this.textBoxLat = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxLon = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxMountPoint = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBoxDataType = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonX2 = new DevComponents.DotNetBar.ButtonX();
            this.buttonX1 = new DevComponents.DotNetBar.ButtonX();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panelEx1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownBuffer)).BeginInit();
            this.SuspendLayout();
            // 
            // label48
            // 
            this.label48.AutoSize = true;
            this.label48.Location = new System.Drawing.Point(49, 403);
            this.label48.Name = "label48";
            this.label48.Size = new System.Drawing.Size(41, 12);
            this.label48.TabIndex = 23;
            this.label48.Text = "备注：";
            // 
            // tbDes
            // 
            this.tbDes.Location = new System.Drawing.Point(94, 399);
            this.tbDes.Name = "tbDes";
            this.tbDes.Size = new System.Drawing.Size(197, 21);
            this.tbDes.TabIndex = 22;
            // 
            // label46
            // 
            this.label46.AutoSize = true;
            this.label46.Location = new System.Drawing.Point(47, 97);
            this.label46.Name = "label46";
            this.label46.Size = new System.Drawing.Size(41, 12);
            this.label46.TabIndex = 21;
            this.label46.Text = "端口：";
            // 
            // tbPort
            // 
            this.tbPort.Location = new System.Drawing.Point(94, 94);
            this.tbPort.Name = "tbPort";
            this.tbPort.Size = new System.Drawing.Size(197, 21);
            this.tbPort.TabIndex = 20;
            // 
            // panelEx1
            // 
            this.panelEx1.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx1.Controls.Add(this.numericUpDownBuffer);
            this.panelEx1.Controls.Add(this.label6);
            this.panelEx1.Controls.Add(this.label7);
            this.panelEx1.Controls.Add(this.buttonMap);
            this.panelEx1.Controls.Add(this.checkBoxAddToPool);
            this.panelEx1.Controls.Add(this.textBoxLat);
            this.panelEx1.Controls.Add(this.label5);
            this.panelEx1.Controls.Add(this.textBoxLon);
            this.panelEx1.Controls.Add(this.label4);
            this.panelEx1.Controls.Add(this.textBoxMountPoint);
            this.panelEx1.Controls.Add(this.label3);
            this.panelEx1.Controls.Add(this.comboBoxDataType);
            this.panelEx1.Controls.Add(this.label2);
            this.panelEx1.Controls.Add(this.buttonX2);
            this.panelEx1.Controls.Add(this.buttonX1);
            this.panelEx1.Controls.Add(this.textBoxName);
            this.panelEx1.Controls.Add(this.label1);
            this.panelEx1.Controls.Add(this.tbPort);
            this.panelEx1.Controls.Add(this.label46);
            this.panelEx1.Controls.Add(this.tbDes);
            this.panelEx1.Controls.Add(this.label48);
            this.panelEx1.DisabledBackColor = System.Drawing.Color.Empty;
            this.panelEx1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx1.Location = new System.Drawing.Point(0, 0);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(344, 502);
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
            // numericUpDownBuffer
            // 
            this.numericUpDownBuffer.Location = new System.Drawing.Point(94, 354);
            this.numericUpDownBuffer.Maximum = new decimal(new int[] {
            800000,
            0,
            0,
            0});
            this.numericUpDownBuffer.Name = "numericUpDownBuffer";
            this.numericUpDownBuffer.Size = new System.Drawing.Size(197, 21);
            this.numericUpDownBuffer.TabIndex = 52;
            this.numericUpDownBuffer.Value = new decimal(new int[] {
            30000,
            0,
            0,
            0});
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(293, 356);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(17, 12);
            this.label6.TabIndex = 51;
            this.label6.Text = "米";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(24, 356);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 12);
            this.label7.TabIndex = 50;
            this.label7.Text = "覆盖范围：";
            // 
            // buttonMap
            // 
            this.buttonMap.Location = new System.Drawing.Point(295, 257);
            this.buttonMap.Name = "buttonMap";
            this.buttonMap.Size = new System.Drawing.Size(37, 23);
            this.buttonMap.TabIndex = 44;
            this.buttonMap.Text = "地图";
            this.buttonMap.UseVisualStyleBackColor = true;
            this.buttonMap.Click += new System.EventHandler(this.buttonMap_Click);
            // 
            // checkBoxAddToPool
            // 
            this.checkBoxAddToPool.AutoSize = true;
            this.checkBoxAddToPool.Location = new System.Drawing.Point(94, 321);
            this.checkBoxAddToPool.Name = "checkBoxAddToPool";
            this.checkBoxAddToPool.Size = new System.Drawing.Size(108, 16);
            this.checkBoxAddToPool.TabIndex = 49;
            this.checkBoxAddToPool.Text = "创建单独接入点";
            this.checkBoxAddToPool.UseVisualStyleBackColor = true;
            // 
            // textBoxLat
            // 
            this.textBoxLat.Location = new System.Drawing.Point(94, 281);
            this.textBoxLat.Name = "textBoxLat";
            this.textBoxLat.Size = new System.Drawing.Size(197, 21);
            this.textBoxLat.TabIndex = 47;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(47, 285);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 12);
            this.label5.TabIndex = 48;
            this.label5.Text = "纬度：";
            // 
            // textBoxLon
            // 
            this.textBoxLon.Location = new System.Drawing.Point(94, 233);
            this.textBoxLon.Name = "textBoxLon";
            this.textBoxLon.Size = new System.Drawing.Size(197, 21);
            this.textBoxLon.TabIndex = 45;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(47, 237);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 46;
            this.label4.Text = "经度：";
            // 
            // textBoxMountPoint
            // 
            this.textBoxMountPoint.Location = new System.Drawing.Point(94, 186);
            this.textBoxMountPoint.Name = "textBoxMountPoint";
            this.textBoxMountPoint.Size = new System.Drawing.Size(197, 21);
            this.textBoxMountPoint.TabIndex = 43;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(35, 190);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 44;
            this.label3.Text = "接入点：";
            // 
            // comboBoxDataType
            // 
            this.comboBoxDataType.FormattingEnabled = true;
            this.comboBoxDataType.Items.AddRange(new object[] {
            "RTCM32",
            "RTCM30"});
            this.comboBoxDataType.Location = new System.Drawing.Point(94, 141);
            this.comboBoxDataType.Name = "comboBoxDataType";
            this.comboBoxDataType.Size = new System.Drawing.Size(197, 20);
            this.comboBoxDataType.TabIndex = 42;
            this.comboBoxDataType.SelectedIndexChanged += new System.EventHandler(this.comboBoxDataType_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(24, 144);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 41;
            this.label2.Text = "数据类型：";
            // 
            // buttonX2
            // 
            this.buttonX2.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX2.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonX2.Location = new System.Drawing.Point(198, 442);
            this.buttonX2.Name = "buttonX2";
            this.buttonX2.Size = new System.Drawing.Size(75, 23);
            this.buttonX2.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonX2.TabIndex = 40;
            this.buttonX2.Text = "取消";
            // 
            // buttonX1
            // 
            this.buttonX1.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX1.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonX1.Location = new System.Drawing.Point(67, 442);
            this.buttonX1.Name = "buttonX1";
            this.buttonX1.Size = new System.Drawing.Size(75, 23);
            this.buttonX1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonX1.TabIndex = 39;
            this.buttonX1.Text = "确定";
            this.buttonX1.Click += new System.EventHandler(this.buttonX1_Click);
            // 
            // textBoxName
            // 
            this.textBoxName.Location = new System.Drawing.Point(94, 44);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(197, 21);
            this.textBoxName.TabIndex = 37;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(47, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 38;
            this.label1.Text = "名称：";
            // 
            // FrmTCP
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(344, 502);
            this.Controls.Add(this.panelEx1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmTCP";
            this.Text = "TCP管理";
            this.Load += new System.EventHandler(this.FrmAccount_Load);
            this.panelEx1.ResumeLayout(false);
            this.panelEx1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownBuffer)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label label48;
        private System.Windows.Forms.TextBox tbDes;
        private System.Windows.Forms.Label label46;
        private System.Windows.Forms.TextBox tbPort;
        private DevComponents.DotNetBar.PanelEx panelEx1;
        private DevComponents.DotNetBar.ButtonX buttonX1;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.Label label1;
        private DevComponents.DotNetBar.ButtonX buttonX2;
        private System.Windows.Forms.ComboBox comboBoxDataType;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxMountPoint;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxLat;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBoxLon;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox checkBoxAddToPool;
        private System.Windows.Forms.Button buttonMap;
        private System.Windows.Forms.NumericUpDown numericUpDownBuffer;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
    }
}