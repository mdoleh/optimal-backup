using System;
using System.IO;
using System.Text;

namespace optimal_backup.Utils
{
    public class Logger: ILogger
    {
        private StringBuilder _builder;
        private string _filePath;

        public Logger()
        {
            _filePath = Directory.GetCurrentDirectory() + $"\\log_{getTimestamp()}.txt";
            _builder = new StringBuilder();
        }

        public void FlushLogs()
        {
            File.WriteAllText(_filePath, _builder.ToString());
        }

        public void Log(string message)
        {
            Console.WriteLine(message);
            _builder.AppendLine(message);
        }

        public void Log(Exception ex)
        {
            while (ex != null)
            {
                Log(ex.GetType().FullName);
                Log("Message : " + ex.Message);
                Log("StackTrace : " + ex.StackTrace);

                ex = ex.InnerException;
            }
            Log("=======================================");
            Log($"A log file was written to {_filePath}");
            Log("=======================================");
            FlushLogs();
        }

        private string getTimestamp()
        {
            return DateTime.Now.ToString("MM-dd-yyyy-hh_mm");
        }
    }
}
