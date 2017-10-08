using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmsModemClient
{
    public static class ErrorLog
    {
        private static string destinationDirectory = Directory.GetCurrentDirectory();
        private static string filename = "\\ERROR_LOG.txt";
        private static string logPath = destinationDirectory + filename;

        public static void LogError(Exception e)
        {
            if (!Directory.Exists(destinationDirectory)) Directory.CreateDirectory(destinationDirectory); //создать папку, если еще нет
            using (FileStream stream = new FileStream(logPath, FileMode.Append))
            {
                string innerException = "";
                if (e.InnerException != null)
                {
                    innerException = e.InnerException.Source + e.InnerException.Message;
                }
                byte[] array = Encoding.Default.GetBytes(DateTime.Now.ToString() + "      " + e.ToString() + "  " + e.Message + innerException + Environment.NewLine);
                stream.Write(array, 0, array.Length);
            }
        }

        public static void LogMessage(string text)
        {
            if (!Directory.Exists(destinationDirectory)) Directory.CreateDirectory(destinationDirectory); //создать папку, если еще нет
            using (FileStream stream = new FileStream(logPath, FileMode.Append))
            {
                byte[] array = Encoding.Default.GetBytes(DateTime.Now.ToString() + "      " + text + Environment.NewLine);
                stream.Write(array, 0, array.Length);
            }
        }
    }
}
