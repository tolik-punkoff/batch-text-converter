using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace PacketTextConverter
{
    public partial class frmReadme : Form
    {
        public frmReadme()
        {
            InitializeComponent();
        }

        private void frmReadme_Load(object sender, EventArgs e)
        {
            txtHelp.Text = Properties.Resources.readme;            
        }

        private void frmReadme_Shown(object sender, EventArgs e)
        {
            txtHelp.SelectionLength = 0;
        }
    }
}
