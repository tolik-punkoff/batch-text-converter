using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading;

namespace PacketTextConverter
{
    public partial class frmMain : Form
    {                
        //создаем рабочий класс
        cWork mainWorker = new cWork();
        appSettings Settings = null;
        public frmMain()
        {
            InitializeComponent();
        }
        

        private void frmMain_Load(object sender, EventArgs e)
        {                                    
            //создаем события
            mainWorker.StateChanged += new LogState(mainWorker_StateChanged);
            mainWorker.GlobalStateChanged += new GlobalLogState(mainWorker_GlobalStateChanged);
            //загружаем конфигурацию 
            Settings = new appSettings(CommonFunctions.SettingsFile);
            if (!Settings.LoadConfig())
            {
                CommonFunctions.ErrMessage(Settings.ConfigError);
            }
            //присваиваем данные конфига полям формы
            txtSourceCatalog.Text = Settings.SourceData;
            txtTargetCatalog.Text = Settings.TargetData;
            chkRecursive.Checked = Settings.Recursive;
            chkReplaceSource.Checked = Settings.ReplaceSource;
            chkBackup.Checked = Settings.Backup;
            
            chkReplaceSource_CheckedChanged(null, null);
        }

        void mainWorker_GlobalStateChanged(string logdata, OperationState result)
        {
            Invoke((MethodInvoker)delegate
            {
                lvLog.Items.Add(logdata);
                lvLog.EnsureVisible(lvLog.Items.Count - 1);

                //рисуем красивости для отображения глобальных операций                
                if (result == OperationState.Success) //зелененький если все хорошо
                {
                    lvLog.Items[lvLog.Items.Count - 1].ForeColor = Color.Lime;
                }
                else //и красненький если все плохо
                {
                    lvLog.Items[lvLog.Items.Count - 1].ForeColor = Color.Red;
                }
            });
        }

        void mainWorker_StateChanged(string logdata, OperationState logstate)
        {
            Invoke((MethodInvoker)delegate
            {
                lvLog.Items.Add(logdata);
                lvLog.EnsureVisible(lvLog.Items.Count - 1);

                //рисуем красивости для отображения маленьких операций 
                switch (logstate)
                {
                    case OperationState.Success: lvLog.Items[lvLog.Items.Count - 1].ForeColor = Color.Green; break; //ok
                    case OperationState.IOError: lvLog.Items[lvLog.Items.Count - 1].ForeColor = Color.Yellow; break; //err
                    case OperationState.Skip: lvLog.Items[lvLog.Items.Count - 1].ForeColor = Color.Gray; break; //skip
                }

            });
        }

        private void btnMasks_Click(object sender, EventArgs e)
        {
            frmMask fMask = new frmMask();
            fMask.Settings = Settings;
            fMask.ShowDialog();
        }

        private void btnSelectSource_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.SelectedPath = txtSourceCatalog.Text;
            fbd.ShowNewFolderButton = false;
            DialogResult dr = fbd.ShowDialog();
            if (dr == DialogResult.OK)
            {
                if (fbd.SelectedPath != txtTargetCatalog.Text)
                {
                    txtSourceCatalog.Text = fbd.SelectedPath;
                    Settings.SourceData = txtSourceCatalog.Text;
                    if (!Settings.SaveConfig())
                    {
                        CommonFunctions.ErrMessage(Settings.ConfigError);
                        return;
                    }
                }
                else
                {                    
                    lvLog.Items.Add("Исходный и целевой каталог не должны совпадать");
                    lvLog.EnsureVisible(lvLog.Items.Count - 1);
                    lvLog.Items[lvLog.Items.Count - 1].ForeColor = Color.Red;
                }
            }
        }

        private void btnSelectTarget_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.SelectedPath = txtSourceCatalog.Text;
            DialogResult dr = fbd.ShowDialog();
            if (dr == DialogResult.OK)
            {
                if (fbd.SelectedPath != txtSourceCatalog.Text)
                {
                    txtTargetCatalog.Text = fbd.SelectedPath;
                    Settings.TargetData = txtTargetCatalog.Text;
                    if (!Settings.SaveConfig())
                    {
                        CommonFunctions.ErrMessage(Settings.ConfigError);
                        return;
                    }
                }
                else
                {                    
                    lvLog.Items.Add("Исходный и целевой каталог не должны совпадать");
                    lvLog.EnsureVisible(lvLog.Items.Count - 1);
                    lvLog.Items[lvLog.Items.Count - 1].ForeColor = Color.Red;
                }
            }
        }

        private void chkRecursive_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Recursive = chkRecursive.Checked;
            if (!Settings.SaveConfig())
            {
                CommonFunctions.ErrMessage(Settings.ConfigError);
            }
        }
        
        private void chkReplaceSource_CheckedChanged(object sender, EventArgs e)
        {
            Settings.ReplaceSource = chkReplaceSource.Checked;
            if (!Settings.SaveConfig())
            {
                CommonFunctions.ErrMessage(Settings.ConfigError);
            }
            if (chkReplaceSource.Checked)
            {

                chkBackup.Enabled = true;
                btnSelectTarget.Enabled = false;
                txtTargetCatalog.Enabled = false;
            }
            else
            {
                chkBackup.Enabled = false;
                btnSelectTarget.Enabled = true;
                txtTargetCatalog.Enabled = true;
            }
        }

        private void chkBackup_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Backup = chkBackup.Checked;
            if (!Settings.SaveConfig())
            {
                CommonFunctions.ErrMessage(Settings.ConfigError);
            }
        }

        private void btnRecode_Click(object sender, EventArgs e)
        {
            mainWorker.ClearMasks(); //очищаем все маски
            if (!Settings.LoadConfig()) //загружаем конфигурацию
            {
                lvLog.Items.Add(Settings.ConfigError);
                lvLog.EnsureVisible(lvLog.Items.Count - 1);
                lvLog.Items[lvLog.Items.Count - 1].ForeColor = Color.Red;
                CommonFunctions.ErrMessage(Settings.ConfigError);
                return;
            }
            Dictionary<string, bool> dctMasks = Settings.GetAllMasks();
            foreach (KeyValuePair<string, bool> kvp in dctMasks)
            {
                if (kvp.Value) //если маска активна, то передаем ее рабочему классу
                {
                    mainWorker.AddMask(kvp.Key);
                }
            }
            
            //передаем остальные параметры
            mainWorker.SourceEnc = Settings.SourceEnc;
            mainWorker.TargetEnc = Settings.TargetEnc;
            mainWorker.SourceCatalog = Settings.SourceData;
            mainWorker.TargetCatalog = Settings.TargetData;
            mainWorker.BOM = Settings.BOM;
            mainWorker.Recursive = Settings.Recursive;
            mainWorker.ReplaceSource = Settings.ReplaceSource;
            mainWorker.Backup = Settings.Backup;

            if ((mainWorker.SourceEnc == 0) || (mainWorker.TargetEnc == 0))
            {                
                lvLog.Items.Add("Не выбраны кодировки!");
                lvLog.EnsureVisible(lvLog.Items.Count - 1);
                lvLog.Items[lvLog.Items.Count - 1].ForeColor = Color.Red;
                return;
            }

            string msg = "Будет произведена перекодировка текстовых файлов из " 
                 + CommonFunctions.GetEncName(Settings.SourceEnc) + " в " 
                 + CommonFunctions.GetEncName(Settings.TargetEnc) + "\n" +
                 "Внимание! Проверка на соответствие файлов выбранной целевой кодировке не "
                 + "производится, убедитесь, что ваши файлы действительно находятся в нужной кодировке";

            if (Settings.ReplaceSource)
            {
                msg = msg + "\n\n" +
                    "ВНИМАНИЕ! Вы собираетесь перезаписать файлы в целевом каталоге!";

                if (Settings.Backup)
                {
                    msg = msg + "\n\n" +
                     "Будут созданы резервные копии файлов (с расширением *.enc.bak).";
                }
                else
                {
                    msg = msg + "\n\n" +
                     "ВНИМАНИЕ! У вас не установлена опция резервного копирования! \n" +
                     "Настоятельно рекомендуем сделать резервную копию исходного каталога" +
                    "вручную, или установить опцию резервного копирования!\n" +
                    "В случае повреждения файлы могут быть утеряны!";
                }
            }            
            
            DialogResult dr = MessageBox.Show(msg, "Приступить?", 
                MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

            if (dr == DialogResult.Cancel) return;
                        
            //запускаем отдельный поток с рабочим процессом
            Thread thread = new Thread(mainWorker.StartBatchRecoding);
            thread.Start();
        }
        
        private void btnSelectEncodings_Click(object sender, EventArgs e)
        {            
            //установка кодировок
            frmSelectEncodings fse = new frmSelectEncodings(); //создаем
            fse.Settings = Settings;
            fse.ShowDialog(); //и показываем соотв. форму            
        }

        private void btnDeleteBOM_Click(object sender, EventArgs e)
        {
            string msg = "Будет удален BOM (UTF-8). Продолжить?";

            if (Settings.ReplaceSource)
            {
                msg = msg + "\n\n" +
                    "ВНИМАНИЕ! Вы собираетесь перезаписать файлы в целевом каталоге!";

                if (Settings.Backup)
                {
                    msg = msg + "\n\n" +
                     "Будут созданы резервные копии файлов (с расширением *.rem.bak).";
                }
                else
                {
                    msg = msg + "\n\n" +
                     "ВНИМАНИЕ! У вас не установлена опция резервного копирования! \n" +
                     "Настоятельно рекомендуем сделать резервную копию исходного каталога" +
                    "вручную, или установить опцию резервного копирования!\n" +
                    "В случае повреждения файлы могут быть утеряны!";
                }
            }

            DialogResult dr = MessageBox.Show(msg,
                "Приступить?", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            
            if (dr == DialogResult.Cancel) return;

            mainWorker.ClearMasks(); //очищаем все маски
            if (!Settings.LoadConfig()) //загружаем конфигурацию
            {
                lvLog.Items.Add(Settings.ConfigError);
                lvLog.EnsureVisible(lvLog.Items.Count - 1);
                lvLog.Items[lvLog.Items.Count - 1].ForeColor = Color.Red;
                CommonFunctions.ErrMessage(Settings.ConfigError);
                return;
            }
            Dictionary<string, bool> dctMasks = Settings.GetAllMasks();
            foreach (KeyValuePair<string, bool> kvp in dctMasks)
            {
                if (kvp.Value) //если маска активна, то передаем ее рабочему классу
                {
                    mainWorker.AddMask(kvp.Key);
                }
            }
            //другие настройки...
            mainWorker.SourceCatalog = Settings.SourceData;
            mainWorker.TargetCatalog = Settings.TargetData;
            mainWorker.Recursive = Settings.Recursive;
            mainWorker.ReplaceSource = Settings.ReplaceSource;
            mainWorker.Backup = Settings.Backup;

            //запускаем отдельный поток с рабочим процессом
            Thread thread = new Thread(mainWorker.StartBatchKillBOM);
            thread.Start();
        }

        private void btnClearLog_Click(object sender, EventArgs e)
        {
            lvLog.Items.Clear();
        }

        private void btnSaveLog_Click(object sender, EventArgs e)
        {                        
            string log = "";

            if (lvLog.Items.Count == 0) return;

            DialogResult Ans = dlgSaveLog.ShowDialog();
            if (Ans == DialogResult.Cancel) return;
           
            for (int i = 0; i < lvLog.Items.Count;i++)
            {
                log = log + lvLog.Items[i].Text + "\n";
            }
            string result = CommonFunctions.SaveTextFile(dlgSaveLog.FileName, log);
            if (result == "")
            {
                lvLog.Items.Add("Сохранено в " + dlgSaveLog.FileName);
                lvLog.EnsureVisible(lvLog.Items.Count - 1);
                lvLog.Items[lvLog.Items.Count - 1].ForeColor = Color.Lime;
            }
            else
            {
                lvLog.Items.Add(result + dlgSaveLog.FileName);
                lvLog.EnsureVisible(lvLog.Items.Count - 1);
                lvLog.Items[lvLog.Items.Count - 1].ForeColor = Color.Red;
            }
        }

        private void btnAbout_Click(object sender, EventArgs e)
        {
            frmAbout fAbout = new frmAbout();
            fAbout.ShowDialog();
        }

        private void btnHelp_Click(object sender, EventArgs e)
        {
            frmReadme fReadme = new frmReadme();
            fReadme.ShowDialog();
        }
    }
}
