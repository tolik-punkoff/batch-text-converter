namespace PacketTextConverter
{
    partial class frmSelectEncodings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSelectEncodings));
            this.lblIz = new System.Windows.Forms.Label();
            this.lstSourceEnc = new System.Windows.Forms.ListBox();
            this.lstTargetEnc = new System.Windows.Forms.ListBox();
            this.lblV = new System.Windows.Forms.Label();
            this.chkAddCP = new System.Windows.Forms.CheckBox();
            this.chkBOM = new System.Windows.Forms.CheckBox();
            this.lblEncStatus = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblIz
            // 
            this.lblIz.AutoSize = true;
            this.lblIz.Location = new System.Drawing.Point(3, 9);
            this.lblIz.Name = "lblIz";
            this.lblIz.Size = new System.Drawing.Size(78, 13);
            this.lblIz.TabIndex = 0;
            this.lblIz.Text = "Из кодировки";
            // 
            // lstSourceEnc
            // 
            this.lstSourceEnc.FormattingEnabled = true;
            this.lstSourceEnc.Location = new System.Drawing.Point(6, 26);
            this.lstSourceEnc.Name = "lstSourceEnc";
            this.lstSourceEnc.Size = new System.Drawing.Size(306, 147);
            this.lstSourceEnc.TabIndex = 1;
            this.lstSourceEnc.SelectedIndexChanged += new System.EventHandler(this.lstSourceEnc_SelectedIndexChanged);
            // 
            // lstTargetEnc
            // 
            this.lstTargetEnc.FormattingEnabled = true;
            this.lstTargetEnc.Location = new System.Drawing.Point(318, 26);
            this.lstTargetEnc.Name = "lstTargetEnc";
            this.lstTargetEnc.Size = new System.Drawing.Size(306, 147);
            this.lstTargetEnc.TabIndex = 3;
            this.lstTargetEnc.SelectedIndexChanged += new System.EventHandler(this.lstTargetEnc_SelectedIndexChanged);
            // 
            // lblV
            // 
            this.lblV.AutoSize = true;
            this.lblV.Location = new System.Drawing.Point(315, 9);
            this.lblV.Name = "lblV";
            this.lblV.Size = new System.Drawing.Size(69, 13);
            this.lblV.TabIndex = 2;
            this.lblV.Text = "в кодировку";
            // 
            // chkAddCP
            // 
            this.chkAddCP.AutoSize = true;
            this.chkAddCP.Location = new System.Drawing.Point(6, 179);
            this.chkAddCP.Name = "chkAddCP";
            this.chkAddCP.Size = new System.Drawing.Size(213, 17);
            this.chkAddCP.TabIndex = 19;
            this.chkAddCP.Text = "Дополнительные кодовые страницы";
            this.chkAddCP.UseVisualStyleBackColor = true;
            this.chkAddCP.CheckedChanged += new System.EventHandler(this.chkAddCP_CheckedChanged);
            // 
            // chkBOM
            // 
            this.chkBOM.AutoSize = true;
            this.chkBOM.Location = new System.Drawing.Point(6, 202);
            this.chkBOM.Name = "chkBOM";
            this.chkBOM.Size = new System.Drawing.Size(526, 17);
            this.chkBOM.TabIndex = 20;
            this.chkBOM.Text = "При перекодировке в UTF-8 добавить BOM (в остальных случаях данный флажок не учит" +
                "ывается)";
            this.chkBOM.UseVisualStyleBackColor = true;
            // 
            // lblEncStatus
            // 
            this.lblEncStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblEncStatus.Location = new System.Drawing.Point(6, 226);
            this.lblEncStatus.Name = "lblEncStatus";
            this.lblEncStatus.Size = new System.Drawing.Size(618, 67);
            this.lblEncStatus.TabIndex = 21;
            this.lblEncStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(6, 316);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(85, 23);
            this.btnOK.TabIndex = 22;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(539, 316);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(85, 23);
            this.btnCancel.TabIndex = 23;
            this.btnCancel.Text = "Отмена";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // frmSelectEncodings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(631, 342);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.lblEncStatus);
            this.Controls.Add(this.chkBOM);
            this.Controls.Add(this.chkAddCP);
            this.Controls.Add(this.lstTargetEnc);
            this.Controls.Add(this.lblV);
            this.Controls.Add(this.lstSourceEnc);
            this.Controls.Add(this.lblIz);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSelectEncodings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Выбрать кодировку";
            this.Load += new System.EventHandler(this.frmSelectEncodings_Load);
            this.Shown += new System.EventHandler(this.frmSelectEncodings_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblIz;
        private System.Windows.Forms.ListBox lstSourceEnc;
        private System.Windows.Forms.ListBox lstTargetEnc;
        private System.Windows.Forms.Label lblV;
        private System.Windows.Forms.CheckBox chkAddCP;
        private System.Windows.Forms.CheckBox chkBOM;
        private System.Windows.Forms.Label lblEncStatus;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
    }
}