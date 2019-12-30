using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace PacketTextConverter
{
    class CommonFunctions
    {
        //public static Dictionary<string,bool> dctMasks = new Dictionary<string,bool>(); //содержит маски файлов
        public static Dictionary<string, Int32> dctEnc = new Dictionary<string, int>(); //словарь кодировок
        public  static string SettingsPath = "";
        public  static string SettingsFile = SettingsPath + "config.xml";        

        static CommonFunctions()
        {
            LoadEncodingDic(true);
        }

        public static void LoadEncodingDic(bool standart) //загрузка словаря кодовых страниц
        {
            dctEnc.Clear(); //очищаем словарь кодировок

            if (standart) //минимальный словарь кодировок
            {
                //создаем список самых используемых кодировок                
                dctEnc.Add(GetEncName(1251), 1251); //win
                dctEnc.Add(GetEncName(65001), 65001); //utf8
                dctEnc.Add(GetEncName(866), 866); //dos
                dctEnc.Add(GetEncName(10007), 10007); //mac
                dctEnc.Add(GetEncName(20866), 20866); //koi8-r
                dctEnc.Add(GetEncName(21866), 21866); //koi8-u
            }
            else
            {
                //и всякий зоопарк
                foreach (EncodingInfo ei in Encoding.GetEncodings())
                {                    
                    dctEnc.Add(ei.DisplayName + " (" + ei.CodePage.ToString() + ")", ei.CodePage);
                }
            }
        }

        public static void ErrMessage(string stMessage)
        {
            MessageBox.Show(stMessage, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        
        public static string AddSlash(string st)
        {
            if (st.EndsWith("\\"))
            {
                return st;
            }

            return st + "\\";
        }

        public static string  GetEncName(int codepage) //получает имя кодировки из кодовой страницы
        {
            if (codepage == 0) return String.Empty;

            return Encoding.GetEncoding(codepage).EncodingName;
        }

        public static string SaveTextFile(string path, string content)
        {
            try
            {
                File.WriteAllText(path,content,Encoding.GetEncoding(1251));
                return String.Empty;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

    }
}
