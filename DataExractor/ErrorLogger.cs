using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;


namespace DataExractor
{
    public class ErrorLogger
    {
        private static string strLogFilePath = ConfigurationManager.AppSettings["ExeLogPath"].ToString().Trim();
        private static StreamWriter sw = null;

        public enum Loglevel 
        { 
            ERROR = 0, 
            DEBUG = 1, 
            INFO = 2, 
            DATA = 3 
        }

        public static bool WriteErrorLog(string strSource, string strMethodName, string strLogMessage, string strLogType, Exception objException)
        {
            bool bReturn = false;
            int iLogLevel = GetLogLevel(strLogType);

            try
            {
                ErrorLogger objErrorLogger = new ErrorLogger();
                lock (objErrorLogger)
                {
                    if (iLogLevel <= 4)
                    {
                        string strPathName = GetLogFilePath();
                        using (FileStream fs = new FileStream(strPathName, FileMode.Append, FileAccess.Write, FileShare.Read))
                        using (sw = new StreamWriter(fs))
                        {
                            sw.WriteLine("[" + strLogType.Trim() + "] [" + DateTime.Now.ToString("MM/dd/yyyy") + " " + DateTime.Now.ToLongTimeString() + "] Source      : " + strSource.Trim() + " :: " + strMethodName.Trim());
                            sw.WriteLine("Log Message: " + strLogMessage.Trim());
                            if (objException != null)
                            {
                                sw.WriteLine("Error: " + objException.Message.ToString().Trim());
                                sw.WriteLine("Stack Trace: " + objException.StackTrace.ToString().Trim());
                            }
                            sw.WriteLine("^^-----------------------------------------------------------------^^");
                        }
                    }
                    bReturn = true;
                }
            }
            catch
            {
                bReturn = false;
            }
            return bReturn;
        }


        private static int GetLogLevel(string logType)
        {
            switch (logType.Trim().ToUpper())
            {
                case "ERROR": return (int)Loglevel.ERROR;
                case "DEBUG": return (int)Loglevel.DEBUG;
                case "INFO": return (int)Loglevel.INFO;
                case "DATA": return (int)Loglevel.DATA;
                default: return 5;
            }
        }

        private static string GetLogFilePath()
        {
            string baseDir = AppDomain.CurrentDomain.BaseDirectory + AppDomain.CurrentDomain.RelativeSearchPath;
            string retFilePath = string.IsNullOrEmpty(strLogFilePath) ? baseDir + "//" + DateTime.Now.ToShortDateString() : string.Format(strLogFilePath, DateTime.Now);

            if (!Directory.Exists(Path.GetDirectoryName(retFilePath)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(retFilePath));
            }
            return retFilePath;
        }
    }
}
