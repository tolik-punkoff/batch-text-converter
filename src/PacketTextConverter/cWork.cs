using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.IO;
using FindFilesByMask;

namespace PacketTextConverter
{
    public delegate void LogState(string logdata, OperationState  logstate); //для статуса небольших операций
    public delegate void GlobalLogState(string logdata, OperationState result); //для статусов глобальных операций

    public enum OperationState
    {
        Success = 0,
        IOError = 1,
        Skip = 2
    }
    
    public enum BOMDetect
    {
        BOMExist = 0,
        BOMNotExist = 1,
        FileError = 2
    }

    class cWork
    {             
        private ArrayList FoundFiles = new ArrayList(); //для хранения имен найденных файлов
        private ArrayList FileMasks = new ArrayList(); //Маски искомых файлов        
        public bool Recursive { get; set; } //рекурсивный поиск
        public string SourceCatalog { get; set; } //исходный каталог
        public string TargetCatalog { get; set; } //целевой каталог
        public Int32 SourceEnc { get; set; } //исходная кодировка
        public Int32 TargetEnc { get; set; } //целевая кодировка
        public bool BOM { get; set; } //добавлять BOM?
        public bool ReplaceSource { get; set; } //переписать файлы в исходном каталоге
        public bool Backup { get; set; } //создать бэкап

        //события
        public event LogState StateChanged; //статус маленькой операции 
        public event GlobalLogState GlobalStateChanged; //статус глобальной операции
        
        private int RecodingErrorCount = 0; //счетчик ошибок при перекодировке файлов/удалении BOM
        private int SkipCount = 0;          //счетчик пропущенных файлов
        private string ErrorMessage = "";

        public cWork()
        {
            //установка параметров по умолчанию
            Recursive = true; //рекурсивный поиск
            SourceCatalog = ""; //исходный каталог
            TargetCatalog = ""; //целевой каталог
            SourceEnc = 0; //исходная кодировка
            TargetEnc = 0; //целевая кодировка
            BOM = false; //добавлять BOM?
            ReplaceSource = false; //переписать файлы в исходном каталоге
            Backup = false; //создать бэкап
        }

        private void LogStateAdd(string stMessage, OperationState StateCode) //добавляет в лог и меняет статус маленькой операции
        {
            DateTime dt = new DateTime();
            dt = DateTime.Now;
            stMessage  = "[" + dt.ToString() + "]:" + stMessage;                        
            StateChanged(stMessage, StateCode);            
        }

        private void GlobalLogStateAdd(string stMessage, OperationState StateCode) //добавляет в лог и меняет статус маленькой операции
        {
            DateTime dt = new DateTime();
            dt = DateTime.Now;
            stMessage = "[" + dt.ToString() + "]:" + stMessage;
            GlobalStateChanged(stMessage, StateCode);            
        }
        

        private OperationState FindFilesMask(string stPath, string stMask, bool bRecurs) //ищет файлы 1 маски
        {
            string[] files = null;
            SearchOption so = SearchOption.AllDirectories;
            if (!bRecurs)
            {
                so = SearchOption.TopDirectoryOnly;
            }

            try
            {
                LogStateAdd("Поиск файлов " + stMask + "...",0);                 
                //оказывается родная функция криво ищет файлы
                //принимает *.htm и *.html за одно и то же
                //более прямофикс
                files = FindFiles.Find(stPath, stMask, so);
                FoundFiles.AddRange(files);                
            }
            catch (UnauthorizedAccessException UAEx)
            {
                LogStateAdd("Ошибка " + UAEx.Message, OperationState.IOError);
                return OperationState.IOError;
            }
            catch (PathTooLongException PathEx)
            {
                LogStateAdd("Ошибка " + PathEx.Message, OperationState.IOError);
                return OperationState.IOError;
            }
            catch (DirectoryNotFoundException DNFEx)
            {
                LogStateAdd("Ошибка " + DNFEx.Message, OperationState.IOError);
                return OperationState.IOError;
            }
            catch (IOException IOEx)
            {
                LogStateAdd("Ошибка " + IOEx.Message, OperationState.IOError);
                return OperationState.IOError;
            }

            LogStateAdd("Поиск файлов " + stMask + " завершен. Найдено " + files.Length + " файлов", 0);
            return OperationState.Success;
        }

        public void AddMask(string stMask) //добавляет маску в список
        {
            FileMasks.Add(stMask);            
        }
        
        public void ClearMasks() //очищает все маски
        {
            FileMasks.Clear();
        }

        private OperationState FindAllFiles()
        {
            FoundFiles.Clear();
            GlobalLogStateAdd("Начат поиск файлов...", 0);
            for (int i = 0; i < FileMasks.Count; i++)
            {                
                if (FindFilesMask(SourceCatalog, FileMasks[i].ToString(), Recursive)
                    == OperationState.IOError)
                {
                    GlobalLogStateAdd("Поиск файлов завершился из-за ошибки", 
                        OperationState.IOError);
                    return OperationState.IOError;
                }
            }
            return OperationState.Success;
        }

        private OperationState CheckParameters()
        {
            if (FileMasks.Count == 0)
            {
                GlobalLogStateAdd("Ошибка, не указаны маски файлов", 
                    OperationState.IOError);
                return OperationState.IOError;
            }
            if (SourceCatalog == "")
            {
                GlobalLogStateAdd("Ошибка, исходный каталог не указан", 
                    OperationState.IOError);
                return OperationState.IOError;
            }
            if (!Directory.Exists(SourceCatalog))
            {
                GlobalLogStateAdd("Ошибка, исходный каталог не существует", 
                    OperationState.IOError);
                return OperationState.IOError;
            }
            if (!ReplaceSource)
            {
                if (TargetCatalog == "")
                {
                    GlobalLogStateAdd("Ошибка, целевой каталог не указан", 
                        OperationState.IOError);
                    return OperationState.IOError;
                }
            }

            return OperationState.Success;
        }

        private OperationState CreateCatalogStructure()
        {
            if (FoundFiles.Count == 0)
            {
                GlobalLogStateAdd("Ошибка: Файлы не найдены.", OperationState.IOError);
                return OperationState.IOError;
            }
            
            FoundFiles.Sort();
            GlobalLogStateAdd("Создаю структуру каталогов...", OperationState.Success);

            for (int i = 0; i < FoundFiles.Count; i++)
            {
                string curCat = FoundFiles[i].ToString().Replace(SourceCatalog, TargetCatalog);
                try
                {
                    curCat = Path.GetDirectoryName(curCat);
                    if (!Directory.Exists(curCat))
                    {
                        LogStateAdd("Создаю:"+curCat, OperationState.Success);
                        Directory.CreateDirectory(curCat);
                    }
                }
                catch (Exception ex)
                {
                    GlobalLogStateAdd("Ошибка " + ex.Message, OperationState.IOError);
                    return OperationState.IOError;
                }
            }

            return OperationState.Success;
        }
        
        private void StartRecoding(string Source, string Dest)
        {            
            LogStateAdd("Перекодирую:" + Source, 0);
            if (File.Exists(Source)) //проверяем наличие файла
            {
                if (Backup && ReplaceSource) // если заменяем исходник
                {
                    //и стоит флаг бэкапа - бэкапим
                    string BackupFile = Source + ".enc.bak";

                    try
                    {
                        File.Copy(Source, BackupFile);
                    }
                    catch (Exception ex)
                    {
                        LogStateAdd("Ошибка создания копии " + ex.Message + Source, OperationState.IOError);
                        RecodingErrorCount++;
                        return;
                    }
                }

                StreamReader sr = null;
                StreamWriter sw = null;

                try 
                {                                        
                    //готовим поток для чтения в исходной кодировке
                    sr = new StreamReader(Source,
                        Encoding.GetEncoding(SourceEnc));

                    //Поток для записи в целевой кодировке                    
                    if (TargetEnc == 65001) //Если целевая кодировка UTF-8
                    {
                        UTF8Encoding utf8 = new UTF8Encoding(BOM);
                        sw = new StreamWriter(Dest + ".tmp", false, utf8);
                    }
                    else
                    {
                        sw = new StreamWriter(Dest+".tmp",false,
                            Encoding.GetEncoding(TargetEnc));
                    }

                    sw.Write(sr.ReadToEnd());

                    sr.Close();
                    sw.Close();

                    File.Copy(Dest + ".tmp", Dest, true);
                    File.Delete(Dest + ".tmp");
                }
                catch (Exception ex)
                {
                    LogStateAdd("Ошибка " +ex.Message+" "+ Source, OperationState.IOError);
                    if (sr != null) sr.Close();
                    if (sw != null) sr.Close();
                    RecodingErrorCount++;
                    return;
                }                
            }
            else
            {
                LogStateAdd("Файл " + Source + " не существует",OperationState.IOError);
                RecodingErrorCount++;
                return;
            }
        }
        
        public void StartBatchRecoding()
        {
            //проверка параметров
            if (CheckParameters() != OperationState.Success) return;
            //поиск файлов
            if (FindAllFiles() != OperationState.Success) return;

            if (!ReplaceSource) //если заменять в целевом - пропускаем создание структуры каталогов
            {
                //создание стр-ры каталогов
                if (CreateCatalogStructure() != 0) return;
            }

            //процесс перекодировки
            RecodingErrorCount = 0; //очищаем счетчик ошибок            
            for (int i = 0; i < FoundFiles.Count; i++)
            {
                string src = FoundFiles[i].ToString(); //исходное имя файла с полным путем
                string dst = ""; //целевое имя файла                

                if (ReplaceSource) //если заменяем в исходном
                {
                    dst = src;                    
                }
                else
                {
                    dst = src.Replace(SourceCatalog, TargetCatalog); //заменяем исходный каталог
                    //на целевой - получаем новое имя файла
                }

                StartRecoding(src, dst);
            }

            if (RecodingErrorCount > 0)
            {
                GlobalLogStateAdd("Всего файлов: "+FoundFiles.Count.ToString()+"."+
                    " В процессе перекодировки произошло " + RecodingErrorCount.ToString() +
                    " ошибок.", OperationState.IOError);
            }
            else
            {
                GlobalLogStateAdd("Всего файлов "+FoundFiles.Count.ToString()+"."+
                    " Все файлы перекодированы успешно.", OperationState.Success);
            }
        }

        private BOMDetect DetectBOM(string FileName)
        {
            FileStream readStream = null;
            byte[] BOMBuf = new byte[3];

            if (!File.Exists(FileName))
            {
                ErrorMessage = "Файл не найден";
                return BOMDetect.FileError;
            }
            
            try
            {
                //проверяем размер файла
                FileInfo fi = new FileInfo(FileName);
                if (fi.Length < 4) // <4 - точно нет BOM :)
                {
                    return BOMDetect.BOMNotExist;
                }

                //открываем файл для чтения и читаем 4 байта
                readStream = new FileStream(FileName, FileMode.Open);
                readStream.Read(BOMBuf, 0, 3);
                readStream.Close();
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                if (readStream != null) readStream.Close();
                return BOMDetect.FileError;
            }
            
            if ((BOMBuf[0] == 0xEF) && (BOMBuf[1] == 0xBB) && (BOMBuf[2] == 0xBF))
            {
                return BOMDetect.BOMExist;
            }
            else
            {
                return BOMDetect.BOMNotExist;
            }            
        }

        private bool KillBOM(string Source, string Dest)
        {
            int size = 1024; //размер буфера
            byte[] ReadBuf = new byte[size]; // буфер для обмена между потоками
            int count = 0; //для хранения фактически прочитанных байт
            FileStream readStream = null;
            FileStream writeStream = null;

            if (Backup && ReplaceSource) // если заменяем исходник
            {
                //и стоит флаг бэкапа - бэкапим
                string BackupFile = Source + ".rem.bak";

                try
                {
                    File.Copy(Source, BackupFile);
                }
                catch (Exception ex)
                {
                    LogStateAdd("Ошибка создания копии " + ex.Message + Source, OperationState.IOError);
                    RecodingErrorCount++;
                    return false;
                }
            }

            try
            {
                //открыли исходный для чтения
                readStream = new FileStream(Source, FileMode.Open);
                //создали временный для записи
                writeStream = new FileStream(Dest + ".tmp", FileMode.Create);

                readStream.Read(ReadBuf, 0, 3); //прочитали BOM (и забыли про него)

                //обмен между потоками
                do
                {
                    count = readStream.Read(ReadBuf, 0, size); //читаем кусками size
                    if (count > 0) //если данные есть
                    {
                        writeStream.Write(ReadBuf, 0, count);
                    }
                } while (count > 0);
                readStream.Close();
                writeStream.Close();

                File.Copy(Dest + ".tmp", Dest, true);
                File.Delete(Dest + ".tmp");
            }
            catch (Exception ex)
            {
                if (readStream != null) readStream.Close();
                if (writeStream != null) writeStream.Close();
                LogStateAdd("Ошибка " + ex.Message + " " + Source,
                    OperationState.IOError);
                RecodingErrorCount++;
                return false;
            }
            return true;
        }

        public void StartBatchKillBOM()
        {
            //проверка параметров
            if (CheckParameters() != OperationState.Success) return;
            //поиск файлов
            if (FindAllFiles() != OperationState.Success) return;
            
            if (!ReplaceSource) //если заменять в целевом - пропускаем создание структуры каталогов
            {
                //создание стр-ры каталогов
                if (CreateCatalogStructure() != OperationState.Success) return;
            }


            SkipCount = 0; //очищаем счетчики
            RecodingErrorCount = 0;

            //процесс удаления BOM
            GlobalLogStateAdd("Удаляю BOM...", 0);
            for (int i = 0; i < FoundFiles.Count; i++)
            {
                string src = FoundFiles[i].ToString(); //исходное имя файла с полным путем
                string dst = "";
                if (ReplaceSource) //если перезаписываем в целевом каталоге
                {
                    dst = src;
                }
                else
                {
                    dst = src.Replace(SourceCatalog, TargetCatalog); //заменяем исходный каталог
                    //на целевой - получаем новое имя файла
                }

                BOMDetect BD = DetectBOM(src);
                switch (BD)
                {
                    case BOMDetect.BOMExist: //удаляем BOM
                        {
                            if (KillBOM(src, dst))
                            {
                                LogStateAdd(src + ": BOM удален", 
                                    OperationState.Success);
                            }

                        }; break;
                    case BOMDetect.BOMNotExist: //пропускаем файл
                        {
                            LogStateAdd(src + ": BOM не найден", 
                                OperationState.Skip);
                            SkipCount++;
                        }; break;
                    case BOMDetect.FileError: //ошибка
                        {
                            LogStateAdd("Ошибка "+ErrorMessage +" "+
                                src, OperationState.IOError);
                            RecodingErrorCount++;
                        } break;
                }
            }

            if (RecodingErrorCount > 0)
            {
                GlobalLogStateAdd("Всего файлов " + FoundFiles.Count.ToString() + "."
                    + " Пропущено " + SkipCount.ToString() + "."
                    + " Ошибок "+RecodingErrorCount.ToString(), OperationState.IOError);

            }
            else
            {
                GlobalLogStateAdd("Всего файлов " + FoundFiles.Count.ToString() + "."
                    + " Пропущено " + SkipCount.ToString() + ".", OperationState.Success);

            }
        }
    }
}
