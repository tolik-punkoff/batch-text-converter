using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace PacketTextConverter
{
    public partial class frmSelectEncodings : Form
    {        
        public appSettings Settings = null;

        bool isStandartDic = true;
        int SourceEnc = 0;
        int TargetEnc = 0;
        string sSourceEnc = "";
        string sTargetEnc = "";
        bool BOM = false;
        
        //флаг, показывающий, меняется ли список кодировок при загрузке формы, или это юзер щелкает
        private bool isFirstChange = true;
        public frmSelectEncodings()
        {
            InitializeComponent();
        }

        private void LoadingCP(ListBox lst,bool standart) //загружаем список кодировок
        {
            CommonFunctions.LoadEncodingDic(standart); //заполняем словарь кодировок
            lst.Items.Clear(); //очищаем Listbox
            foreach (KeyValuePair<string, int> kvp in CommonFunctions.dctEnc)
            {
                lst.Items.Add(kvp.Key);
            }            
        }
        

        private void frmSelectEncodings_Load(object sender, EventArgs e)
        {            
            SourceEnc = Settings.SourceEnc;
            TargetEnc = Settings.TargetEnc;
            BOM = Settings.BOM;
            isStandartDic = Settings.isStandartDict;

            sSourceEnc = CommonFunctions.GetEncName(SourceEnc);
            sTargetEnc = CommonFunctions.GetEncName(TargetEnc);
        }
        
        private void frmSelectEncodings_Shown(object sender, EventArgs e)
        {
            //загружаем кодировки в ListBox'ы
            LoadingCP(lstTargetEnc, isStandartDic);
            LoadingCP(lstSourceEnc, isStandartDic);
            isFirstChange = !isStandartDic; //если словарь стандартный CheckedChange будет, 
                                            //если пользователь кликнет и надо будет скорректировать индексы
                                            //иначе CheckedChange будет все равно, и не надо корректировать индексы
            //устанавливаем чекбокс кодировок 
            //(маленький или полный передает вызывающая форма)            
            chkAddCP.Checked = !isStandartDic;

            //выбираем установленный элемент ListBox
            if (sSourceEnc == "")
            {
                lstSourceEnc.SelectedIndex = 0;
            }
            else
            {
                lstSourceEnc.SelectedIndex = lstSourceEnc.Items.IndexOf(sSourceEnc);
            }
            //аналогично для 2 ListBox
            //выбираем установленный элемент ListBox
            if (sTargetEnc == "")
            {
                lstTargetEnc.SelectedIndex = 0;
            }
            else
            {
                lstTargetEnc.SelectedIndex = lstTargetEnc.Items.IndexOf(sTargetEnc);
            }
            //устанавливаем чекбокс BOM
            chkBOM.Checked = BOM;
        }

        private void chkAddCP_CheckedChanged(object sender, EventArgs e)
        {            
            LoadingCP(lstSourceEnc, !chkAddCP.Checked);
            LoadingCP(lstTargetEnc, !chkAddCP.Checked);
            isStandartDic = !chkAddCP.Checked;
            
            if (isFirstChange) //если это первое срабатывание события
            {
                //значит индексы установят при загрузке формы
                //и ничего делать не надо, только поменять флаг
                isFirstChange = false;
            }
            else
            {
                //иначе надо сбросить индексы
                lstTargetEnc.SelectedIndex = 0;
                lstSourceEnc.SelectedIndex = 0;
            }
        }

        private void lstSourceEnc_SelectedIndexChanged(object sender, EventArgs e)
        {
            //вытаскиваем ID выбранной кодировки из словаря и пишем в переменную
            CommonFunctions.dctEnc.TryGetValue(lstSourceEnc.Text, out SourceEnc);
            //отображаем пользователю
            lblEncStatus.ForeColor = Color.Black;
            lblEncStatus.Text = lstSourceEnc.Text + " --> " + lstTargetEnc.Text;                   
        }

        private void lstTargetEnc_SelectedIndexChanged(object sender, EventArgs e)
        {
            //вытаскиваем ID выбранной кодировки из словаря и пишем в переменную
            CommonFunctions.dctEnc.TryGetValue(lstTargetEnc.Text, out TargetEnc);
            //отображаем пользователю
            lblEncStatus.ForeColor = Color.Black;
            lblEncStatus.Text = lstSourceEnc.Text + " --> " + lstTargetEnc.Text;
            //если кодировка UTF8 включаем чекбокс добавить BOM
            if (TargetEnc != 65001)
            {
                chkBOM.Enabled = false;
            }
            else
            {
                chkBOM.Enabled = true;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (TargetEnc == 65001 && chkBOM.Checked) BOM = true; else BOM = false;

            if (SourceEnc == TargetEnc) //проверяем ошибку на совпадение кодировок
            {
                lblEncStatus.ForeColor = Color.Red;
                lblEncStatus.Text = "Исходная и целевая кодировка не могут совпадать.";
                return;
            }
            else //если все нормально
            {
                //записываем параметры в переменные
                Settings.SourceEnc = SourceEnc;
                Settings.TargetEnc = TargetEnc;                
                Settings.BOM = BOM;
                Settings.isStandartDict = isStandartDic;

                //сохраняем конфигурацию и закрываем окно
                if (!Settings.SaveConfig())
                {
                    CommonFunctions.ErrMessage(Settings.ConfigError);
                    return;
                }
                this.DialogResult = DialogResult.OK;
                this.Close();                
            }            
        }
        
    }
}
