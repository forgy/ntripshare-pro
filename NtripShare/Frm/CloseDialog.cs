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

    public partial class CloseDialog
    {
        public CloseDialog()
        {
            InitializeComponent();

            //Added to support default instance behavour in C#
            if (defaultInstance == null)
                defaultInstance = this;
        }

        #region Default Instance

        private static CloseDialog defaultInstance;
        public string userName { get; set; }
        public string password { get; set; }
        public DateTime time { get; set; }

        /// <summary>
        /// Added by the VB.Net to C# Converter to support default instance behavour in C#
        /// </summary>
        public static CloseDialog Default
        {
            get
            {
                if (defaultInstance == null)
                {
                    defaultInstance = new CloseDialog();
                    defaultInstance.FormClosed += new FormClosedEventHandler(defaultInstance_FormClosed);
                }

                return defaultInstance;
            }
            set
            {
                defaultInstance = value;
            }
        }

        static void defaultInstance_FormClosed(object sender, FormClosedEventArgs e)
        {
            defaultInstance = null;
        }

        #endregion

        public void OK_Button_Click(System.Object sender, System.EventArgs e)
        {
            if (tbPassword.Text.Trim().Length == 0)
            {
                MessageBox.Show("Password is empty");
                return;
            }

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
           
        }

        public void Cancel_Button_Click(System.Object sender, System.EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }

}
