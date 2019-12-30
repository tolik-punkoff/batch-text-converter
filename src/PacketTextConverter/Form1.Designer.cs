namespace PacketTextConverter
{
    partial class frmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.btnMasks = new System.Windows.Forms.Button();
            this.lblSourceCatalog = new System.Windows.Forms.Label();
            this.txtSourceCatalog = new System.Windows.Forms.TextBox();
            this.btnSelectSource = new System.Windows.Forms.Button();
            this.btnSelectTarget = new System.Windows.Forms.Button();
            this.txtTargetCatalog = new System.Windows.Forms.TextBox();
            this.lblTargetCatalog = new System.Windows.Forms.Label();
            this.chkRecursive = new System.Windows.Forms.CheckBox();
            this.btnDeleteBOM = new System.Windows.Forms.Button();
            this.btnRecode = new System.Windows.Forms.Button();
            this.btnAbout = new System.Windows.Forms.Button();
            this.btnSelectEncodings = new System.Windows.Forms.Button();
            this.btnClearLog = new System.Windows.Forms.Button();
            this.btnSaveLog = new System.Windows.Forms.Button();
            this.btnHelp = new System.Windows.Forms.Button();
            this.chkReplaceSource = new System.Windows.Forms.CheckBox();
            this.chkBackup = new System.Windows.Forms.CheckBox();
            this.lvLog = new PacketTextConverter.MyListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.dlgSaveLog = new System.Windows.Forms.SaveFileDialog();
            this.SuspendLayout();
            // 
            // btnMasks
            // 
            this.btnMasks.Location = new System.Drawing.Point(132, 3);
            this.btnMasks.Name = "btnMasks";
            this.btnMasks.Size = new System.Drawing.Size(124, 23);
            this.btnMasks.TabIndex = 5;
            this.btnMasks.Text = "Маски файлов";
            this.btnMasks.UseVisualStyleBackColor = true;
            this.btnMasks.Click += new System.EventHandler(this.btnMasks_Click);
            // 
            // lblSourceCatalog
            // 
            this.lblSourceCatalog.AutoSize = true;
            this.lblSourceCatalog.Location = new System.Drawing.Point(-1, 39);
            this.lblSourceCatalog.Name = "lblSourceCatalog";
            this.lblSourceCatalog.Size = new System.Drawing.Size(101, 13);
            this.lblSourceCatalog.TabIndex = 6;
            this.lblSourceCatalog.Text = "Исходный каталог";
            // 
            // txtSourceCatalog
            // 
            this.txtSourceCatalog.Location = new System.Drawing.Point(106, 37);
            this.txtSourceCatalog.Name = "txtSourceCatalog";
            this.txtSourceCatalog.ReadOnly = true;
            this.txtSourceCatalog.Size = new System.Drawing.Size(497, 20);
            this.txtSourceCatalog.TabIndex = 7;
            // 
            // btnSelectSource
            // 
            this.btnSelectSource.Location = new System.Drawing.Point(609, 34);
            this.btnSelectSource.Name = "btnSelectSource";
            this.btnSelectSource.Size = new System.Drawing.Size(75, 23);
            this.btnSelectSource.TabIndex = 8;
            this.btnSelectSource.Text = "Выбор";
            this.btnSelectSource.UseVisualStyleBackColor = true;
            this.btnSelectSource.Click += new System.EventHandler(this.btnSelectSource_Click);
            // 
            // btnSelectTarget
            // 
            this.btnSelectTarget.Location = new System.Drawing.Point(609, 60);
            this.btnSelectTarget.Name = "btnSelectTarget";
            this.btnSelectTarget.Size = new System.Drawing.Size(75, 23);
            this.btnSelectTarget.TabIndex = 11;
            this.btnSelectTarget.Text = "Выбор";
            this.btnSelectTarget.UseVisualStyleBackColor = true;
            this.btnSelectTarget.Click += new System.EventHandler(this.btnSelectTarget_Click);
            // 
            // txtTargetCatalog
            // 
            this.txtTargetCatalog.Location = new System.Drawing.Point(106, 63);
            this.txtTargetCatalog.Name = "txtTargetCatalog";
            this.txtTargetCatalog.ReadOnly = true;
            this.txtTargetCatalog.Size = new System.Drawing.Size(497, 20);
            this.txtTargetCatalog.TabIndex = 10;
            // 
            // lblTargetCatalog
            // 
            this.lblTargetCatalog.AutoSize = true;
            this.lblTargetCatalog.Location = new System.Drawing.Point(-1, 65);
            this.lblTargetCatalog.Name = "lblTargetCatalog";
            this.lblTargetCatalog.Size = new System.Drawing.Size(94, 13);
            this.lblTargetCatalog.TabIndex = 9;
            this.lblTargetCatalog.Text = "Целевой каталог";
            // 
            // chkRecursive
            // 
            this.chkRecursive.AutoSize = true;
            this.chkRecursive.Checked = true;
            this.chkRecursive.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkRecursive.Location = new System.Drawing.Point(106, 90);
            this.chkRecursive.Name = "chkRecursive";
            this.chkRecursive.Size = new System.Drawing.Size(137, 17);
            this.chkRecursive.TabIndex = 12;
            this.chkRecursive.Text = "Включая подкаталоги";
            this.chkRecursive.UseVisualStyleBackColor = true;
            this.chkRecursive.CheckedChanged += new System.EventHandler(this.chkRecursive_CheckedChanged);
            // 
            // btnDeleteBOM
            // 
            this.btnDeleteBOM.Location = new System.Drawing.Point(115, 359);
            this.btnDeleteBOM.Name = "btnDeleteBOM";
            this.btnDeleteBOM.Size = new System.Drawing.Size(107, 23);
            this.btnDeleteBOM.TabIndex = 14;
            this.btnDeleteBOM.Text = "Удалить BOM";
            this.btnDeleteBOM.UseVisualStyleBackColor = true;
            this.btnDeleteBOM.Click += new System.EventHandler(this.btnDeleteBOM_Click);
            // 
            // btnRecode
            // 
            this.btnRecode.Location = new System.Drawing.Point(2, 359);
            this.btnRecode.Name = "btnRecode";
            this.btnRecode.Size = new System.Drawing.Size(107, 23);
            this.btnRecode.TabIndex = 15;
            this.btnRecode.Text = "Перекодировать";
            this.btnRecode.UseVisualStyleBackColor = true;
            this.btnRecode.Click += new System.EventHandler(this.btnRecode_Click);
            // 
            // btnAbout
            // 
            this.btnAbout.Location = new System.Drawing.Point(577, 3);
            this.btnAbout.Name = "btnAbout";
            this.btnAbout.Size = new System.Drawing.Size(107, 23);
            this.btnAbout.TabIndex = 16;
            this.btnAbout.Text = "О программе...";
            this.btnAbout.UseVisualStyleBackColor = true;
            this.btnAbout.Click += new System.EventHandler(this.btnAbout_Click);
            // 
            // btnSelectEncodings
            // 
            this.btnSelectEncodings.Location = new System.Drawing.Point(2, 4);
            this.btnSelectEncodings.Name = "btnSelectEncodings";
            this.btnSelectEncodings.Size = new System.Drawing.Size(124, 23);
            this.btnSelectEncodings.TabIndex = 18;
            this.btnSelectEncodings.Text = "Выбрать кодировки";
            this.btnSelectEncodings.UseVisualStyleBackColor = true;
            this.btnSelectEncodings.Click += new System.EventHandler(this.btnSelectEncodings_Click);
            // 
            // btnClearLog
            // 
            this.btnClearLog.Location = new System.Drawing.Point(262, 3);
            this.btnClearLog.Name = "btnClearLog";
            this.btnClearLog.Size = new System.Drawing.Size(124, 23);
            this.btnClearLog.TabIndex = 19;
            this.btnClearLog.Text = "Очистить протокол";
            this.btnClearLog.UseVisualStyleBackColor = true;
            this.btnClearLog.Click += new System.EventHandler(this.btnClearLog_Click);
            // 
            // btnSaveLog
            // 
            this.btnSaveLog.Location = new System.Drawing.Point(392, 3);
            this.btnSaveLog.Name = "btnSaveLog";
            this.btnSaveLog.Size = new System.Drawing.Size(124, 23);
            this.btnSaveLog.TabIndex = 20;
            this.btnSaveLog.Text = "Сохранить протокол";
            this.btnSaveLog.UseVisualStyleBackColor = true;
            this.btnSaveLog.Click += new System.EventHandler(this.btnSaveLog_Click);
            // 
            // btnHelp
            // 
            this.btnHelp.Location = new System.Drawing.Point(577, 359);
            this.btnHelp.Name = "btnHelp";
            this.btnHelp.Size = new System.Drawing.Size(107, 23);
            this.btnHelp.TabIndex = 21;
            this.btnHelp.Text = "Помощь...";
            this.btnHelp.UseVisualStyleBackColor = true;
            this.btnHelp.Click += new System.EventHandler(this.btnHelp_Click);
            // 
            // chkReplaceSource
            // 
            this.chkReplaceSource.AutoSize = true;
            this.chkReplaceSource.Location = new System.Drawing.Point(249, 90);
            this.chkReplaceSource.Name = "chkReplaceSource";
            this.chkReplaceSource.Size = new System.Drawing.Size(202, 17);
            this.chkReplaceSource.TabIndex = 22;
            this.chkReplaceSource.Text = "Перезаписывать исходные файлы";
            this.chkReplaceSource.UseVisualStyleBackColor = true;
            this.chkReplaceSource.CheckedChanged += new System.EventHandler(this.chkReplaceSource_CheckedChanged);
            // 
            // chkBackup
            // 
            this.chkBackup.AutoSize = true;
            this.chkBackup.Location = new System.Drawing.Point(457, 90);
            this.chkBackup.Name = "chkBackup";
            this.chkBackup.Size = new System.Drawing.Size(114, 17);
            this.chkBackup.TabIndex = 23;
            this.chkBackup.Text = "Резервная копия";
            this.chkBackup.UseVisualStyleBackColor = true;
            this.chkBackup.CheckedChanged += new System.EventHandler(this.chkBackup_CheckedChanged);
            // 
            // lvLog
            // 
            this.lvLog.BackColor = System.Drawing.Color.Black;
            this.lvLog.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.lvLog.Location = new System.Drawing.Point(2, 113);
            this.lvLog.Name = "lvLog";
            this.lvLog.Size = new System.Drawing.Size(682, 240);
            this.lvLog.TabIndex = 0;
            this.lvLog.UseCompatibleStateImageBehavior = false;
            this.lvLog.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "";
            this.columnHeader1.Width = 678;
            // 
            // dlgSaveLog
            // 
            this.dlgSaveLog.FileName = "packetconverter_log.txt";
            this.dlgSaveLog.Filter = "Text files|*.txt";
            this.dlgSaveLog.Title = "Save log";
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(686, 385);
            this.Controls.Add(this.chkBackup);
            this.Controls.Add(this.chkReplaceSource);
            this.Controls.Add(this.btnHelp);
            this.Controls.Add(this.btnSaveLog);
            this.Controls.Add(this.btnClearLog);
            this.Controls.Add(this.btnSelectEncodings);
            this.Controls.Add(this.btnAbout);
            this.Controls.Add(this.btnRecode);
            this.Controls.Add(this.btnDeleteBOM);
            this.Controls.Add(this.chkRecursive);
            this.Controls.Add(this.btnSelectTarget);
            this.Controls.Add(this.txtTargetCatalog);
            this.Controls.Add(this.lblTargetCatalog);
            this.Controls.Add(this.btnSelectSource);
            this.Controls.Add(this.txtSourceCatalog);
            this.Controls.Add(this.lblSourceCatalog);
            this.Controls.Add(this.btnMasks);
            this.Controls.Add(this.lvLog);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Пакетный конвертер текста";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private PacketTextConverter.MyListView lvLog;
        private System.Windows.Forms.Button btnMasks;
        private System.Windows.Forms.Label lblSourceCatalog;
        private System.Windows.Forms.TextBox txtSourceCatalog;
        private System.Windows.Forms.Button btnSelectSource;
        private System.Windows.Forms.Button btnSelectTarget;
        private System.Windows.Forms.TextBox txtTargetCatalog;
        private System.Windows.Forms.Label lblTargetCatalog;
        private System.Windows.Forms.CheckBox chkRecursive;
        private System.Windows.Forms.Button btnDeleteBOM;
        private System.Windows.Forms.Button btnRecode;
        private System.Windows.Forms.Button btnAbout;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.Button btnSelectEncodings;
        private System.Windows.Forms.Button btnClearLog;
        private System.Windows.Forms.Button btnSaveLog;
        private System.Windows.Forms.Button btnHelp;
        private System.Windows.Forms.CheckBox chkReplaceSource;
        private System.Windows.Forms.CheckBox chkBackup;
        private System.Windows.Forms.SaveFileDialog dlgSaveLog;
    }
}

