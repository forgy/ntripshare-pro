namespace NtripShare.Frm
{
    partial class FrmUserList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmUserList));
            this.panelEx1 = new DevComponents.DotNetBar.PanelEx();
            this.buttonXRemove = new DevComponents.DotNetBar.ButtonX();
            this.buttonXEdit = new DevComponents.DotNetBar.ButtonX();
            this.buttonAdd = new DevComponents.DotNetBar.ButtonX();
            this.dataGridViewConnections = new System.Windows.Forms.DataGridView();
            this.Column29 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column30 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column31 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column33 = new DevComponents.DotNetBar.Controls.DataGridViewDateTimeInputColumn();
            this.Column34 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelEx1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewConnections)).BeginInit();
            this.SuspendLayout();
            // 
            // panelEx1
            // 
            this.panelEx1.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx1.Controls.Add(this.buttonXRemove);
            this.panelEx1.Controls.Add(this.buttonXEdit);
            this.panelEx1.Controls.Add(this.buttonAdd);
            this.panelEx1.Controls.Add(this.dataGridViewConnections);
            this.panelEx1.DisabledBackColor = System.Drawing.Color.Empty;
            this.panelEx1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx1.Location = new System.Drawing.Point(0, 0);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(800, 450);
            this.panelEx1.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx1.Style.GradientAngle = 90;
            this.panelEx1.TabIndex = 0;
            // 
            // buttonXRemove
            // 
            this.buttonXRemove.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonXRemove.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonXRemove.Location = new System.Drawing.Point(743, 424);
            this.buttonXRemove.Name = "buttonXRemove";
            this.buttonXRemove.Size = new System.Drawing.Size(50, 23);
            this.buttonXRemove.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonXRemove.TabIndex = 2;
            this.buttonXRemove.Text = "删除";
            this.buttonXRemove.Click += new System.EventHandler(this.buttonXRemove_Click);
            // 
            // buttonXEdit
            // 
            this.buttonXEdit.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonXEdit.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonXEdit.Location = new System.Drawing.Point(686, 424);
            this.buttonXEdit.Name = "buttonXEdit";
            this.buttonXEdit.Size = new System.Drawing.Size(50, 23);
            this.buttonXEdit.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonXEdit.TabIndex = 1;
            this.buttonXEdit.Text = "编辑";
            this.buttonXEdit.Click += new System.EventHandler(this.buttonXEdit_Click);
            // 
            // buttonAdd
            // 
            this.buttonAdd.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonAdd.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonAdd.Location = new System.Drawing.Point(630, 424);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(50, 23);
            this.buttonAdd.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonAdd.TabIndex = 3;
            this.buttonAdd.Text = "添加";
            this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
            // 
            // dataGridViewConnections
            // 
            this.dataGridViewConnections.AllowUserToAddRows = false;
            this.dataGridViewConnections.AllowUserToDeleteRows = false;
            this.dataGridViewConnections.AllowUserToResizeRows = false;
            this.dataGridViewConnections.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridViewConnections.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridViewConnections.BackgroundColor = System.Drawing.Color.White;
            this.dataGridViewConnections.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewConnections.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column29,
            this.Column30,
            this.Column31,
            this.Column33,
            this.Column34});
            this.dataGridViewConnections.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewConnections.Location = new System.Drawing.Point(0, 0);
            this.dataGridViewConnections.Name = "dataGridViewConnections";
            this.dataGridViewConnections.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewConnections.ShowEditingIcon = false;
            this.dataGridViewConnections.Size = new System.Drawing.Size(800, 450);
            this.dataGridViewConnections.TabIndex = 5;
            // 
            // Column29
            // 
            this.Column29.DataPropertyName = "Username";
            this.Column29.HeaderText = "用户名";
            this.Column29.Name = "Column29";
            this.Column29.ReadOnly = true;
            this.Column29.Width = 66;
            // 
            // Column30
            // 
            this.Column30.DataPropertyName = "Password";
            this.Column30.HeaderText = "密码";
            this.Column30.Name = "Column30";
            this.Column30.ReadOnly = true;
            this.Column30.Width = 54;
            // 
            // Column31
            // 
            this.Column31.DataPropertyName = "MaxConnectCount";
            this.Column31.HeaderText = "最大连接数";
            this.Column31.Name = "Column31";
            this.Column31.ReadOnly = true;
            this.Column31.Width = 90;
            // 
            // Column33
            // 
            // 
            // 
            // 
            this.Column33.BackgroundStyle.Class = "DataGridViewDateTimeBorder";
            this.Column33.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.Column33.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.Column33.DataPropertyName = "DeadLineTime";
            this.Column33.Format = DevComponents.Editors.eDateTimePickerFormat.Custom;
            this.Column33.HeaderText = "过期时间";
            this.Column33.InputHorizontalAlignment = DevComponents.Editors.eHorizontalAlignment.Left;
            // 
            // 
            // 
            // 
            // 
            // 
            this.Column33.MonthCalendar.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.Column33.MonthCalendar.CalendarDimensions = new System.Drawing.Size(1, 1);
            // 
            // 
            // 
            this.Column33.MonthCalendar.CommandsBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.Column33.MonthCalendar.DisplayMonth = new System.DateTime(2020, 5, 1, 0, 0, 0, 0);
            this.Column33.MonthCalendar.FirstDayOfWeek = System.DayOfWeek.Monday;
            // 
            // 
            // 
            this.Column33.MonthCalendar.NavigationBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.Column33.Name = "Column33";
            this.Column33.Width = 78;
            // 
            // Column34
            // 
            this.Column34.DataPropertyName = "Des";
            this.Column34.HeaderText = "备注";
            this.Column34.Name = "Column34";
            this.Column34.ReadOnly = true;
            this.Column34.Width = 54;
            // 
            // FrmUserList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.panelEx1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmUserList";
            this.Text = "用户管理";
            this.Load += new System.EventHandler(this.FrmUserList_Load);
            this.panelEx1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewConnections)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.PanelEx panelEx1;
        private DevComponents.DotNetBar.ButtonX buttonAdd;
        private DevComponents.DotNetBar.ButtonX buttonXRemove;
        private DevComponents.DotNetBar.ButtonX buttonXEdit;
        internal System.Windows.Forms.DataGridView dataGridViewConnections;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column29;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column30;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column31;
        private DevComponents.DotNetBar.Controls.DataGridViewDateTimeInputColumn Column33;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column34;
    }
}