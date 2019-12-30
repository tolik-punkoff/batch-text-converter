using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Collections;
using System.Windows.Forms;
using System.Diagnostics;

namespace PacketTextConverter
{
    public partial class frmAbout : Form
    {
        public frmAbout()
        {
            InitializeComponent();
        }

        AboutDrawer Drawer = null;

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmAbout_Load(object sender, EventArgs e)
        {
            
            this.BackColor = Color.Black;
            this.ForeColor = Color.Green;
            pctMessages.Location = new Point(
                (this.Width/2 - pctMessages.Width/2),
                pctMessages.Location.Y);


            Drawer = new AboutDrawer(pctMessages);
            Drawer.DefaultBackColor = Color.Black;
            Drawer.DefaultTextColor = Color.Lime;            

            if (!Drawer.LoadScript(Properties.Resources.about))
            {
                MessageBox.Show(Drawer.ErrorMessage, "Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
                return;
            }

            if (!Drawer.StartDraw())
            {
                MessageBox.Show(Drawer.ErrorMessage, "Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
                return;
            }
        }
        
                
        private void pctLogo_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start("http://tolik-punkoff.com");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
            }
        }
    }
}