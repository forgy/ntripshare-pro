// VBConversions Note: VB project level imports
using System.Collections.Generic;
using System;
using System.Drawing;
using System.Diagnostics;
using System.Data;
using Microsoft.VisualBasic;
using System.Collections;
using System.Windows.Forms;
// End of VB project level imports


namespace NtripShare
{
	[global::Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]public 
	partial class CasterForm : System.Windows.Forms.Form
	{
		
		//Form overrides dispose to clean up the component list.
		[System.Diagnostics.DebuggerNonUserCode()]protected override void Dispose(bool disposing)
		{
			try
			{
				if (disposing && components != null)
				{
					components.Dispose();
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}
		
		//NOTE: The following procedure is required by the Windows Form Designer
		//It can be modified using the Windows Form Designer.
		//Do not modify it using the code editor.
		[System.Diagnostics.DebuggerStepThrough()]private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CasterForm));
            this.Timer1 = new System.Windows.Forms.Timer(this.components);
            this.StatusStrip = new System.Windows.Forms.StatusStrip();
            this.lblStatusBar = new System.Windows.Forms.ToolStripStatusLabel();
            this.TabControl1 = new System.Windows.Forms.TabControl();
            this.TabConnections = new System.Windows.Forms.TabPage();
            this.dgvConnections = new System.Windows.Forms.DataGridView();
            this.TabMountPoints = new System.Windows.Forms.TabPage();
            this.btnSaveMountPoints = new System.Windows.Forms.Button();
            this.btnReloadMountPoints = new System.Windows.Forms.Button();
            this.dgvMountPoints = new System.Windows.Forms.DataGridView();
            this.tabUsers = new System.Windows.Forms.TabPage();
            this.btnSaveUsers = new System.Windows.Forms.Button();
            this.btnReloadUsers = new System.Windows.Forms.Button();
            this.dgvUsers = new System.Windows.Forms.DataGridView();
            this.TabServer = new System.Windows.Forms.TabPage();
            this.Label3 = new System.Windows.Forms.Label();
            this.lblSerialStatus = new System.Windows.Forms.Label();
            this.tbServerMountPoint = new System.Windows.Forms.TextBox();
            this.Label11 = new System.Windows.Forms.Label();
            this.btnSerialConnect = new System.Windows.Forms.Button();
            this.lblServerSP = new System.Windows.Forms.Label();
            this.lblServer1 = new System.Windows.Forms.Label();
            this.lblServerBR = new System.Windows.Forms.Label();
            this.lblServerNone = new System.Windows.Forms.Label();
            this.lblServerDB = new System.Windows.Forms.Label();
            this.boxDataBits = new System.Windows.Forms.ComboBox();
            this.lblServerP = new System.Windows.Forms.Label();
            this.boxSpeed = new System.Windows.Forms.ComboBox();
            this.lblServerSB = new System.Windows.Forms.Label();
            this.boxSerialPort = new System.Windows.Forms.ComboBox();
            this.tbServerInfo = new System.Windows.Forms.TextBox();
            this.TabLogs = new System.Windows.Forms.TabPage();
            this.rtbEvents = new System.Windows.Forms.RichTextBox();
            this.boxDoLogging = new System.Windows.Forms.ComboBox();
            this.Label9 = new System.Windows.Forms.Label();
            this.TabAbout = new System.Windows.Forms.TabPage();
            this.Label2 = new System.Windows.Forms.Label();
            this.Label1 = new System.Windows.Forms.Label();
            this.lblVersion = new System.Windows.Forms.Label();
            this.StatusStrip.SuspendLayout();
            this.TabControl1.SuspendLayout();
            this.TabConnections.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvConnections)).BeginInit();
            this.TabMountPoints.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMountPoints)).BeginInit();
            this.tabUsers.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsers)).BeginInit();
            this.TabServer.SuspendLayout();
            this.TabLogs.SuspendLayout();
            this.TabAbout.SuspendLayout();
            this.SuspendLayout();
            // 
            // Timer1
            // 
            // 
            // StatusStrip
            // 
            this.StatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblStatusBar});
            this.StatusStrip.Location = new System.Drawing.Point(0, 346);
            this.StatusStrip.Name = "StatusStrip";
            this.StatusStrip.Size = new System.Drawing.Size(630, 22);
            this.StatusStrip.TabIndex = 5;
            this.StatusStrip.Text = "StatusStrip";
            // 
            // lblStatusBar
            // 
            this.lblStatusBar.Name = "lblStatusBar";
            this.lblStatusBar.Size = new System.Drawing.Size(96, 17);
            this.lblStatusBar.Text = "Not Connected";
            // 
            // TabControl1
            // 
            this.TabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TabControl1.Controls.Add(this.TabConnections);
            this.TabControl1.Controls.Add(this.TabMountPoints);
            this.TabControl1.Controls.Add(this.tabUsers);
            this.TabControl1.Controls.Add(this.TabServer);
            this.TabControl1.Controls.Add(this.TabLogs);
            this.TabControl1.Controls.Add(this.TabAbout);
            this.TabControl1.Location = new System.Drawing.Point(12, 11);
            this.TabControl1.Name = "TabControl1";
            this.TabControl1.SelectedIndex = 0;
            this.TabControl1.Size = new System.Drawing.Size(606, 336);
            this.TabControl1.TabIndex = 6;
            // 
            // TabConnections
            // 
            this.TabConnections.BackColor = System.Drawing.SystemColors.Window;
            this.TabConnections.Controls.Add(this.dgvConnections);
            this.TabConnections.Location = new System.Drawing.Point(4, 22);
            this.TabConnections.Name = "TabConnections";
            this.TabConnections.Padding = new System.Windows.Forms.Padding(3);
            this.TabConnections.Size = new System.Drawing.Size(598, 310);
            this.TabConnections.TabIndex = 1;
            this.TabConnections.Text = "连接用户";
            // 
            // dgvConnections
            // 
            this.dgvConnections.AllowUserToAddRows = false;
            this.dgvConnections.AllowUserToDeleteRows = false;
            this.dgvConnections.AllowUserToResizeRows = false;
            this.dgvConnections.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvConnections.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvConnections.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvConnections.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvConnections.Location = new System.Drawing.Point(0, 0);
            this.dgvConnections.MultiSelect = false;
            this.dgvConnections.Name = "dgvConnections";
            this.dgvConnections.ReadOnly = true;
            this.dgvConnections.RowHeadersVisible = false;
            this.dgvConnections.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvConnections.ShowEditingIcon = false;
            this.dgvConnections.Size = new System.Drawing.Size(598, 312);
            this.dgvConnections.TabIndex = 2;
            // 
            // TabMountPoints
            // 
            this.TabMountPoints.BackColor = System.Drawing.SystemColors.Window;
            this.TabMountPoints.Controls.Add(this.btnSaveMountPoints);
            this.TabMountPoints.Controls.Add(this.btnReloadMountPoints);
            this.TabMountPoints.Controls.Add(this.dgvMountPoints);
            this.TabMountPoints.Location = new System.Drawing.Point(4, 22);
            this.TabMountPoints.Name = "TabMountPoints";
            this.TabMountPoints.Padding = new System.Windows.Forms.Padding(3);
            this.TabMountPoints.Size = new System.Drawing.Size(598, 310);
            this.TabMountPoints.TabIndex = 2;
            this.TabMountPoints.Text = "挂载点";
            // 
            // btnSaveMountPoints
            // 
            this.btnSaveMountPoints.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSaveMountPoints.Location = new System.Drawing.Point(520, 288);
            this.btnSaveMountPoints.Name = "btnSaveMountPoints";
            this.btnSaveMountPoints.Size = new System.Drawing.Size(75, 21);
            this.btnSaveMountPoints.TabIndex = 5;
            this.btnSaveMountPoints.Text = "Save";
            this.btnSaveMountPoints.UseVisualStyleBackColor = true;
            // 
            // btnReloadMountPoints
            // 
            this.btnReloadMountPoints.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnReloadMountPoints.Location = new System.Drawing.Point(3, 288);
            this.btnReloadMountPoints.Name = "btnReloadMountPoints";
            this.btnReloadMountPoints.Size = new System.Drawing.Size(110, 21);
            this.btnReloadMountPoints.TabIndex = 4;
            this.btnReloadMountPoints.Text = "Reload from File";
            this.btnReloadMountPoints.UseVisualStyleBackColor = true;
            // 
            // dgvMountPoints
            // 
            this.dgvMountPoints.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvMountPoints.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvMountPoints.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvMountPoints.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMountPoints.Location = new System.Drawing.Point(0, 0);
            this.dgvMountPoints.MultiSelect = false;
            this.dgvMountPoints.Name = "dgvMountPoints";
            this.dgvMountPoints.RowHeadersWidth = 20;
            this.dgvMountPoints.Size = new System.Drawing.Size(598, 282);
            this.dgvMountPoints.TabIndex = 3;
            // 
            // tabUsers
            // 
            this.tabUsers.BackColor = System.Drawing.SystemColors.Window;
            this.tabUsers.Controls.Add(this.btnSaveUsers);
            this.tabUsers.Controls.Add(this.btnReloadUsers);
            this.tabUsers.Controls.Add(this.dgvUsers);
            this.tabUsers.Location = new System.Drawing.Point(4, 22);
            this.tabUsers.Name = "tabUsers";
            this.tabUsers.Padding = new System.Windows.Forms.Padding(3);
            this.tabUsers.Size = new System.Drawing.Size(598, 310);
            this.tabUsers.TabIndex = 3;
            this.tabUsers.Text = "用户";
            // 
            // btnSaveUsers
            // 
            this.btnSaveUsers.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSaveUsers.Location = new System.Drawing.Point(520, 288);
            this.btnSaveUsers.Name = "btnSaveUsers";
            this.btnSaveUsers.Size = new System.Drawing.Size(75, 21);
            this.btnSaveUsers.TabIndex = 6;
            this.btnSaveUsers.Text = "Save";
            this.btnSaveUsers.UseVisualStyleBackColor = true;
            // 
            // btnReloadUsers
            // 
            this.btnReloadUsers.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnReloadUsers.Location = new System.Drawing.Point(3, 288);
            this.btnReloadUsers.Name = "btnReloadUsers";
            this.btnReloadUsers.Size = new System.Drawing.Size(110, 21);
            this.btnReloadUsers.TabIndex = 5;
            this.btnReloadUsers.Text = "Reload from File";
            this.btnReloadUsers.UseVisualStyleBackColor = true;
            // 
            // dgvUsers
            // 
            this.dgvUsers.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvUsers.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvUsers.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvUsers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvUsers.Location = new System.Drawing.Point(0, 0);
            this.dgvUsers.MultiSelect = false;
            this.dgvUsers.Name = "dgvUsers";
            this.dgvUsers.RowHeadersWidth = 20;
            this.dgvUsers.Size = new System.Drawing.Size(598, 282);
            this.dgvUsers.TabIndex = 4;
            // 
            // TabServer
            // 
            this.TabServer.BackColor = System.Drawing.SystemColors.Window;
            this.TabServer.Controls.Add(this.Label3);
            this.TabServer.Controls.Add(this.lblSerialStatus);
            this.TabServer.Controls.Add(this.tbServerMountPoint);
            this.TabServer.Controls.Add(this.Label11);
            this.TabServer.Controls.Add(this.btnSerialConnect);
            this.TabServer.Controls.Add(this.lblServerSP);
            this.TabServer.Controls.Add(this.lblServer1);
            this.TabServer.Controls.Add(this.lblServerBR);
            this.TabServer.Controls.Add(this.lblServerNone);
            this.TabServer.Controls.Add(this.lblServerDB);
            this.TabServer.Controls.Add(this.boxDataBits);
            this.TabServer.Controls.Add(this.lblServerP);
            this.TabServer.Controls.Add(this.boxSpeed);
            this.TabServer.Controls.Add(this.lblServerSB);
            this.TabServer.Controls.Add(this.boxSerialPort);
            this.TabServer.Controls.Add(this.tbServerInfo);
            this.TabServer.Location = new System.Drawing.Point(4, 22);
            this.TabServer.Name = "TabServer";
            this.TabServer.Padding = new System.Windows.Forms.Padding(3);
            this.TabServer.Size = new System.Drawing.Size(598, 310);
            this.TabServer.TabIndex = 5;
            this.TabServer.Text = "服务器";
            // 
            // Label3
            // 
            this.Label3.AutoSize = true;
            this.Label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label3.Location = new System.Drawing.Point(172, 283);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(60, 20);
            this.Label3.TabIndex = 65;
            this.Label3.Text = "Status:";
            // 
            // lblSerialStatus
            // 
            this.lblSerialStatus.AutoSize = true;
            this.lblSerialStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSerialStatus.Location = new System.Drawing.Point(228, 283);
            this.lblSerialStatus.Name = "lblSerialStatus";
            this.lblSerialStatus.Size = new System.Drawing.Size(107, 20);
            this.lblSerialStatus.TabIndex = 64;
            this.lblSerialStatus.Text = "Disconnected";
            // 
            // tbServerMountPoint
            // 
            this.tbServerMountPoint.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbServerMountPoint.Location = new System.Drawing.Point(110, 90);
            this.tbServerMountPoint.Name = "tbServerMountPoint";
            this.tbServerMountPoint.Size = new System.Drawing.Size(303, 26);
            this.tbServerMountPoint.TabIndex = 63;
            // 
            // Label11
            // 
            this.Label11.AutoSize = true;
            this.Label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label11.Location = new System.Drawing.Point(6, 92);
            this.Label11.Name = "Label11";
            this.Label11.Size = new System.Drawing.Size(98, 20);
            this.Label11.TabIndex = 61;
            this.Label11.Text = "Mount Point:";
            // 
            // btnSerialConnect
            // 
            this.btnSerialConnect.BackColor = System.Drawing.Color.LightGray;
            this.btnSerialConnect.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSerialConnect.Location = new System.Drawing.Point(30, 276);
            this.btnSerialConnect.Name = "btnSerialConnect";
            this.btnSerialConnect.Size = new System.Drawing.Size(136, 30);
            this.btnSerialConnect.TabIndex = 60;
            this.btnSerialConnect.Text = "Connect";
            this.btnSerialConnect.UseVisualStyleBackColor = false;
            // 
            // lblServerSP
            // 
            this.lblServerSP.AutoSize = true;
            this.lblServerSP.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblServerSP.Location = new System.Drawing.Point(18, 124);
            this.lblServerSP.Name = "lblServerSP";
            this.lblServerSP.Size = new System.Drawing.Size(86, 20);
            this.lblServerSP.TabIndex = 15;
            this.lblServerSP.Text = "Serial Port:";
            // 
            // lblServer1
            // 
            this.lblServer1.AutoSize = true;
            this.lblServer1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblServer1.Location = new System.Drawing.Point(110, 249);
            this.lblServer1.Name = "lblServer1";
            this.lblServer1.Size = new System.Drawing.Size(18, 20);
            this.lblServer1.TabIndex = 24;
            this.lblServer1.Text = "1";
            // 
            // lblServerBR
            // 
            this.lblServerBR.AutoSize = true;
            this.lblServerBR.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblServerBR.Location = new System.Drawing.Point(14, 155);
            this.lblServerBR.Name = "lblServerBR";
            this.lblServerBR.Size = new System.Drawing.Size(90, 20);
            this.lblServerBR.TabIndex = 16;
            this.lblServerBR.Text = "Baud Rate:";
            // 
            // lblServerNone
            // 
            this.lblServerNone.AutoSize = true;
            this.lblServerNone.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblServerNone.Location = new System.Drawing.Point(110, 218);
            this.lblServerNone.Name = "lblServerNone";
            this.lblServerNone.Size = new System.Drawing.Size(47, 20);
            this.lblServerNone.TabIndex = 23;
            this.lblServerNone.Text = "None";
            // 
            // lblServerDB
            // 
            this.lblServerDB.AutoSize = true;
            this.lblServerDB.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblServerDB.Location = new System.Drawing.Point(25, 186);
            this.lblServerDB.Name = "lblServerDB";
            this.lblServerDB.Size = new System.Drawing.Size(79, 20);
            this.lblServerDB.TabIndex = 17;
            this.lblServerDB.Text = "Data Bits:";
            // 
            // boxDataBits
            // 
            this.boxDataBits.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.boxDataBits.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.boxDataBits.FormattingEnabled = true;
            this.boxDataBits.Items.AddRange(new object[] {
            "7",
            "8"});
            this.boxDataBits.Location = new System.Drawing.Point(110, 184);
            this.boxDataBits.Name = "boxDataBits";
            this.boxDataBits.Size = new System.Drawing.Size(77, 28);
            this.boxDataBits.TabIndex = 22;
            // 
            // lblServerP
            // 
            this.lblServerP.AutoSize = true;
            this.lblServerP.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblServerP.Location = new System.Drawing.Point(52, 218);
            this.lblServerP.Name = "lblServerP";
            this.lblServerP.Size = new System.Drawing.Size(52, 20);
            this.lblServerP.TabIndex = 18;
            this.lblServerP.Text = "Parity:";
            // 
            // boxSpeed
            // 
            this.boxSpeed.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.boxSpeed.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.boxSpeed.FormattingEnabled = true;
            this.boxSpeed.Items.AddRange(new object[] {
            "2400",
            "4800",
            "9600",
            "14400",
            "19200",
            "38400",
            "57600",
            "115200"});
            this.boxSpeed.Location = new System.Drawing.Point(110, 152);
            this.boxSpeed.Name = "boxSpeed";
            this.boxSpeed.Size = new System.Drawing.Size(150, 28);
            this.boxSpeed.TabIndex = 21;
            // 
            // lblServerSB
            // 
            this.lblServerSB.AutoSize = true;
            this.lblServerSB.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblServerSB.Location = new System.Drawing.Point(26, 249);
            this.lblServerSB.Name = "lblServerSB";
            this.lblServerSB.Size = new System.Drawing.Size(78, 20);
            this.lblServerSB.TabIndex = 19;
            this.lblServerSB.Text = "Stop Bits:";
            // 
            // boxSerialPort
            // 
            this.boxSerialPort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.boxSerialPort.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.boxSerialPort.FormattingEnabled = true;
            this.boxSerialPort.Location = new System.Drawing.Point(110, 121);
            this.boxSerialPort.Name = "boxSerialPort";
            this.boxSerialPort.Size = new System.Drawing.Size(303, 28);
            this.boxSerialPort.TabIndex = 20;
            // 
            // tbServerInfo
            // 
            this.tbServerInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbServerInfo.Location = new System.Drawing.Point(6, 6);
            this.tbServerInfo.Multiline = true;
            this.tbServerInfo.Name = "tbServerInfo";
            this.tbServerInfo.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbServerInfo.Size = new System.Drawing.Size(586, 66);
            this.tbServerInfo.TabIndex = 0;
            // 
            // TabLogs
            // 
            this.TabLogs.BackColor = System.Drawing.SystemColors.Window;
            this.TabLogs.Controls.Add(this.rtbEvents);
            this.TabLogs.Controls.Add(this.boxDoLogging);
            this.TabLogs.Controls.Add(this.Label9);
            this.TabLogs.Location = new System.Drawing.Point(4, 22);
            this.TabLogs.Name = "TabLogs";
            this.TabLogs.Padding = new System.Windows.Forms.Padding(3);
            this.TabLogs.Size = new System.Drawing.Size(598, 310);
            this.TabLogs.TabIndex = 6;
            this.TabLogs.Text = "日志";
            // 
            // rtbEvents
            // 
            this.rtbEvents.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rtbEvents.BackColor = System.Drawing.Color.White;
            this.rtbEvents.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtbEvents.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtbEvents.ForeColor = System.Drawing.Color.Black;
            this.rtbEvents.Location = new System.Drawing.Point(6, 37);
            this.rtbEvents.Name = "rtbEvents";
            this.rtbEvents.ReadOnly = true;
            this.rtbEvents.Size = new System.Drawing.Size(592, 275);
            this.rtbEvents.TabIndex = 19;
            this.rtbEvents.Text = "Events will show up here.";
            // 
            // boxDoLogging
            // 
            this.boxDoLogging.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.boxDoLogging.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.boxDoLogging.FormattingEnabled = true;
            this.boxDoLogging.Items.AddRange(new object[] {
            "No",
            "Yes"});
            this.boxDoLogging.Location = new System.Drawing.Point(313, 3);
            this.boxDoLogging.Name = "boxDoLogging";
            this.boxDoLogging.Size = new System.Drawing.Size(77, 28);
            this.boxDoLogging.TabIndex = 17;
            this.boxDoLogging.SelectionChangeCommitted += new System.EventHandler(this.boxDoLogging_SelectionChangeCommitted);
            // 
            // Label9
            // 
            this.Label9.AutoSize = true;
            this.Label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label9.Location = new System.Drawing.Point(6, 6);
            this.Label9.Name = "Label9";
            this.Label9.Size = new System.Drawing.Size(301, 20);
            this.Label9.TabIndex = 16;
            this.Label9.Text = "Record Events in /Logs/YYYYMMDD.txt?";
            // 
            // TabAbout
            // 
            this.TabAbout.BackColor = System.Drawing.SystemColors.Window;
            this.TabAbout.Controls.Add(this.Label2);
            this.TabAbout.Controls.Add(this.Label1);
            this.TabAbout.Controls.Add(this.lblVersion);
            this.TabAbout.Location = new System.Drawing.Point(4, 22);
            this.TabAbout.Name = "TabAbout";
            this.TabAbout.Padding = new System.Windows.Forms.Padding(3);
            this.TabAbout.Size = new System.Drawing.Size(598, 310);
            this.TabAbout.TabIndex = 4;
            this.TabAbout.Text = "关于";
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label2.Location = new System.Drawing.Point(6, 75);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(156, 20);
            this.Label2.TabIndex = 3;
            this.Label2.Text = "giserpeng@163.com";
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label1.Location = new System.Drawing.Point(6, 11);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(133, 20);
            this.Label1.TabIndex = 2;
            this.Label1.Text = "GG NTRIP Caster";
            // 
            // lblVersion
            // 
            this.lblVersion.AutoSize = true;
            this.lblVersion.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVersion.Location = new System.Drawing.Point(6, 41);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(89, 20);
            this.lblVersion.TabIndex = 1;
            this.lblVersion.Text = "Version:0.1";
            // 
            // CasterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(630, 368);
            this.Controls.Add(this.TabControl1);
            this.Controls.Add(this.StatusStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "CasterForm";
            this.Text = "GG NTRIP Caster";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.StatusStrip.ResumeLayout(false);
            this.StatusStrip.PerformLayout();
            this.TabControl1.ResumeLayout(false);
            this.TabConnections.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvConnections)).EndInit();
            this.TabMountPoints.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMountPoints)).EndInit();
            this.tabUsers.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsers)).EndInit();
            this.TabServer.ResumeLayout(false);
            this.TabServer.PerformLayout();
            this.TabLogs.ResumeLayout(false);
            this.TabLogs.PerformLayout();
            this.TabAbout.ResumeLayout(false);
            this.TabAbout.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		internal System.Windows.Forms.Timer Timer1;
		internal System.Windows.Forms.StatusStrip StatusStrip;
		internal System.Windows.Forms.ToolStripStatusLabel lblStatusBar;
		internal System.Windows.Forms.TabControl TabControl1;
		internal System.Windows.Forms.TabPage TabConnections;
		internal System.Windows.Forms.DataGridView dgvConnections;
		internal System.Windows.Forms.TabPage TabMountPoints;
		internal System.Windows.Forms.TabPage tabUsers;
		internal System.Windows.Forms.TabPage TabAbout;
		internal System.Windows.Forms.Label Label2;
		internal System.Windows.Forms.Label Label1;
		internal System.Windows.Forms.Label lblVersion;
		internal System.Windows.Forms.DataGridView dgvMountPoints;
		internal System.Windows.Forms.DataGridView dgvUsers;
		internal System.Windows.Forms.TabPage TabServer;
		internal System.Windows.Forms.TextBox tbServerInfo;
		internal System.Windows.Forms.TabPage TabLogs;
		internal System.Windows.Forms.Label Label9;
		internal System.Windows.Forms.Label Label11;
		internal System.Windows.Forms.Button btnSerialConnect;
		internal System.Windows.Forms.Label lblServerSP;
		internal System.Windows.Forms.Label lblServer1;
		internal System.Windows.Forms.Label lblServerBR;
		internal System.Windows.Forms.Label lblServerNone;
		internal System.Windows.Forms.Label lblServerDB;
		internal System.Windows.Forms.ComboBox boxDataBits;
		internal System.Windows.Forms.Label lblServerP;
		internal System.Windows.Forms.ComboBox boxSpeed;
		internal System.Windows.Forms.Label lblServerSB;
		internal System.Windows.Forms.ComboBox boxSerialPort;
		internal System.Windows.Forms.ComboBox boxDoLogging;
		internal System.Windows.Forms.TextBox tbServerMountPoint;
		internal System.Windows.Forms.Label lblSerialStatus;
		internal System.Windows.Forms.Label Label3;
		internal System.Windows.Forms.Button btnSaveMountPoints;
		internal System.Windows.Forms.Button btnReloadMountPoints;
		internal System.Windows.Forms.Button btnReloadUsers;
		internal System.Windows.Forms.Button btnSaveUsers;
		internal System.Windows.Forms.RichTextBox rtbEvents;
        private System.ComponentModel.IContainer components;
    }
	
}
