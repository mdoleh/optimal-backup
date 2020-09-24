using System;

namespace optimal_backup.Utils
{
    public interface ILogger
    {
        void Log(string message);
        void Log(Exception ex);
    }
}
