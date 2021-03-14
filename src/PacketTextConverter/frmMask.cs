using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace PacketTextConverter
{
    public partial class frmMask : Form
    {
        public frmMask()
        {
            InitializeComponent();
        }

        public appSettings Settings = null;
        Dictionary<string, bool> dctMasks = null;

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string mask = txtMask.Text.Trim();
            if (mask != "")
            {
                mask = mask.ToLower();
                if (lstMasks.Items.Contains(mask))
                {
                    MessageBox.Show("Маска " + mask + " уже добавлена", "Ошибка добавления маски",
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                lstMasks.Items.Add(txtMask.Text.Trim(), true);
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            dctMasks.Clear();
            for (int i = 0; i < lstMasks.Items.Count;i++)
            {
                string mask = lstMasks.Items[i].ToString();
                dctMasks.Add(lstMasks.Items[i].ToString(), lstMasks.GetItemChecked(i));                
            }
            
            Settings.SaveAllMasks(dctMasks);
            if (!Settings.SaveConfig())
            {
                CommonFunctions.ErrMessage(Settings.ConfigError);
                return;
            }
            this.Close();
        }

        private void frmMask_Load(object sender, EventArgs e)
        {
            dctMasks = new Dictionary<string, bool>();
            dctMasks = Settings.GetAllMasks();

            foreach (KeyValuePair<string, bool> kvp in dctMasks)
            {
                lstMasks.Items.Add(kvp.Key, kvp.Value);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (lstMasks.SelectedIndex != -1)
            {
                lstMasks.Items.RemoveAt(lstMasks.SelectedIndex);
            }
        }
    }
}
