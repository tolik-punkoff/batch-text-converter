using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Reflection;
using System.IO;

namespace PacketTextConverter
{
    
    public class appSettings
    {
        public string SourceData { get; set; }
        public string TargetData { get; set; }
        public bool BOM { get; set; }
        public bool Recursive { get; set; }
        public bool isStandartDict { get; set; }
        public bool ReplaceSource { get; set; }
        public bool Backup { get; set; }
        public int SourceEnc { get; set; }
        public int TargetEnc { get; set; }

        public string ConfigError { get; private set; }
        
        private string TableName = "";
        private string configFile = "";
        private DataSet dsConfig = new DataSet();        

        public appSettings(string filename)
        {
            configFile = filename;
            TableName = this.GetType().Name;
            CreateDataSet();
            string s = Path.GetDirectoryName(filename);
            try
            {
                Directory.CreateDirectory(s);
            }
            catch { }
            
            SourceData = "";
            TargetData = "";        
            BOM = false;
            isStandartDict = true;
            SourceEnc = 0;
            TargetEnc = 0;
            Recursive = true;
            ReplaceSource = false;
            Backup = false;
        }        

        public bool LoadConfig()
        {
            //файла нет, ок значения по умолчанию
            if (!File.Exists(configFile))
            {
                return true;
            }

            //почистим таблицы DataSet перед загрузкой
            foreach (DataTable table in dsConfig.Tables)
            {
                table.Rows.Clear();
            }

            //файл есть, пробуем загрузить в DataSet
            try
            {
                dsConfig.ReadXml(configFile);
            }
            catch (Exception ex)
            {
                ConfigError = ex.Message;
                return false;
            }

            //загрузка полей класса из DataSet
            if (dsConfig.Tables[TableName].Rows.Count > 0)
            {
                PropertyInfo[] properties = this.GetType().GetProperties();
                foreach (PropertyInfo pr in properties)
                {
                    string propName = pr.Name;
                    object propValue = dsConfig.Tables[TableName].Rows[0][propName];
                    if (propValue.GetType() != typeof(System.DBNull))
                    {
                        pr.SetValue(this, propValue, null);
                    }
                }                
            }                        
            return true;
        }

        public bool SaveConfig()
        {         
            ConfigError = null;
            
            dsConfig.Tables[TableName].Rows.Clear();
            DataRow dr = dsConfig.Tables[TableName].NewRow();


            PropertyInfo[] properties = this.GetType().GetProperties();
            foreach (PropertyInfo pr in properties)
            {
                string propName = pr.Name;
                object propValue = pr.GetValue(this, null);
                dr[propName] = propValue;
            }

            dsConfig.Tables[TableName].Rows.Add(dr);

            try
            {
                dsConfig.WriteXml(configFile);
            }
            catch (Exception ex)
            {
                ConfigError = ex.Message;
                return false;
            }

            return true;
        }

        private void CreateDataSet()
        {
            dsConfig.Tables.Add(TableName);

            PropertyInfo[] properties = this.GetType().GetProperties();

            foreach (PropertyInfo pr in properties)
            {
                dsConfig.Tables[TableName].Columns.Add(pr.Name, pr.PropertyType);
            }

            //добавляем таблицу масок
            dsConfig.Tables.Add("tblMasks");
            dsConfig.Tables["tblMasks"].Columns.Add("Mask", typeof(string));
            dsConfig.Tables["tblMasks"].Columns.Add("Enabled", typeof(bool));
            dsConfig.Tables["tblMasks"].Columns["Mask"].Unique = true;
        }

        public void ClearMasks()
        {
            dsConfig.Tables["tblMasks"].Clear();
        }

        public Dictionary<string, bool> GetAllMasks()
        {
            Dictionary<string, bool> AllMasks = new Dictionary<string, bool>();

            foreach (DataRow dr in dsConfig.Tables["tblMasks"].Rows)
            {
                AllMasks.Add(dr["Mask"].ToString(), Convert.ToBoolean(dr["Enabled"]));
            }

            return AllMasks;
        }

        public void SaveAllMasks(Dictionary<string, bool> dctMasks)
        {
            ClearMasks();
            foreach (KeyValuePair<string, bool> kvp in dctMasks)
            {
                dsConfig.Tables["tblMasks"].Rows.Add(kvp.Key, kvp.Value);
            }
        }
    }
}
